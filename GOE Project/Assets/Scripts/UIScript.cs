using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{

    [SerializeField] GameObject Title;
    [SerializeField] GameObject Menu;

    private RectTransform TitleRt;
    private RectTransform MenuRt;

    void Start()
    {
        TitleRt = Title.GetComponent<RectTransform>();
        MenuRt = Menu.GetComponent<RectTransform>();
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
        SceneManager.LoadScene("Main Game");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
