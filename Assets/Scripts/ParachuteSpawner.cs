using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;

public class ParachuteSpawner : MonoBehaviourPunCallbacks
{
    public GameObject[] objectPrefab; // An array of prefabs of the objects you want to spawn.
    public string requiredTag; // The tag of the object that must be disabled before spawning.
    public List<Transform> spawnPoints; // The list of spawn points where the objects will be spawned.
    public float spawnInterval; // The time between each spawn.

    private List<GameObject> spawnedObjects = new List<GameObject>(); // The list of spawned objects.
    private bool canSpawn = false; // Whether the spawner is currently able to spawn.

    void Start()
    {
        canSpawn = false;
    }

    void Update()
    {
        // Check if the required object has been disabled.
        if (!canSpawn && GameObject.FindGameObjectWithTag(requiredTag) == null)
        {
            canSpawn = true;
            InvokeRepeating("SpawnObject", 0f, spawnInterval);
        }
    }

    void SpawnObject()
    {
        // Check if the spawner is able to spawn.
        if (!canSpawn)
        {
            return;
        }

        // Choose a random spawn point.
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

        // Check if the chosen spawn point is clear.
        if (IsSpawnPointClear(spawnPoint.position))
        {
            // Choose a random prefab from the array.
            int index = Random.Range(0, objectPrefab.Length);
            GameObject obj = PhotonNetwork.Instantiate(objectPrefab[index].name, spawnPoint.position, spawnPoint.rotation);
            spawnedObjects.Add(obj);
        }
    }

    bool IsSpawnPointClear(Vector3 position)
    {
        // Check if any spawned objects are already at the position.
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null && Vector3.Distance(obj.transform.position, position) < 1f)
            {
                return false;
            }
        }

        // Check if any non-spawned objects are already at the position.
        Collider[] colliders = Physics.OverlapSphere(position, 1f);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("SpawnPoint") || collider.CompareTag("Player"))
            {
                return false;
            }
        }

        return true;
    }
}
