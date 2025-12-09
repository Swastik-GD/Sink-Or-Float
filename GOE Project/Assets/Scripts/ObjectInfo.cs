using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInfo : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] string objectName;
    [SerializeField] bool willFloat;

    [Header("Buttons")]
    [SerializeField] Button sinkButton;
    [SerializeField] Button floatButton;

    [Header("Targets")]
    public Transform sinkTarget;
    public Transform floatTarget;

    [Header("Buttons container")]
    public GameObject buttonsContainer;

    [Header("Movement")]
    [SerializeField] float moveDuration = 0.6f;

    bool answered = false;
    public ScoreManager scoreManager;
    public ObjectSpawner objectSpawner;

    public GameObject plusTen;
    public GameObject minusFive;

    void Start()
    {
        objectSpawner = FindAnyObjectByType<ObjectSpawner>();
        scoreManager = FindAnyObjectByType<ScoreManager>();
        transform.localScale = new Vector3();
        LeanTween.scale(gameObject, new Vector3(0.6f, 0.6f, 0.6f), 1f).setEaseOutBounce().setDelay(1f).setOnComplete(ButtonBounce);
    }
    public void SetupForSpawn()
    {
        sinkButton = buttonsContainer.transform.Find("Sink").GetComponent<Button>();
        floatButton = buttonsContainer.transform.Find("Float").GetComponent<Button>();

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

    void ButtonBounce()
    {
        buttonsContainer.SetActive(true);
        buttonsContainer.transform.localScale = new Vector3();
        LeanTween.scale(buttonsContainer, new Vector3(1f, 1f, 1f), 1f).setEaseOutQuint();
    }

    void OnAnswer(bool choseFloat)
    {
        if (answered)
        {
            //objectSpawner.NotifyAnswered();
            return;
        }

        answered = true;
        //objectSpawner.NotifyAnswered();

        // Hide buttons immediately
        if (buttonsContainer != null)
        {
            buttonsContainer.SetActive(false);
        }

        bool correct = (choseFloat == willFloat);
        Debug.Log(objectName + " -> " + (correct ? "Correct" : "Wrong"));
        if (correct)
        {
            GameObject clone = Instantiate(plusTen, transform.position, Quaternion.identity);
            clone.transform.localScale = new Vector3();
            LeanTween.scale(clone, new Vector3(0.5f, 0.5f, 0.5f), 0.8f).setEaseOutBack();
            LeanTween.move(clone, new Vector3(-7.1f, -2.6f, 0), 0.8f).setEaseLinear().setDelay(0.5f).setOnComplete(() =>
            {
                Destroy(clone);
            });
            scoreManager.CorrectAnswer();
        }
        else
        {
            GameObject clone = Instantiate(minusFive, transform.position, Quaternion.identity);
            clone.transform.localScale = new Vector3();
            LeanTween.scale(clone, new Vector3(0.4f, 0.4f, 0.4f), 0.8f).setEaseOutBack();
            LeanTween.move(clone, new Vector3(-7.1f, -2.6f, 0), 0.8f).setDelay(0.5f).setOnComplete(() =>
            {
                Destroy(clone);
            });
            scoreManager.WrongAnswer();
        }

        // Move to actual target determined by willFloat
        Transform target = willFloat ? floatTarget : sinkTarget;
        if (target != null)
        {
            StartCoroutine(MoveToTarget(target));
        }

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
        objectSpawner.NotifyAnswered();
    }
}
