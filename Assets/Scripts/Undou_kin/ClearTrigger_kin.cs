using UnityEngine;

public class ClearTrigger_kin : MonoBehaviour
{
    GameObject gameController;

    void Start()
    {
        // ゲーム開始時にGameControllerオブジェクトを見つけておく
        gameController = GameObject.FindWithTag("GameController");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // ★重要：トリガーから出たのが "Player" タグのオブジェクトか確認
        if (other.gameObject.CompareTag("Player"))
        {
            // gameControllerが正しく見つかっているか念のため確認
            if (gameController != null)
            {
                // スコアを増やす
                gameController.SendMessage("IncreaseScore");
            }
        }
    }
}