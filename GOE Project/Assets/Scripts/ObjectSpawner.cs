using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{

    [Header("Spawner")]
    [SerializeField] List<GameObject> Spawner1 = new List<GameObject>();
    [SerializeField] List<GameObject> Spawner2 = new List<GameObject>();
    [SerializeField] List<GameObject> Spawner3 = new List<GameObject>();

    [Header("Spawn Points")]
    [SerializeField] Transform spawnPoint1;
    [SerializeField] Transform spawnPoint2;
    [SerializeField] Transform spawnPoint3;

    [Header("Per-slot Float / Sink Targets")]
    [SerializeField] Transform floatTarget1;
    [SerializeField] Transform sinkTarget1;

    [SerializeField] Transform floatTarget2;
    [SerializeField] Transform sinkTarget2;

    [SerializeField] Transform floatTarget3;
    [SerializeField] Transform sinkTarget3;

    [Header("Button Container")]
    [SerializeField] GameObject buttonContainer1;
    [SerializeField] GameObject buttonContainer2;
    [SerializeField] GameObject buttonContainer3;

    void Start()
    {
        buttonContainer1.SetActive(false);
        buttonContainer2.SetActive(false);
        buttonContainer3.SetActive(false);
        SpawnObjects();
    }

    void SpawnObjects()
    {
        if (Spawner1.Count > 0)
        {
            int r1 = UnityEngine.Random.Range(0, Spawner1.Count);
            GameObject obj1 = Instantiate(Spawner1[r1], spawnPoint1.position, Quaternion.identity);
            AssignTargets(obj1, floatTarget1, sinkTarget1, buttonContainer1);
        }

        if (Spawner2.Count > 0)
        {
            int r2 = UnityEngine.Random.Range(0, Spawner2.Count);
            GameObject obj2 = Instantiate(Spawner2[r2], spawnPoint2.position, Quaternion.identity);
            AssignTargets(obj2, floatTarget2, sinkTarget2, buttonContainer2);
        }

        if (Spawner3.Count > 0)
        {
            int r3 = UnityEngine.Random.Range(0, Spawner3.Count);
            GameObject obj3 = Instantiate(Spawner3[r3], spawnPoint3.position, Quaternion.identity);
            AssignTargets(obj3, floatTarget3, sinkTarget3, buttonContainer3);
        }
    }

    void AssignTargets(GameObject spawned, Transform floatT, Transform sinkT, GameObject container)
    {
        if (spawned == null) return;

        ObjectInfo info = spawned.GetComponent<ObjectInfo>();
        if(info != null)
        {
            info.floatTarget = floatT;
            info.sinkTarget = sinkT;
            info.buttonsContainer = container;

            info.SetupForSpawn();
        }

        else
        {
            Debug.LogWarning("Spawned object does not have an ObjectInfo component.");
        }
    }
}
