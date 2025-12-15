using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{

    [SerializeField] GameObject Title;
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject SettingsPanel;

    private RectTransform TitleRt;
    private RectTransform MenuRt;
    private RectTransform SettingsPanelRt;  

    void Start()
    {
        TitleRt = Title.GetComponent<RectTransform>();
        MenuRt = Menu.GetComponent<RectTransform>();
        SettingsPanelRt = SettingsPanel.GetComponent<RectTransform>();

        SettingsPanel.SetActive(false);
        SettingsPanel.transform.localScale = new Vector3(0, 0, 0);
        TitleRt.anchoredPosition = new Vector3(0, 0, 0);
        MenuRt.anchoredPosition = new Vector3(1330, 0, 0);
        Title.transform.localScale = new Vector3(0, 0, 0);

        LeanTween.scale(Title, new Vector3(4f, 4f, 4f), 1.5f).setEaseOutElastic().setOnComplete(MenuAtRight);
    }

    void MenuAtRight()
    {
        LeanTween.moveX(MenuRt, 500, 2f).setEaseOutExpo();
        LeanTween.scale(Title, new Vector3(2f, 2f, 2f), 2f).setEaseOutExpo();
        LeanTween.moveX(TitleRt, -400, 2f).setEaseOutExpo().setOnComplete(ShakeTitle);
    }

    void ShakeTitle()
    {
        LeanTween.rotate(Title, new Vector3(0, 0, 10), 3f).setEaseShake().setLoopPingPong();
    }

    public void Play()
    {
        LeanTween.moveX(MenuRt, 1330, 1f).setEaseInExpo();
        LeanTween.moveX(TitleRt, -1330, 1f).setEaseInExpo().setOnComplete(()=>
        {
            SceneManager.LoadScene("Main Game");
        });
    }

    public void OpenSettings()
    {
        LeanTween.moveX(MenuRt, 1330, 1f).setEaseInExpo();
        LeanTween.moveX(TitleRt, -1330, 1f).setEaseInExpo().setOnComplete(()=>
        {
            SettingsPanel.SetActive(true);
            LeanTween.scale(SettingsPanel, new Vector3(1f, 1f, 1f), 0.5f).setEaseOutBack();
        });
    }

    public void CloseSettings()
    {
        LeanTween.scale(SettingsPanel, new Vector3(0, 0, 0), 0.5f).setEaseInBack().setOnComplete(()=>
        {
            SettingsPanel.SetActive(false);
            LeanTween.moveX(MenuRt, 500, 1f).setEaseOutExpo();
            LeanTween.moveX(TitleRt, -400, 1f).setEaseOutExpo();
        });
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
