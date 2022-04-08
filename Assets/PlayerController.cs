using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isGrounded;
    public float playerSpeed;
    public float jumpForce;
    public float rotationSpeed;
    Rigidbody rb;
    public Camera cam;
    CapsuleCollider capsuleCollider;
    Quaternion playerRotation;
    Quaternion camRotation;
    private float minX = -90f;
    private float maxX = 90f;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        //cam = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");        
        transform.position += new Vector3(inputX * playerSpeed, 0, inputZ * playerSpeed);
        if((Input.GetKeyDown(KeyCode.Space))  &&  IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce);
        }

        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;
        playerRotation *= Quaternion.Euler(0, mouseY, 0);
        camRotation = ClamRotationPlayer(Quaternion.Euler(-mouseX, 0, 0) * camRotation);
        this.transform.localRotation = playerRotation;
        cam.transform.localRotation = camRotation;
        Debug.Log(mouseX);
        Debug.Log(mouseY);
                
    }
    public bool IsGrounded()
    {
        RaycastHit raycastHit;
        if (Physics.SphereCast(transform.position, capsuleCollider.radius, Vector3.down, out raycastHit, (capsuleCollider.height / 2f) - capsuleCollider.radius + 0.1f))
        {
            return true;
        }
        else
            return false;
    }
    Quaternion ClamRotationPlayer(Quaternion quaternion)
    {
        quaternion.w = 1f;
        quaternion.x /= quaternion.w;
        quaternion.y /= quaternion.w;
        quaternion.z /= quaternion.w;
        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(quaternion.x);
        angleX = Mathf.Clamp(angleX, minX, maxX);
        quaternion.x = Mathf.Tan(Mathf.Deg2Rad * 0.5f * angleX);
        return quaternion;
    }


}
