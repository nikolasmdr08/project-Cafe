using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] float spawnDelay = 0.5f;
    [SerializeField] float spawnInterval = 1.5f;
    [SerializeField] float spawnInitialPositionX = 12f;
    [SerializeField] float spawnInitialPositionY = -2.24f;

    void Start()
    {
        InvokeRepeating("Spawn", spawnDelay, spawnInterval);
    }

    void Spawn()
    {

        Vector3 spawnLocation = new Vector3(spawnInitialPositionX, spawnInitialPositionY, 0);
        int index = Random.Range(0, enemyPrefabs.Length);

        Instantiate(enemyPrefabs[index], spawnLocation, enemyPrefabs[index].transform.rotation);

    }
}
