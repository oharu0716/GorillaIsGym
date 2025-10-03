using UnityEngine;
using System.Collections;

public class CharaController_kin : MonoBehaviour
{
    const int MinLane = -1;
    const int MaxLane = 1;
    const float LaneWidth = 0.2f;
    const float StunDuration = 0.5f;
    const float MoveDuration = 0.01f;

    CharacterController controller;
    Animator animator;

    Vector3 moveDirection = Vector3.zero;
    float recoverTime = 0.0f;
    private Coroutine moveCoroutine;

    public float gravity;
    public float speedZ;
    public float speedJump;
    public float accelerationZ;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    bool IsStun()
    {
        return recoverTime > 0.0f;
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        // 左キーを「押した瞬間」
        if (Input.GetKeyDown("left"))
        {
            MoveToLane(MinLane); // 左のレーン(-1)へ移動開始
        }
        // 右キーを「押した瞬間」
        else if (Input.GetKeyDown("right"))
        {
            MoveToLane(MaxLane); // 右のレーン(1)へ移動開始
        }

        // 左キーか右キーを「離した瞬間」
        if (Input.GetKeyUp("left") || Input.GetKeyUp("right"))
        {
            MoveToLane(0); // 真ん中のレーン(0)へ移動開始
        }

        // スペースキーを「押した瞬間」
        if (Input.GetKeyDown("space"))
        {
            Jump();
        }
        // ----- ★キー入力の変更はここまで -----


        if (IsStun())
        {
            //動きを止め気絶状態からの復帰カウントを進める。
            moveDirection.x = 0.0f;
            moveDirection.z = 0.0f;
            recoverTime -= Time.deltaTime;
        }
        else
        {
            // 前に進む力の計算
            float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
            moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);

            // 横移動はコルーチンが担当。moveDirection.x は基本的に0らしい
            moveDirection.x = 0;
        }
        //重力分の力を毎フレーム追加
        moveDirection.y -= gravity * Time.deltaTime;
        //移動実行
        Vector3 globalDirection = transform.TransformDirection(moveDirection);
        controller.Move(globalDirection * Time.deltaTime);
        //移動後接地してたらY方向の速度はリセットする
        if (controller.isGrounded) moveDirection.y = 0;
        //速度が０以上なら走っているフラグをtrueにする
        animator.SetBool("run", moveDirection.z > 0.0f);
    }

    // ★レーン移動を開始させる命令
    public void MoveToLane(int targetLane)
    {
        if (IsStun()) return;

        // もしすでに移動中だったら、古い動きは止める
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        // 新しい移動のコルーチンを開始する！
        moveCoroutine = StartCoroutine(MoveLaneCoroutine(targetLane));
    }

    // ★ピッタリ0.1秒で移動するためのコルーチンの本体！
    IEnumerator MoveLaneCoroutine(int targetLane)
    {
        float startPositionX = transform.position.x; // 今いる場所
        float endPositionX = targetLane * LaneWidth;  // 行きたい場所
        float elapsedTime = 0f; // 経過時間タイマー

        // 経過時間が MoveDuration(0.1秒) になるまで、この中を繰り返す
        while (elapsedTime < MoveDuration)
        {
            // 経過時間を更新
            elapsedTime += Time.deltaTime;
            // 進捗度を計算 (0.0 ～ 1.0 の値になる)
            float progress = elapsedTime / MoveDuration;

            // Lerp（ラープ）という魔法で、スタートとゴールの間の今の位置を計算する
            float newX = Mathf.Lerp(startPositionX, endPositionX, progress);

            // 計算した位置まで、キャラクターを動かすための「移動量」を計算
            Vector3 move = new Vector3(newX - transform.position.x, 0, 0);

            // キャラクターを実際に動かす！
            controller.Move(move);

            // ここで一旦休憩して、次のフレームまで待つ
            yield return null;
        }

        // ループが終わったら、誤差をなくすためにピッタリの位置に合わせる
        Vector3 finalMove = new Vector3(endPositionX - transform.position.x, 0, 0);
        controller.Move(finalMove);

        // 移動が終わったので、覚えていたコルーチンを空にする
        moveCoroutine = null;
    }
    public void Jump()
    {
        if (IsStun()) return;
        if (controller.isGrounded)
        {
            //ジャンプトリガーを設定
            moveDirection.y = speedJump;
            animator.SetTrigger("jump");
        }

    }
    //CharavterControllerに衝突判定が生じた時の処理
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (IsStun()) return;
        if (hit.gameObject.CompareTag("Robo"))
        {
            recoverTime = StunDuration;
            //ダメージトリガーを設定
            animator.SetTrigger("damage");
            //ヒットしたオブジェクトは消去
            Destroy(hit.gameObject);
        }

    }
}
