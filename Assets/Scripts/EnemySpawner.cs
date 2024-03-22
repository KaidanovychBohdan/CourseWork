using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject SpawnEnemy(int enemyNumb)
    {
        return Instantiate(enemyPrefabs[enemyNumb], transform.position, Quaternion.identity); 
    }
}
