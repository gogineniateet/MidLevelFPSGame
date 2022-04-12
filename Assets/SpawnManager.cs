using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] zombiePrefabs;
    public int number;
    public float spawnRadius;
    

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < number; i++)
        {
            Vector3 randonPoint = transform.position + Random.insideUnitSphere * spawnRadius;
            Instantiate(zombiePrefabs[0], randonPoint, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
