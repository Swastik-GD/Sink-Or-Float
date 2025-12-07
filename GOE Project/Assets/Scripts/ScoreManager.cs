using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("Score Value")]
    [SerializeField] int correcrValue = 10;
    [SerializeField] int wrongValue = 5;
    [SerializeField] TextMeshProUGUI scoreText;
    public int finalScore = 0;

    void Update()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + finalScore.ToString();
        }
    }

    public void CorrectAnswer()
    {
        finalScore += correcrValue;

    }

    public void WrongAnswer()
    {
        finalScore -= wrongValue;
    }
}
