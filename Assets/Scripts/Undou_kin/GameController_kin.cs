using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
public class GameController_kin : MonoBehaviour
{
    //ゲームスタート
    enum State
    {
        Ready,
        Play,
        GameOver
    }
    State state;
    int score;
    public CharaController_kin chara;
    public GameObject blocks;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI stateText;
    public GameObject startButton;
    public GameObject EndPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Ready();
    }
    void LateUpdate()
    {
        switch (state)
        {
            case State.Ready:
                //if (Input.GetButtonDown("Fire1")) GameStart();
                break;
            case State.Play:
                if (chara.IsClash()) GameOver();
                break;
            case State.GameOver:
                if (Input.GetButtonDown("Fire1")) Reload();
                break;
        }

    }
    void Ready()
    {
        state = State.Ready;
        chara.SetSteerActive(false);
        blocks.SetActive(false);

        scoreText.text = "Score:" + 0;
        stateText.gameObject.SetActive(true);
        //stateText.text = "Ready";
        if (EndPanel != null)
        {
            EndPanel.SetActive(false);
        }
    }
    public void GameStart()
    {
        state = State.Play;
        chara.SetSteerActive(true);
        blocks.SetActive(true);
        // chara.Flap();

        stateText.gameObject.SetActive(false);
        stateText.text = "";
        // 【追記】スタートボタンを非表示にする
        if (startButton != null)
        {
            startButton.SetActive(false);
        }
    }
    public void GameOver()
    {
        state = State.GameOver;

        // 修正前のコード
        // ScrollObject[] scrollObjects = FindObjectsOfType<ScrollObject>();

        // 修正後のコード (並び替えをしない高速な方法)
        ScrollObject_kin[] scrollObjects = FindObjectsByType<ScrollObject_kin>(FindObjectsSortMode.None);

        foreach (ScrollObject_kin so in scrollObjects) so.enabled = false;
        stateText.gameObject.SetActive(true);
        stateText.text = "『"+score+"%』のストレス解消になりました！";
        if (EndPanel != null)
        {
            EndPanel.SetActive(true);
        }
    }
    void Reload()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Main");
    }
    public void IncreaseScore()
    {
        score++;
        Debug.Log(score);
        scoreText.text =  "『"+score+"%』のストレス解消になりました！";
    }
}
