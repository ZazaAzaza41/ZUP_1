using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    public GameObject player;

    public GameObject enemyprefab0;
    public GameObject enemyprefab1;
    public GameObject enemyprefab1_2;
    public GameObject enemyprefab2;
    public GameObject enemyprefab2_2;
    public GameObject enemyprefab3;

    public List<GameObject> objectlist;

    public float timespawn;
    public float livetime;
    private float timer;
    //private int maxEnemy = 21;
    private int kolobstacles;

    private void Awake()
    {

        objectlist = new List<GameObject> { enemyprefab0, enemyprefab1, enemyprefab1_2, enemyprefab2, enemyprefab2_2, enemyprefab3 };
        kolobstacles = objectlist.Count;
        timespawn = 1f;
        livetime = 4.5f;

    }


    //private float distance = 3;
    void Start()
    {
        timer = timespawn;
        //Debug.Log(objectlist.Count);
    }
    void spawnobject(float y, float z,float miny, float maxy)
    {
        int obj;
        obj = 0;
        int xx = UnityEngine.Random.Range(1, 4);
        float x;
        if (xx == 1)
        {
            x = -2.65f;
            GameObject spawnedObject = Instantiate(objectlist[obj], new Vector3(x, UnityEngine.Random.Range(y, miny), z), Quaternion.identity, transform);
            Destroy(spawnedObject, livetime + timespawn - 0.1f);
            obj = UnityEngine.Random.Range(0, kolobstacles);
            GameObject spawnedObject2 = Instantiate(objectlist[obj], new Vector3(0, UnityEngine.Random.Range(y, miny), z), Quaternion.identity, transform);
            Destroy(spawnedObject2, livetime + timespawn - 0.1f);
            obj = UnityEngine.Random.Range(0, kolobstacles);
            GameObject spawnedObject3 = Instantiate(objectlist[obj], new Vector3(2.65f,UnityEngine.Random.Range(y, miny), z), Quaternion.identity, transform);
            Destroy(spawnedObject3, livetime + timespawn - 0.1f);

            obj = UnityEngine.Random.Range(0, kolobstacles);
            GameObject spawnedObject11 = Instantiate(objectlist[obj], new Vector3(x, y + UnityEngine.Random.Range(miny, miny), z), Quaternion.identity, transform);
            Destroy(spawnedObject11, livetime + timespawn - 0.1f);
            obj = UnityEngine.Random.Range(0, kolobstacles);
            GameObject spawnedObject22 = Instantiate(objectlist[obj], new Vector3(0, y + UnityEngine.Random.Range(miny, maxy), z), Quaternion.identity, transform);
            Destroy(spawnedObject22, livetime + timespawn - 0.1f);
            obj = UnityEngine.Random.Range(0, kolobstacles);
            GameObject spawnedObject33 = Instantiate(objectlist[obj], new Vector3(2.65f, y + UnityEngine.Random.Range(miny, maxy), z), Quaternion.identity, transform);
            Destroy(spawnedObject33, livetime + timespawn - 0.1f);

        }
        else if (xx == 2)
        {
            x = 0;
            GameObject spawnedObject = Instantiate(objectlist[obj], new Vector3(x, UnityEngine.Random.Range(y, miny), z), Quaternion.identity, transform);
            Destroy(spawnedObject, livetime + timespawn - 0.1f);
            obj = UnityEngine.Random.Range(0, kolobstacles);
            GameObject spawnedObject2 = Instantiate(objectlist[obj], new Vector3(-2.65f, UnityEngine.Random.Range(y, miny), z), Quaternion.identity, transform);
            Destroy(spawnedObject2, livetime + timespawn - 0.1f);
            obj = UnityEngine.Random.Range(0, kolobstacles);
            GameObject spawnedObject3 = Instantiate(objectlist[obj], new Vector3(2.65f, UnityEngine.Random.Range(y, miny), z), Quaternion.identity, transform);
            Destroy(spawnedObject3, livetime + timespawn - 0.1f);

            obj = UnityEngine.Random.Range(0, kolobstacles);
            GameObject spawnedObject11 = Instantiate(objectlist[obj], new Vector3(x, y+UnityEngine.Random.Range(miny, maxy), z), Quaternion.identity, transform);
            Destroy(spawnedObject11, livetime + timespawn - 0.1f);
            obj = UnityEngine.Random.Range(0, kolobstacles);
            GameObject spawnedObject22 = Instantiate(objectlist[obj], new Vector3(-2.65f, y +UnityEngine.Random.Range(miny, maxy), z), Quaternion.identity, transform);
            Destroy(spawnedObject22, livetime + timespawn - 0.1f);
            obj = UnityEngine.Random.Range(0, kolobstacles);
            GameObject spawnedObject33 = Instantiate(objectlist[obj], new Vector3(2.65f, y +UnityEngine.Random.Range(miny, maxy), z), Quaternion.identity, transform);
            Destroy(spawnedObject33, livetime + timespawn - 0.1f);
        }
        else
        {
            x = 2.65f;
            GameObject spawnedObject = Instantiate(objectlist[obj], new Vector3(x, UnityEngine.Random.Range(y, miny), z), Quaternion.identity, transform);
            Destroy(spawnedObject, livetime + timespawn - 0.1f);
            obj = UnityEngine.Random.Range(0, kolobstacles);
            GameObject spawnedObject2 = Instantiate(objectlist[obj], new Vector3(-2.65f, UnityEngine.Random.Range(y, miny), z), Quaternion.identity, transform);
            Destroy(spawnedObject2, livetime + timespawn - 0.1f);
            obj = UnityEngine.Random.Range(0, kolobstacles);
            GameObject spawnedObject3 = Instantiate(objectlist[obj], new Vector3(0f, UnityEngine.Random.Range(y, miny), z), Quaternion.identity, transform);
            Destroy(spawnedObject3, livetime + timespawn - 0.1f);

            obj = UnityEngine.Random.Range(0, kolobstacles);
            GameObject spawnedObject11 = Instantiate(objectlist[obj], new Vector3(x, y + UnityEngine.Random.Range(miny, maxy), z), Quaternion.identity, transform);
            Destroy(spawnedObject11, livetime + timespawn - 0.1f);
            obj = UnityEngine.Random.Range(0, kolobstacles);
            GameObject spawnedObject22 = Instantiate(objectlist[obj], new Vector3(-2.65f, y + UnityEngine.Random.Range(miny, maxy), z), Quaternion.identity, transform);
            Destroy(spawnedObject22, livetime + timespawn - 0.1f);
            obj = UnityEngine.Random.Range(0, kolobstacles);
            GameObject spawnedObject33 = Instantiate(objectlist[obj], new Vector3(0f, y + UnityEngine.Random.Range(miny, maxy), z), Quaternion.identity, transform);
            Destroy(spawnedObject33, livetime + timespawn - 0.1f);
        }
    }
    void Update()
    {

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = timespawn;
            spawnobject(1.1f, player.transform.position.z + 70, 2.5f, 6f);

        }
    }
}
