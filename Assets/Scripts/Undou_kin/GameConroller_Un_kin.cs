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
