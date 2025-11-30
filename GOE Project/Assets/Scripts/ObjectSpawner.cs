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
        SpawnObjects();
    }

    void Update()
    {

    }

    void SpawnObjects()
    {
        if (Spawner1.Count > 0)
        {
            int r1 = UnityEngine.Random.Range(0, Spawner1.Count);
            GameObject obj1 = Instantiate(Spawner1[r1], spawnPoint1.position, Quaternion.identity);

            ObjectInfo info1 = obj1.GetComponent<ObjectInfo>();
            info1.floatTarget = floatTarget1;
            info1.sinkTarget = sinkTarget1;
            info1.buttonsContainer = buttonContainer1;
        }

        if (Spawner2.Count > 0)
        {
            int r2 = UnityEngine.Random.Range(0, Spawner2.Count);
            GameObject obj2 = Instantiate(Spawner2[r2], spawnPoint2.position, Quaternion.identity);

            ObjectInfo info2 = obj2.GetComponent<ObjectInfo>();
            info2.floatTarget = floatTarget2;
            info2.sinkTarget = sinkTarget2;
            info2.buttonsContainer = buttonContainer2;
        }

        if (Spawner3.Count > 0)
        {
            int r3 = UnityEngine.Random.Range(0, Spawner3.Count);
            GameObject obj3 = Instantiate(Spawner3[r3], spawnPoint3.position, Quaternion.identity);

            ObjectInfo info3 = obj3.GetComponent<ObjectInfo>();
            info3.floatTarget = floatTarget3;
            info3.sinkTarget = sinkTarget3;
            info3.buttonsContainer = buttonContainer3;
        }
    }
}
