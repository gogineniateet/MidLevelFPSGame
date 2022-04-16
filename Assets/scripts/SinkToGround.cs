using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkToGround : MonoBehaviour
{
    float destroyHeight;
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        if(this.gameObject.tag == "RagdollZombie")
        {
           Invoke("ReadyToSink",2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SinkIntoGround()
    {
        this.transform.Translate(0f, -0.001f, 0f);
        if(transform.position.y > destroyHeight)
        {
            Destroy(this.gameObject);
        }
    }
    public void ReadyToSink()
    {
        destroyHeight = Terrain.activeTerrain.SampleHeight(this.transform.position) - 5f;
        Collider[] collidersList = this.transform.GetComponentsInChildren<Collider>();
        foreach (Collider item in collidersList)
        {
            Destroy(item);
        }
        InvokeRepeating("SinkIntoGround", 5f, 0.1f);
    }
}
