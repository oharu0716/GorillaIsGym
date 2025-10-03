using UnityEngine;
using UnityEngine.SceneManagement; // ★シーンを操作するための道具箱を持ってくるおまじない

public class RetryManager_kin : MonoBehaviour
{
    // ★ボタンが押されたときに呼び出すための命令
    public void RetryGame()
    {
        // 今いるシーンの名前を調べて、もう一度そのシーンを読み込む
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}