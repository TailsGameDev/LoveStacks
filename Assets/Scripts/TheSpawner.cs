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

    private void Awake()
    {
        timeToSpawnNextObject = Time.time + timeBetweenSpawns;
    }

    private void Update()
    {
        if (numberOfObjectsToSpawn > 0 && timeToSpawnNextObject < Time.time)
        {
            Transform spawnPoint = spawnPoints[spawnPointIndex];
            // Increment but go back to zero when overcome spawnPoints.Lenght
            spawnPointIndex = (spawnPointIndex + 1) % spawnPoints.Length;

            Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation, parent: parentForSpawnedObjects);

            numberOfObjectsToSpawn--;

            timeToSpawnNextObject = Time.time + timeBetweenSpawns;
        }
    }
}
