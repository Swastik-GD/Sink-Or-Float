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

    void Start()
    {
        sinkButton = buttonsContainer.transform.Find("Sink").GetComponent<Button>();
        floatButton = buttonsContainer.transform.Find("Float").GetComponent<Button>();

        if (buttonsContainer != null)
        {
            buttonsContainer.SetActive(false);
        }
    }


}
