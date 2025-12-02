using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInfo : MonoBehaviour
{
    [Header("Info")]
    public string objectName;
    public bool willFloat;

    [Header("Buttons")]
    public Button sinkButton;
    public Button floatButton;

    [Header("Targets")]
    public Transform sinkTarget;
    public Transform floatTarget;

    [Header("Buttons container")]
    public GameObject buttonsContainer;

    [Header("Movement")]
    public float moveDuration = 0.6f;

    bool answered = false;

    void Start()
    {
        sinkButton = buttonsContainer.transform.Find("Sink").GetComponent<Button>();
        floatButton = buttonsContainer.transform.Find("Float").GetComponent<Button>();

        buttonsContainer.SetActive(false);
    }

    public void SetupForSpawn()
    {
        // Show buttons
        if (buttonsContainer != null)
            buttonsContainer.SetActive(true);
        else
        {
            if (sinkButton != null) sinkButton.gameObject.SetActive(true);
            if (floatButton != null) floatButton.gameObject.SetActive(true);
        }

        // Ensure we have button references (try to find if still null)
        if (sinkButton == null && buttonsContainer != null)
        {
            Transform t = buttonsContainer.transform.Find("Sink");
            if (t != null) sinkButton = t.GetComponent<Button>();
        }
        if (floatButton == null && buttonsContainer != null)
        {
            Transform t = buttonsContainer.transform.Find("Float");
            if (t != null) floatButton = t.GetComponent<Button>();
        }

        // Hook up listeners (clear previous to avoid duplicate)
        if (sinkButton != null)
        {
            sinkButton.onClick.RemoveAllListeners();
            sinkButton.onClick.AddListener(() => OnAnswer(false)); // chose sink
        }
        else Debug.LogWarning($"ObjectInfo ({name}) - sinkButton missing.");

        if (floatButton != null)
        {
            floatButton.onClick.RemoveAllListeners();
            floatButton.onClick.AddListener(() => OnAnswer(true)); // chose float
        }
        else Debug.LogWarning($"ObjectInfo ({name}) - floatButton missing.");
    }

    void OnAnswer(bool choseFloat)
    {
        if (answered) return;
        answered = true;

        // Hide buttons immediately
        if (buttonsContainer != null)
            buttonsContainer.SetActive(false);
        else
        {
            if (sinkButton != null) sinkButton.gameObject.SetActive(false);
            if (floatButton != null) floatButton.gameObject.SetActive(false);
        }

        bool correct = (choseFloat == willFloat);
        Debug.Log(objectName + " -> " + (correct ? "Correct" : "Wrong"));

        // Move to actual target determined by willFloat
        Transform target = willFloat ? floatTarget : sinkTarget;
        if (target != null)
            StartCoroutine(MoveToTarget(target));
        else
            Debug.LogWarning("ObjectInfo: target not assigned for " + objectName);
    }

    IEnumerator MoveToTarget(Transform target)
    {
        Vector3 start = transform.position;
        Vector3 end = target.position;
        float t = 0f;

        while (t < moveDuration)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(start, end, t / moveDuration);
            yield return null;
        }

        transform.position = end;
    }
}
