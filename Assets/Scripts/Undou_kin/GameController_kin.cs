using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Rendering;
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
    //kin:固定値で１０％は減らす。
    int score = 10;
    // kin: scoreの値を外部から取得するためのpublicプロパティ
    public int CurrentScore
    {
        get { return score; }
    }
    public CharaController_kin chara;
    public GameObject blocks;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI stateText;
    public GameObject startButton;
    public GameObject EndPanel;
    //志村が書き足します
    public PlayerStatus ps;
    // kin:『クリックで戻る』のTextMeshProUGUI
    public TextMeshProUGUI EndText;
    // ★追加: スタートボタンのCanvasGroup（フェードイン用）
    private CanvasGroup startButtonCanvasGroup;

    // kin: リロードが可能かどうかを管理するフラグ
    private bool isReloadEnabled = false;

    //BGM関係
    AudioManager exercise_am;
    public AudioClip exerciseBGM_Start;
    public AudioClip exerciseBGM_End;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ps = PlayerStatus.instance;
        exercise_am = AudioManager.Instance;
        //kin: startButtonのCanvasGroupを取得
        if (startButton != null)
        {
            startButtonCanvasGroup = startButton.GetComponent<CanvasGroup>();
    
        }
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
                // kin: isReloadEnabled が true の場合のみクリックでリロード
                if (isReloadEnabled && Input.GetButtonDown("Fire1")) Reload();
                //正常動作後に消去→    if (Input.GetButtonDown("Fire1")) Reload();
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
        //BGM

        exercise_am.PlayBGM(exerciseBGM_Start);
        if (EndPanel != null)
        {
            EndPanel.SetActive(false);
        }
        //kin: すべてのScrollObject_kinを無効にする
        ScrollObject_kin[] scrollObjects = FindObjectsByType<ScrollObject_kin>(FindObjectsSortMode.None);
        foreach (ScrollObject_kin so in scrollObjects) so.enabled = false;

        // kin: Ready 状態ではリロードを無効にしておく
        isReloadEnabled = false;

        // kin: EndTextを初期状態で透明にする
        if (EndText != null)
        {
            Color c = EndText.color;
            c.a = 0f;
            EndText.color = c;
        }
        // kin: stateText と startButton のフェードイン初期化と開始
        if (stateText != null)
        {
            // stateTextのアルファ値を0に設定
            Color c = stateText.color;
            c.a = 0f;
            stateText.color = c;
        }

        if (startButtonCanvasGroup != null)
        {
            // startButtonの透明度を0に設定
            startButtonCanvasGroup.alpha = 0f;
        }

        // kin: フェードイン処理を開始
        StartCoroutine(ReadyFadeIn(0.5f));
    }
    // kin: Ready状態のUIをフェードインさせるコルーチン
    IEnumerator ReadyFadeIn(float duration)
    {
        float currentTime = 0f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(currentTime / duration);

            // stateText のフェードイン
            if (stateText != null)
            {
                Color c = stateText.color;
                c.a = alpha;
                stateText.color = c;
            }

            // startButton のフェードイン
            if (startButtonCanvasGroup != null)
            {
                startButtonCanvasGroup.alpha = alpha;
            }

            yield return null;
        }

        // 確実に不透明にする
        if (stateText != null)
        {
            Color c = stateText.color;
            c.a = 1f;
            stateText.color = c;
        }
        if (startButtonCanvasGroup != null)
        {
            startButtonCanvasGroup.alpha = 1f;
        }
    }
    public void GameStart()
    {
        // kin: ゲームスタート時にReadyフェードインコルーチンを停止する（もし実行中なら）
        StopCoroutine("ReadyFadeIn");
        state = State.Play;
        chara.SetSteerActive(true);
        blocks.SetActive(true);

        stateText.gameObject.SetActive(false);
        stateText.text = "";
        // スタートボタンを非表示にする
        if (startButton != null)
        {
            startButton.SetActive(false);
        }
        // kin: すべてのScrollObject_kinを有効にする (スクロール開始)
        ScrollObject_kin[] scrollObjects = FindObjectsByType<ScrollObject_kin>(FindObjectsSortMode.None);
        foreach (ScrollObject_kin so in scrollObjects) so.enabled = true;
    }
    public void GameOver()
    {
        state = State.GameOver;
        //BGM
        exercise_am.PlayBGM(exerciseBGM_End);
        //kin: リロードを無効にし、コルーチンを開始
        isReloadEnabled = false;
        StartCoroutine(EnableReloadAfterDelay(2f));

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
    // kin: 指定時間待機後、リロードを有効にするコルーチン
    IEnumerator EnableReloadAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // 待機後、リロードを可能にする
        isReloadEnabled = true;

        // kin: EndTextをフェードイン表示
        if (EndText != null)
        {
            StartCoroutine(FadeInText(EndText, 0.2f));
        }
    }

    // kin: TextMeshProUGUIをふわっとフェードインさせるコルーチン
    IEnumerator FadeInText(TextMeshProUGUI text, float duration)
    {
        float currentTime = 0f;
        Color color = text.color;

        color.a = 0f;
        text.color = color;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(currentTime / duration);

            color.a = alpha;
            text.color = color;

            yield return null;
        }

        color.a = 1f;
        text.color = color;
    }
    void Reload()
    {
        //志村が書き足しました。ステータス値変更処理
        ps.DecreaseStress(score, 1);
        ps.DecreaseManpuku();
        // kin: 安全にシーンをリロードするためのコルーチンを開始
        StopAllCoroutines();
        StartCoroutine(SafeReloadCoroutine());
    }
    // kin: 安全にシーンをリロードするためのコルーチン
    IEnumerator SafeReloadCoroutine()
    {
        yield return null;
        exercise_am.PlayBGM(exercise_am.mainBGM);
         SceneManager.LoadScene("Main");
    }
    public void IncreaseScore()
    {
        score++;
        Debug.Log(score);
        scoreText.text = "『" + score + "%』のストレス解消になりました！";
    }
}
