using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public float min_X = -2.7f, max_X = 2.7f;

    public GameObject[] asteroid_Prefabs;
    public GameObject enemyPrefab;
    public GameObject boostPrefab;

    public float timer = 4f;
    private float count = 1f;
    private float haha = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
        Invoke("SpawnEnemies", timer);
        

    }

    void SpawnEnemies()
    {

        float pos_X = Random.Range(min_X, max_X);
        Vector3 temp = transform.position;
        temp.x = pos_X;
        haha++;
        if(haha == 10)
        {
            Instantiate(boostPrefab, temp, Quaternion.Euler(0f, 0f, 0f));
            haha = 0;
        }
        if (Random.Range(0, 2) > 0)
        {

            Instantiate(asteroid_Prefabs[Random.Range(0, asteroid_Prefabs.Length)],
            temp, Quaternion.identity);

        }
        else
        {
            Instantiate(enemyPrefab, temp, Quaternion.Euler(0f, 0f, 180f));

        }

        Invoke("SpawnEnemies", timer);
        count++;
        if (count > 5)
            timer = 3.5f;
        if (count > 10)
            timer = 3f;
        if (count > 15)
            timer = 2.5f;
        if (count > 20)
            timer = 2f;
        if (count > 25)
            timer = 1.5f;
        if (count > 35)
            timer = 1f;
    }

}
