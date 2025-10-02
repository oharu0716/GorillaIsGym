// TestKinManager_kin.cs
using UnityEngine;
using UnityEngine.SceneManagement;

// クラス名もファイル名に合わせて変更
public class TestKinManager_kin : MonoBehaviour
{
    void Start()
    {
        // Undou_kinシーンから戻ってきたかを確認
        // GameScore_kin を参照するように変更
        if (GameScore_kin.score > 0)
        {
            Debug.Log("スコア" + GameScore_kin.score);
            
            // スコアをリセット（次回の為）
            GameScore_kin.score = 0;
        }
    }

    // ボタンから呼び出すためのpublicなメソッド
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("Undou_kin");
    }
}