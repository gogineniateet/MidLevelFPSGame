using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float playerSpeed;
    public float playerJumpForce;
    public float playerRotationSpeed;

    public Camera cam;
    public Animator animator;
    public Transform bulletLaunch;
    //public GameObject target;
    public GameObject steveModelPrefab;
    Rigidbody rb;
    CapsuleCollider colliders;

    Quaternion camRotation;
    Quaternion playerRotation;

    float minX = -90f;
    float maxX = 90f;
    float inputX;
    float inputz;
    int ammo = 50;
    int medical = 100;
    int maxAmmo = 100;
    int maxMedical = 100;
    int reloadAmmo = 0;
    int maxReloadAmmo = 10;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        colliders = GetComponent<CapsuleCollider>();
        //animator = GetComponent<Animator>();
        //audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetBool("isAiming", !animator.GetBool("isAiming"));
        }
        if (Input.GetMouseButtonDown(0) && !animator.GetBool("isFiring"))
        {
            if (ammo > 0)
            {
                //animator.SetBool("IsFiring", !animator.GetBool("IsFiring"));
                animator.SetTrigger("isFiring");
                WhenZombieGotHit();
                ammo = Mathf.Clamp(ammo - 1, 0, maxAmmo);
                //Debug.Log("Ammo Fire Value: " + ammo);
            }
            else
            {
                // Trigger the sound for empty bullets 
            }
        }
        if(Input.GetKeyDown(KeyCode.Space))// jumping with rifle
        {
            rb.AddForce(Vector2.up * playerJumpForce);
            animator.SetTrigger("isJumping");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("isReloading");
            int amountAmmoNeeded = maxReloadAmmo - reloadAmmo;
            int ammoAvailable = amountAmmoNeeded < ammo ? amountAmmoNeeded : ammo;

            reloadAmmo += ammoAvailable;
            ammo -= ammoAvailable;
            Debug.Log("Ammo Loaded Value: " + ammo);
            Debug.Log("Ammo Reloaded Value: " + reloadAmmo);
        }
        if (Mathf.Abs(inputX) > 0 || Mathf.Abs(inputz) > 0)
        {
            if (!animator.GetBool("isWalking"))
                animator.SetBool("isWalking", true);
        }
        else if (animator.GetBool("isWalking"))
        {
            animator.SetBool("isWalking", false);
        }

    }

    public void WhenZombieGotHit()
    {
        RaycastHit hitInfo;
        if(Physics.Raycast(bulletLaunch.position,bulletLaunch.forward,out hitInfo,100f))
        {
            GameObject hitZombie = hitInfo.collider.gameObject;
            if (hitZombie.tag == "Zombie")
            {
                if(UnityEngine.Random.Range(0,10)<5)
                {
                    GameObject tempRD = hitZombie.GetComponent<ZombieController>().ragdollPrefab;
                    GameObject newTempRD = Instantiate(tempRD, hitZombie.transform.position, hitZombie.transform.rotation);
                    newTempRD.transform.Find("Hips").GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 1000);
                    Destroy(hitZombie);
                }
                else
                {
                    hitZombie.GetComponent<ZombieController>().KillZombie();
                }
                //GameObject tempRd = Instantiate(ragdollPrefab, this.transform.position, this.transform.rotation);
                //tempRd.transform.Find("Hips").GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 1000);
                //Destroy(this.gameObject);
                
            }
        }

    }

    private void FixedUpdate()
    {
        inputX = Input.GetAxis("Horizontal") * playerSpeed;
        inputz = Input.GetAxis("Vertical") * playerSpeed;

        transform.position += new Vector3(inputX, 0f, inputz);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * playerJumpForce);

        }
        
        float mouseX = Input.GetAxis("Mouse X") * playerRotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * playerRotationSpeed;
        //Debug.Log(mouseY);
        playerRotation = Quaternion.Euler(0f, mouseX, 0f);
        camRotation = Quaternion.Euler(-mouseY, 0f, 0f);
        camRotation = ClampRotationPlayer(camRotation);
        // this.transform.localRotation = playerRotation;
        transform.localRotation = playerRotation * transform.localRotation;
        cam.transform.localRotation = camRotation * cam.transform.localRotation;

    }
    public bool IsGrounded()
    {
        RaycastHit rayCastHit;
        if (Physics.SphereCast(transform.position, colliders.radius, Vector3.down, out rayCastHit, (colliders.height / 2) - colliders.radius + 0.1f))
        {
            return true;
        }
        else
            return false;
    }
    public Quaternion ClampRotationPlayer(Quaternion n)
    {
        n.w = 1f;
        n.x /= n.w;
        n.y /= n.w;
        n.z /= n.w;
        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(n.x);
        angleX = Mathf.Clamp(angleX, minX, maxX);
        n.x = Mathf.Tan(Mathf.Deg2Rad * angleX * 0.5f);
        return (n);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ammo" && ammo < maxAmmo)
        {
            Debug.Log("Collected box");
            //ammo += 10;
            ammo = Mathf.Clamp(ammo + 10, 0, maxAmmo);
            Debug.Log("Ammo: " + ammo);
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "Medical" && medical < maxMedical)
        {
            Debug.Log("Collected medical box");
            //medical += 10;
            medical = Mathf.Clamp(medical + 10, 0, maxMedical);
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Lava")
        {
            // Need to Trigger dead sound , when medical is zero
            medical = Mathf.Clamp(medical - 10, 0, maxMedical);
            //Debug.Log("Medical: " + medical);
        }
    }


    public void TakeHit(float value)
    {
        medical = (int)Mathf.Clamp(medical - value, 0, maxMedical); //  medical is health
        Debug.Log("heath :" + medical);
        if(medical <= 0)
        {
            Vector3 position = new Vector3(transform.position.x, Terrain.activeTerrain.SampleHeight(this.transform.position), transform.position.z);
            GameObject tempSteve = Instantiate(steveModelPrefab, position, this.transform.rotation);
            tempSteve.GetComponent<Animator>().SetTrigger("Death");
            GameState.isGameOver = true;
            Destroy(this.gameObject);
        }
    }
    
}
