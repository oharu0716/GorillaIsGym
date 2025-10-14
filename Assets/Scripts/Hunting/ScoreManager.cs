using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static int score = 0;           //現在のスコア
    public TextMeshProUGUI scoreText;      //UI上に表示するテキスト
    public Color normalColor = Color.white;//通常時の文字色
    public Color flashColor = Color.yellow;//スコアが増えたときに一瞬光らせる色
    public float flashTime = 0.2f;         //光らせる時間（秒）
    
    //ゲーム開始時に現在のスコアをUIに反映
    void Start()
    {
        UpdateScoreUI();
    }

    //スコアを加算した後、UIを更新する
    //コルーチンを呼んでスコア表示を一瞬光らせる
    public void AddScore(int points)
    {
        score += points;          //スコアを加算
        UpdateScoreUI();          //UI更新
        StartCoroutine(FlashScore()); //一瞬色を変える演出
    }

    //スコアをテキストに反映
    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            //$"SCORE: {score}"はC＃の文字列補間
            scoreText.text = $"SCORE: {score}";
        }
    }

    //スコア加算後に色を変えて点数が入ったことを強調
    System.Collections.IEnumerator FlashScore()
    {
        scoreText.color = flashColor;              //黄色にする
        yield return new WaitForSeconds(flashTime);//flashTime(0.2)秒待つ
        scoreText.color = normalColor;             //元の色に戻す
    }
}
