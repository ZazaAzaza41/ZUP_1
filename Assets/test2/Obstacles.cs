using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public GameObject player;

    public GameObject enemyprefab0;
    public GameObject enemyprefab1;
    public GameObject enemyprefab2;

    public List<GameObject> objectlist;

    public float timespawn = 3f;
    private float timer;
    private int maxEnemy = 21;

    private void Awake()
    {

        objectlist = new List<GameObject> { enemyprefab0, enemyprefab1, enemyprefab2 };
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
            Instantiate(objectlist[obj], new Vector3(x, y, z), Quaternion.identity, transform);
            obj = UnityEngine.Random.Range(0, 3);
            Instantiate(objectlist[obj], new Vector3(0, y, z), Quaternion.identity, transform);
            obj = UnityEngine.Random.Range(0, 3);
            Instantiate(objectlist[obj], new Vector3(2.65f, y, z), Quaternion.identity, transform);
        }
        else if (x == 2)
        {
            x = 0;
            Instantiate(objectlist[obj], new Vector3(x, y, z), Quaternion.identity, transform);
            obj = UnityEngine.Random.Range(0, 3);
            Instantiate(objectlist[obj], new Vector3(-2.65f, y, z), Quaternion.identity, transform);
            obj = UnityEngine.Random.Range(0, 3);
            Instantiate(objectlist[obj], new Vector3(2.65f, 5, player.transform.position.z), Quaternion.identity, transform);
        }
        else
        {
            x = 2.65f;
            Instantiate(objectlist[obj], new Vector3(x, y, z), Quaternion.identity, transform);
            obj = UnityEngine.Random.Range(0, 3);
            Instantiate(objectlist[obj], new Vector3(-2.65f, y, z), Quaternion.identity, transform);
            obj = UnityEngine.Random.Range(0, 3);
            Instantiate(objectlist[obj], new Vector3(0f, y, z), Quaternion.identity, transform);
        }
    }
    void Update()
    {

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = timespawn;
            if (transform.childCount < maxEnemy)
            {
                //Instantiate(enemyprefab, UnityEngine.Random.insideUnitCircle * distance, Quaternion.identity, transform);
                spawnobject(1.1f, player.transform.position.z+10);

            }

        }
    }
}
