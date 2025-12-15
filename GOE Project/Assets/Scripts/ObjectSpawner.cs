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

    [SerializeField] GameObject Tub;

    private GameObject obj1, obj2, obj3;
    private int r1, r2, r3;

    public int hasAnswered = 0;

    void Start()
    {
        LeanTween.moveY(Tub, -6.7f, 0.5f);
        buttonContainer1.SetActive(false);
        buttonContainer2.SetActive(false);
        buttonContainer3.SetActive(false);
        StartCoroutine(SpawnObjects());
    }

     IEnumerator SpawnObjects()
     {
        if (Spawner1.Count > 0)
        {
            r1 = UnityEngine.Random.Range(0, Spawner1.Count);
            obj1 = Instantiate(Spawner1[r1], spawnPoint1.position, Quaternion.identity);
            AssignTargets(obj1, floatTarget1, sinkTarget1, buttonContainer1);
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(WaitAndSpawn1());
     }

    IEnumerator WaitAndSpawn1()
    {
        if (Spawner2.Count > 0)
        {
            r2 = UnityEngine.Random.Range(0, Spawner2.Count);
            obj2 = Instantiate(Spawner2[r2], spawnPoint2.position, Quaternion.identity);
            AssignTargets(obj2, floatTarget2, sinkTarget2, buttonContainer2);
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(WaitAndSpawn2());
    }

    IEnumerator WaitAndSpawn2()
    {
        if (Spawner3.Count > 0)
        {
            r3 = UnityEngine.Random.Range(0, Spawner3.Count);
            obj3 = Instantiate(Spawner3[r3], spawnPoint3.position, Quaternion.identity);
            AssignTargets(obj3, floatTarget3, sinkTarget3, buttonContainer3);
        }
        yield return new WaitForSeconds(0.5f);
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

    public void NotifyAnswered()
    {
        hasAnswered++;
        if(hasAnswered == 3)
        {
            LeanTween.scale(obj1, new Vector3(), 0.5f).setEaseInBack();
            LeanTween.scale(obj2, new Vector3(), 0.5f).setEaseInBack();
            LeanTween.scale(obj3, new Vector3(), 0.5f).setEaseInBack().setOnComplete(DestroyObjects);
            hasAnswered = 0;
            // All objects answered, proceed to next step
            Debug.Log("All objects answered. Proceeding to next step.");
            // Implement next step logic here
        }
    }

    void DestroyObjects()
    {
        Destroy(obj1);
        Destroy(obj2);
        Destroy(obj3);
        Spawner1.RemoveAt(r1);
        Spawner2.RemoveAt(r2);
        Spawner3.RemoveAt(r3);
        StartCoroutine(SpawnObjects());
    }
}
