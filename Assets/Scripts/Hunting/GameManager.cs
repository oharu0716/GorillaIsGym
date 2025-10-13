using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //InspectorでCanvas内の各Panelをドラッグして設定します
    [Header("Panels")]
    //ゲーム開始前の説明画面（タイトル）
    public GameObject startPanel;
    //プレイ中のUI（残り時間・スコアなど）
    public GameObject gameUIPanel;
    //プレイ終了後に結果を表示する画面
    public GameObject resultPanel;

    [Header("UI Elements")]
    //残り時間を表示
    public TextMeshProUGUI timeText;
    //最終スコアを表示
    public TextMeshProUGUI resultScoreText;
    //獲得アイテム数を表示
    public TextMeshProUGUI itemText;

    //1プレイの制限時間（秒単位）Inspectorで変更可能
    [Header("Game Settings")]
    public float gameTime = 30f;

    //ターゲット生成スクリプト（TargetSpawner.cs）への参照 
    // GameManagerがターゲット生成の開始／停止を指示します。
    [Header("References")]
    public TargetSpawner targetSpawner;

    private float timer;//残り時間
    private bool isPlaying = false;//プレイ中かどうかのフラグ
    private int itemCount = 0;//取得したアイテム数

    void Start()
    {
        //初期状態 : スタートパネルだけ表示
        startPanel.SetActive(true);
        gameUIPanel.SetActive(false);
        resultPanel.SetActive(false);

        //ゲーム開始前はターゲット生成停止
        if (targetSpawner != null)
            targetSpawner.StopSpawning();
    }

    void Update()
    {
        if (!isPlaying)
        {
            //クリックされたら StartGame() を呼んでゲーム開始。
            if (Input.GetMouseButtonDown(0))
            {
                StartGame();
            }
            return;
        }

        //ゲーム中の時間減少処理
        timer -= Time.deltaTime;
        //少数切り上げで整数表示（29.8→30）
        timeText.text = $"TIME: {Mathf.Ceil(timer)}";

        if (timer <= 0)
        {
            //残り時間が0になったら EndGame() を呼ぶ
            EndGame();
        }
    }

    void StartGame()
    {
        //タイトルパネルを非表示、ゲームUIを表示。
        startPanel.SetActive(false);
        gameUIPanel.SetActive(true);
        resultPanel.SetActive(false);

        //タイマーとスコアをリセット。
        timer = gameTime;
        ScoreManager.score = 0;
        //プレイ中フラグをONにする
        isPlaying = true;

        //ターゲット生成を開始
        if (targetSpawner != null)
            targetSpawner.StartSpawning();
    }
    
    void EndGame()
    {
        isPlaying = false;
        gameUIPanel.SetActive(false);
        resultPanel.SetActive(true);

        if (targetSpawner != null)
            targetSpawner.StopSpawning();

        //スコアとアイテム数を表示
        resultScoreText.text = $"SCORE: {ScoreManager.score}";
        itemText.text = $"獲得アイテム:{itemCount}個";
    }
}
