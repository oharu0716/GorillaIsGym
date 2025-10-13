using UnityEngine;

public class ScrollObject_kin : MonoBehaviour
{
    public float speed = 1.0f;
    public float startPosition;
    public float endPosition;
    public float accelerationRate = 0.1f; // 1秒あたりに増えるスピード
    private float elapsedTime = 0f;//経過時間の測定


    void Update()
    {
        elapsedTime += Time.deltaTime;//経過時間を更新
        // 現在のスピード = 初期スピード + (加速度 * 経過時間)
        float currentSpeed = speed + (accelerationRate * elapsedTime);

        //毎フレームxポジションを少しずつ移動させる
        //transform.Translate(-1 * speed * Time.deltaTime, 0, 0);
        transform.Translate(-1 * currentSpeed * Time.deltaTime, 0, 0);
        //スクロールが目標ポイントまで到達したかをチェック
        if (transform.position.x <= endPosition) ScrollEnd();
    }

    void ScrollEnd()
    {
        //通り過ぎた分を加味してポジションを再設定
        float diff = transform.position.x - endPosition;
        Vector3 restartPosition = transform.position;
        restartPosition.x = startPosition + diff;
        transform.position = restartPosition;
        //同じゲームオブジェクトにアタッチされているコンポーネントにメッセージを送る
        SendMessage("OnScrollEnd", SendMessageOptions.DontRequireReceiver);
    }
}
