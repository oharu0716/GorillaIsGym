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
    int score = 10;
    public CharaController_kin chara;
    public GameObject blocks;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI stateText;
    public GameObject startButton;
    public GameObject EndPanel;
    public TextMeshProUGUI EndText;
    //リロードが可能かどうかを管理するフラグ
    private bool isReloadEnabled = false;
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
                //isReloadEnabled が true の場合のみクリックでリロード
                if (isReloadEnabled && Input.GetButtonDown("Fire1")) Reload();
                //if (Input.GetButtonDown("Fire1")) Reload();
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
        // 【追記】すべてのScrollObject_kinを無効にする
        ScrollObject_kin[] scrollObjects = FindObjectsByType<ScrollObject_kin>(FindObjectsSortMode.None);
        foreach (ScrollObject_kin so in scrollObjects) so.enabled = false;
        //Ready 状態ではリロードを無効にしておく
        isReloadEnabled = false;
        // ★ 修正: EndTextを初期状態で透明にする
        if (EndText != null)
        {
            // EndText.text = "『クリックで戻る』"; // EndTextはEditor側で設定されている前提
            Color c = EndText.color;
            c.a = 0f; // 透明にしておく
            EndText.color = c;
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
        // 【追記】すべてのScrollObject_kinを有効にする (スクロール開始)
        ScrollObject_kin[] scrollObjects = FindObjectsByType<ScrollObject_kin>(FindObjectsSortMode.None);
        foreach (ScrollObject_kin so in scrollObjects) so.enabled = true;

    }
    public void GameOver()
    {
        state = State.GameOver;
        //リロードを無効にし、コルーチンを開始
        isReloadEnabled = false;
        StartCoroutine(EnableReloadAfterDelay(2f));

        // 修正前のコード
        // ScrollObject[] scrollObjects = FindObjectsOfType<ScrollObject>();

        // 修正後のコード (並び替えをしない高速な方法)
        ScrollObject_kin[] scrollObjects = FindObjectsByType<ScrollObject_kin>(FindObjectsSortMode.None);

        foreach (ScrollObject_kin so in scrollObjects) so.enabled = false;
        stateText.gameObject.SetActive(true);
        stateText.text = "『" + score + "%』のストレス解消になりました！";
        if (EndPanel != null)
        {
            EndPanel.SetActive(true);
        }
    }
    IEnumerator EnableReloadAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // 待機後、リロードを可能にする
        isReloadEnabled = true;
        // ★ 修正: EndTextをフェードイン表示
        if (EndText != null)
        {
            StartCoroutine(FadeInText(EndText, 0.2f)); // 0.2秒かけてふわっと表示
        }
    }
    // TextMeshProUGUIをふわっとフェードインさせるコルーチン
    IEnumerator FadeInText(TextMeshProUGUI text, float duration)
    {
        float currentTime = 0f;
        Color color = text.color;

        // 初期アルファ値を0に設定（念のため）
        color.a = 0f;
        text.color = color;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            // 経過時間/指定時間 で 0 から 1 までの値を計算
            float alpha = Mathf.Clamp01(currentTime / duration);

            // アルファ値 (透明度) のみを更新
            color.a = alpha;
            text.color = color;

            yield return null; // 1フレーム待機
        }

        // 確実にアルファ値を 1 (不透明) に設定
        color.a = 1f;
        text.color = color;
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
        scoreText.text = "『" + score + "%』のストレス解消になりました！";
    }
}
