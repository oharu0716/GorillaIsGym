using UnityEngine;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class GameConroller_Un_kin : MonoBehaviour
{
    public CharaController_kin nejiko;
    public TextMeshProUGUI scoreText;

    void Update()
    {
        int score = CalcScore();
        scoreText.text = $"Score : {score}m";
        if (nejiko.Life() <= 0)
        {
            enabled = false;

            if (PlayerPrefs.GetInt("HighScore") < score)
            {
                PlayerPrefs.SetInt("HighScore", score);
            }
            Invoke("ReturnToTitle", 2.0f);
        }


    }
    int CalcScore()
    {
        return (int)nejiko.transform.position.z;
    }
    void ReturnToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
