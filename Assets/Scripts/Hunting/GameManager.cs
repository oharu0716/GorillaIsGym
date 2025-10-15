using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject startPanel;//スタート画面
    public GameObject gameUIPanel;//プレイ中に表示するタイマー
    public GameObject resultPanel;//ゲーム終了後の結果画面

    [Header("UI Elements")]
    public TextMeshProUGUI timeText;//残り時間
    public TextMeshProUGUI resultScoreText;//最終スコア
    public TextMeshProUGUI resultRankText;  // ← 評価を表示
    public TextMeshProUGUI resultMessageText; // ← メッセージを表示
    public TextMeshProUGUI resultItemText; // ← 獲得アイテムを表示
    public TextMeshProUGUI rareItemText; // ← レアアイテム結果

    [Header("Game Settings")]
    public float gameTime = 30f;//1playの制限時間

    //ターゲットを出すスクリプトへの参照
    //ゲーム中だけスクリプトを出す
    [Header("References")]
    public TargetSpawner targetSpawner;
    public SimpleCameraLook cameraLook;
    public Transform cameraDefaultTransform;

    private float timer;//残り時間を計算
    private bool isPlaying = false;//現在がゲーム中か
    private bool gotRareItem = false;//レアアイテムをとったか

    PlayerStatus ps;
    bool uketukenai = false;



    void Start()
    {
        ps = PlayerStatus.instance;
        //パネルの表示の制御
        startPanel.SetActive(true);
        gameUIPanel.SetActive(false);
        resultPanel.SetActive(false);

        //タイトル表示中はターゲットを出現しないように
        if (targetSpawner != null)
            targetSpawner.StopSpawning();

        //初期位置にカメラを戻して固定
        ResetCamera();
        if (cameraLook != null) cameraLook.enabled = false;
    }

    void Update()
    {
        if (!isPlaying)
        {
            //クリックでスタートゲームを呼び出し
            if (Input.GetMouseButtonDown(0)&& uketukenai==false)
            {
                StartGame();
            }
            return;
        }

        //毎フレームtimerを減らす
        timer -= Time.deltaTime;
        //残り時間をUIに表示
        timeText.text = $"TIME: {Mathf.Ceil(timer)}";

        //時間が0になったらEndGameで終了
        if (timer <= 0)
        {
            EndGame();
        }
    }

    void StartGame()
    {
        startPanel.SetActive(false);
        gameUIPanel.SetActive(true);
        resultPanel.SetActive(false);

        //タイマーとスコアを初期化
        timer = gameTime;
        ScoreManager.score = 0;
        gotRareItem = false;

        //ゲーム状態にする
        isPlaying = true;

        //カメラ操作を有効化
        if (cameraLook != null) cameraLook.enabled = true;

        //ターゲットの生成を開始
        if (targetSpawner != null)
            targetSpawner.StartSpawning();
    }

    //ほかのスクリプトから呼び出される
    //特定のターゲットを撃った時に呼び出すとレアアイテムのフラグが立つ
    public void GetRareItem()
    {
        gotRareItem = true;
        Debug.Log("🌟 レアアイテムを取得！");
    }

    void EndGame()
    {
        uketukenai = true;
        Invoke("GotoMain", 4f);
        isPlaying = false;
        gameUIPanel.SetActive(false);
        resultPanel.SetActive(true);

        //ターゲットの出現を停止
        if (targetSpawner != null)
            targetSpawner.StopSpawning();

        //カメラを固定に戻す
        ResetCamera();
        if (cameraLook != null) cameraLook.enabled = false;

        //マウスカーソルを表示・ロック解除
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        int score = ScoreManager.score;
        resultScoreText.text = $"SCORE: {score}";

        // ▼ 評価判定 ▼
        string rank = "";
        string message = "";
        string items = "";

        if (score < 30000)
        {
            rank = "C";
            message = "次こそは頑張ろう！";
            items = "獲得アイテム：りんご、さかな、チーズ";
        }
        else if (score < 60000)
        {
            rank = "B";
            message = "たくさんとれたね！";
            items = "獲得アイテム：りんごx2、さかな、チーズ、骨付き肉";
        }
        else
        {
            rank = "A";
            message = "素晴らしい！あなたは狩りマスター！";
            items = "獲得アイテム：りんご、さかな、チーズx2、骨付き肉x2";
        }

        resultRankText.text = $"評価：{rank}";
        resultMessageText.text = message;
        resultItemText.text = items;

        // 🍖レアアイテム結果
        if (rareItemText != null)
        {
            if (gotRareItem)
                rareItemText.text = "ファミチキをゲット！";
            else
                rareItemText.text = "レアアイテムは見つからなかった…";
        }

        ps.AddFood(score, gotRareItem);

    }

    void ResetCamera()
    {
        //カメラの位置と角度をリセット
        if (cameraDefaultTransform != null && cameraLook != null)
        {
            cameraLook.transform.SetPositionAndRotation(
                cameraDefaultTransform.position,
                cameraDefaultTransform.rotation
            );
        }
    }
    
    void GotoMain()
    {
        SceneManager.LoadScene("Main");
    }
}
