using UnityEngine;
using TMPro;

public class ScoreDisplay_kin : MonoBehaviour
{
    // ★ Unity Editorで割り当てる、スコア表示用のTextコンポーネント
    public TextMeshProUGUI scoreText;

    // ★ シーン内のGameController_kinへの参照
    private GameController_kin gameController;

    void Start()
    {
        // シーン内のGameController_kinインスタンスを探して取得する
        ScoreDisplay_kin score = Object.FindFirstObjectByType<ScoreDisplay_kin>();

        if (scoreText == null)
        {
            Debug.LogError("Score Text is not assigned in the Inspector.");
            // このスクリプトがアタッチされているオブジェクトのTMProUGUIを取得しようと試みる
            scoreText = GetComponent<TextMeshProUGUI>();
        }
    }

    void Update()
    {
        if (gameController != null && scoreText != null)
        {
            //kin: publicプロパティ CurrentScore を使ってスコアを取得　現状、無理やり表示してます。
            scoreText.text = "現在のスコア:" + (gameController.CurrentScore-10);
        }
    }
}
