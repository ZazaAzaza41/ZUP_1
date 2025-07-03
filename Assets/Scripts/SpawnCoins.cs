using System.Collections.Generic;
using UnityEngine;

public class SpawnCoins : MonoBehaviour
{
    public GameObject player;

    public GameObject coinprefab1;

    public List<GameObject> objectlist;

    public float timespawn;
    public float livetime;
    private float timer;
    //private int maxEnemy = 21;
    private int kolobstacles;

    private void Awake()
    {

        objectlist = new List<GameObject> {coinprefab1};
        kolobstacles = objectlist.Count;
        timespawn = 2f;
        livetime = 5.5f;

    }


    //private float distance = 3;
    void Start()
    {
        timer = timespawn;
        //Debug.Log(objectlist.Count);
    }
    void spawnobject(float y, float z, float dist)
    {
        int obj = 0;
        float x = UnityEngine.Random.Range(-1, 2) * 2.65f;
        int kolMoneyOnTheWay = UnityEngine.Random.Range(1, 6);

        for (int i = 0; i < kolMoneyOnTheWay; ++i)
        {
            GameObject spawnedObject = Instantiate(objectlist[obj], new Vector3(x, y , z + dist + i*2), Quaternion.identity, transform);
            if (spawnedObject!=null)
            {
                Destroy(spawnedObject, livetime + timespawn - 0.1f);
            }
 
        }

        obj = 0;
        x = UnityEngine.Random.Range(-1, 2) * 2.65f;
        kolMoneyOnTheWay = UnityEngine.Random.Range(1, 6);

        for (int i = 0; i < kolMoneyOnTheWay; ++i)
        {
            GameObject spawnedObject = Instantiate(objectlist[obj], new Vector3(x, y + 2, z + dist + i * 2), Quaternion.identity, transform);
            if (spawnedObject != null)
            {
                Destroy(spawnedObject, livetime + timespawn - 0.1f);
            }

        }

        obj = 0;
        x = UnityEngine.Random.Range(-1, 2) * 2.65f;
        kolMoneyOnTheWay = UnityEngine.Random.Range(1, 6);

        for (int i = 0; i < kolMoneyOnTheWay; ++i)
        {
            GameObject spawnedObject = Instantiate(objectlist[obj], new Vector3(x, y + 4, z + dist + i * 2), Quaternion.identity, transform);
            if (spawnedObject != null)
            {
                Destroy(spawnedObject, livetime + timespawn - 0.1f);
            }

        }

    }
    void Update()
    {

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = timespawn;
            spawnobject(1.1f, player.transform.position.z + 70, 20);

        }
    }
}
