using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static int score = 0;
    public TextMeshProUGUI scoreText;
    public Color normalColor = Color.white;
    public Color flashColor = Color.yellow;
    public float flashTime = 0.2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScoreUI();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
        StartCoroutine(FlashScore());
    }

    // Update is called once per frame
    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"SCORE: {score}";
        }
    }

    System.Collections.IEnumerator FlashScore()
    {
        scoreText.color = flashColor;
        yield return new WaitForSeconds(flashTime);
        scoreText.color = normalColor;
    }
}
