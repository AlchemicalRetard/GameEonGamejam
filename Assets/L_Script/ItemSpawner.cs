using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // Assign your prefab in the inspector
    public float xRange = 10f; // Range of x positions for spawning
    public float zRange = 10f; // Range of z positions for spawning
    public int maxObjects = 20; // Maximum number of objects allowed in the scene

    public static int currentObjects = 0; // Counter for the current number of objects

    void Start()
    {
        // Spawn maxObjects at the start
        for (int i = 0; i < maxObjects; i++)
        {
            SpawnObject();
        }
    }

    public void SpawnObject()
    {
        // Generate a random position within the specified ranges
        Vector3 randomPosition = new Vector3(
            transform.position.x + Random.Range(-xRange, xRange),
            transform.position.y, // y position is set to the GameObject's y position
            transform.position.z + Random.Range(-zRange, zRange)
        );

        // Instantiate the object at the random position
        GameObject spawnedObject = Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
        currentObjects++;

        // Add the object to a list so we can keep track of it
        spawnedObject.AddComponent<SpawnedObject>();
    }
}

public class SpawnedObject : MonoBehaviour
{
    private void OnDestroy()
    {
        ItemSpawner.currentObjects--;
        // Spawn a new object at a random location when an object is destroyed
        FindObjectOfType<ItemSpawner>().SpawnObject();
    }
}
