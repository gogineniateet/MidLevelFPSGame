using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] zombiePrefabs;
    public int number;
    public float spawnRadius;
    public bool spawnOnStart = true;

    // Start is called before the first frame update
    void Start()
    {
        if(spawnOnStart)
        {
            CreateAllZombies();

        }
    }

    private void CreateAllZombies()
    {
        for (int i = 0; i < number; i++)
        {
            Vector3 randonPoint = transform.position + Random.insideUnitSphere * spawnRadius;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randonPoint, out hit, 10f, NavMesh.AllAreas))
            {
                Instantiate(zombiePrefabs[0], randonPoint, Quaternion.identity);
            }
            else
            {
                i--;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!spawnOnStart && other.gameObject.tag == "Player")
        {
            CreateAllZombies();

        }
    }
}
