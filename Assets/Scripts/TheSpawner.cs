using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnPoints = null;

    [SerializeField]
    private MovableObject objectToSpawn = null;

    [SerializeField]
    private Transform parentForSpawnedObjects = null;

    [SerializeField]
    private int numberOfObjectsToSpawn = 0;

    [SerializeField]
    private float timeBetweenSpawns = 0.0f;

    private float timeToSpawnNextObject;
    private int spawnPointIndex;

    private List<MovableObject> spawnQueue = new List<MovableObject>();

    private void Awake()
    {
        SpawnRange(objectToSpawn, numberOfObjectsToSpawn);
    }

    private void Update()
    {
        if (spawnQueue.Count > 0 && timeToSpawnNextObject < Time.time)
        {
            Transform spawnPoint = spawnPoints[spawnPointIndex];
            // Increment but go back to zero when overcome spawnPoints.Lenght
            spawnPointIndex = (spawnPointIndex + 1) % spawnPoints.Length;

            Instantiate(spawnQueue[0], spawnPoint.position, spawnPoint.rotation, parent: parentForSpawnedObjects);

            spawnQueue.RemoveAt(0);

            timeToSpawnNextObject = Time.time + timeBetweenSpawns;
        }
    }

    public void SpawnRange(MovableObject movableObjectToSpawn, float amountOfObjectsToSpawn)
    {
        timeToSpawnNextObject = Time.time + timeBetweenSpawns;
        for (int i = 0; i < amountOfObjectsToSpawn; i++)
        {
            spawnQueue.Add(movableObjectToSpawn);
        }
    }
}
