using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    public GameObject player;

    public GameObject enemyprefab0;
    public GameObject enemyprefab1;
    public GameObject enemyprefab2;

    public List<GameObject> objectlist;

    public float timespawn;
    public float livetime;
    private float timer;
    //private int maxEnemy = 21;
    private int kolobstacles;

    private void Awake()
    {

        objectlist = new List<GameObject> { enemyprefab0, enemyprefab1, enemyprefab2 };
        kolobstacles = objectlist.Count;
        timespawn = 3f;
        livetime = 3.5f;

    }


    //private float distance = 3;
    void Start()
    {
        timer = timespawn;
    }
    void spawnobject(float y, float z)
    {
        int obj = UnityEngine.Random.Range(0, 2);
        float x = UnityEngine.Random.Range(1f, 4f);
        if (x == 1)
        {
            x = -2.65f;
            GameObject spawnedObject = Instantiate(objectlist[obj], new Vector3(x, y, z), Quaternion.identity, transform);
            Destroy(spawnedObject, livetime + timespawn - 0.1f);
            obj = UnityEngine.Random.Range(0, kolobstacles);
            GameObject spawnedObject2 = Instantiate(objectlist[obj], new Vector3(0, y, z), Quaternion.identity, transform);
            Destroy(spawnedObject2, livetime + timespawn - 0.1f);
            obj = UnityEngine.Random.Range(0, kolobstacles);
            GameObject spawnedObject3 = Instantiate(objectlist[obj], new Vector3(2.65f, y, z), Quaternion.identity, transform);
            Destroy(spawnedObject2, livetime + timespawn - 0.1f);
        }
        else if (x == 2)
        {
            x = 0;
            GameObject spawnedObject = Instantiate(objectlist[obj], new Vector3(x, y, z), Quaternion.identity, transform);
            Destroy(spawnedObject, livetime + timespawn - 0.1f);
            obj = UnityEngine.Random.Range(0, kolobstacles);
            GameObject spawnedObject2 = Instantiate(objectlist[obj], new Vector3(-2.65f, y, z), Quaternion.identity, transform);
            Destroy(spawnedObject2, livetime + timespawn - 0.1f);
            obj = UnityEngine.Random.Range(0, kolobstacles);
            GameObject spawnedObject3 = Instantiate(objectlist[obj], new Vector3(2.65f, 5, player.transform.position.z), Quaternion.identity, transform);
            Destroy(spawnedObject3, livetime + timespawn - 0.1f);
        }
        else
        {
            x = 2.65f;
            GameObject spawnedObject = Instantiate(objectlist[obj], new Vector3(x, y, z), Quaternion.identity, transform);
            Destroy(spawnedObject, livetime + timespawn - 0.1f);
            obj = UnityEngine.Random.Range(0, 3);
            GameObject spawnedObject2 = Instantiate(objectlist[obj], new Vector3(-2.65f, y, z), Quaternion.identity, transform);
            Destroy(spawnedObject2, livetime + timespawn - 0.1f);
            obj = UnityEngine.Random.Range(0, 3);
            GameObject spawnedObject3 = Instantiate(objectlist[obj], new Vector3(0f, y, z), Quaternion.identity, transform);
            Destroy(spawnedObject3, livetime + timespawn - 0.1f);
        }
    }
    void Update()
    {

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = timespawn;
            spawnobject(7f, player.transform.position.z + 70);

        }
    }
}
