using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // Assign your prefab in the inspector
    public float spawnInterval = 10f; // Time interval between spawns
    public float xRange = 10f; // Range of x positions for spawning
    public float zRange = 10f; // Range of z positions for spawning
    public int objectsPerInterval = 3; // Number of objects to spawn at each interval
    public int maxObjects = 20; // Maximum number of objects allowed in the scene

    public static int currentObjects = 0; // Counter for the current number of objects

    void Start()
    {
        StartCoroutine(SpawnObject());
    }

    IEnumerator SpawnObject()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (currentObjects < maxObjects)
            {
                for (int i = 0; i < objectsPerInterval; i++)
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

            // If there are too many objects, stop spawning until some are destroyed
            while (currentObjects >= maxObjects)
            {
                yield return null;
            }
        }
    }
}

public class SpawnedObject : MonoBehaviour
{
    private void OnDestroy()
    {
        ItemSpawner.currentObjects--;
    }
}

