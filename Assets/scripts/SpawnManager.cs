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
    Vector3 result;


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
                result = hit.position;
                Instantiate(zombiePrefabs[0], result, Quaternion.identity);
                //Instantiate(zombiePrefabs[1], result, Quaternion.identity);
                //Instantiate(zombiePrefabs[2], result, Quaternion.identity);
                //Instantiate(zombiePrefabs[3], result, Quaternion.identity);
            }
            else
            {
                i--;
            }
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if(!spawnOnStart && other.gameObject.tag == "Player")
        {
            CreateAllZombies();
        }
    }
}
