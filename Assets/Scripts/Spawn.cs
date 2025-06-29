using System;
using Unity.VisualScripting;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject enemyprefab1;


    public GameObject enemyprefab2;

    public float timespawn = 1f;
    private float timer;

    private int maxEnemy = 5;

    //private float distance = 3;
    void Start()
    {
        timer = timespawn;
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
                int x;
                
                x = UnityEngine.Random.Range(1, 7);
                if (x > 4)
                {
                    x = 5;
                }
                else if (x > 2)
                {
                    x = 3;
                }
                else
                {
                    x = 1;
                }
                
                
                //Instantiate(enemyprefab1, new Vector3(x, 5, 0), Quaternion.identity, transform);
            }
                
        }
    }
}
