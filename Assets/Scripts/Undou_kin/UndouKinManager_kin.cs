// UndouKinManager_kin.cs
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// クラス名もファイル名に合わせて変更
public class UndouKinManager_kin : MonoBehaviour
{
    // 20秒後に戻る
    public float returnTime = 100.0f;

    void Start()
    {
        // コルーチンを開始
        StartCoroutine(ReturnToTestKinScene());
    }

    IEnumerator ReturnToTestKinScene()
    {
        // 指定した秒数だけ待機
        yield return new WaitForSeconds(returnTime);

        // シーンに戻る前にスコアを設定
        // GameScore_kin を参照するように変更
        GameScore_kin.score = 100;

        // Meinシーンに移動
        SceneManager.LoadScene("Main");
    }
}