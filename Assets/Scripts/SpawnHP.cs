using System.Collections.Generic;
using UnityEngine;

public class SpawnHP : MonoBehaviour
{
    public GameObject player;

    public GameObject heartprefab1;

    public List<GameObject> objectlist;

    public float timespawn;
    public float livetime;
    private float timer;


    private void Awake()
    {

        objectlist = new List<GameObject> { heartprefab1 };
        timespawn = 6f;
        livetime = 5.5f;

    }
    void Start()
    {
        timer = timespawn;
    }
    void spawnobject(float y, float z, float dist)
    {
        int obj = 0;
        float x = UnityEngine.Random.Range(-1, 2) * 2.65f;
        int isheartspawn = UnityEngine.Random.Range(0, 10);
        if (isheartspawn == 5)
        {
            GameObject spawnedObject = Instantiate(objectlist[obj], new Vector3(x, UnityEngine.Random.Range(y, 8f), z + UnityEngine.Random.Range(10f, dist)), Quaternion.identity, transform);
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
            spawnobject(1.1f, player.transform.position.z + 70, 50);

        }
    }
}
