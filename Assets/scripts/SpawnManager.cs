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
    Stack<GameObject> zombie = new Stack<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        if(spawnOnStart)
        {
            CreateAllZombies(10);
        }
    }

    public void CreateAllZombies(int value)
    {
        for (int i = 0; i < number; i++)
        {
            Vector3 randonPoint = transform.position + Random.insideUnitSphere * spawnRadius;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randonPoint, out hit, 10f, NavMesh.AllAreas))
            {
                result = hit.position;
                zombie.Push(Instantiate(zombiePrefabs[0], result, Quaternion.identity));
                //zombiePrefabs[0].SetActive(false);
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

    public void BackToPool(GameObject obj)
    {
        zombie.Push(obj);
        zombie.Peek().SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!spawnOnStart && other.gameObject.tag == "Player")
        {
            CreateAllZombies(10);
        }
    }
}
