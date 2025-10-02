using UnityEngine;

public class CharaController_kin : MonoBehaviour
{
    const int MinLane = -2;
    const int MaxLane = 2;
    const float LaneWidth = 1.0f;
    const int DefaultLife = 3;
    const float StunDuration = 0.5f;

    CharacterController controller;
    Animator animator;

    Vector3 moveDirection = Vector3.zero;
    int targetLane;
    int life = DefaultLife;
    float recoverTime = 0.0f;

    public float gravity;
    public float speedZ;
    public float speedX;

    public float speedJump;
    public float accelerationZ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int Life()
    {
        return life;
    }
    bool IsStun()
    {
        return recoverTime > 0.0f || life <= 0;
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //デバッグ用
        if (Input.GetKeyDown("left")) MoveToLeft();
        if (Input.GetKeyDown("right")) MoveToRight();
        if (Input.GetKeyDown("space")) Jump();

        if (IsStun())
        {
            //動きを止め気絶状態からの復帰カウントを進める。
            moveDirection.x = 0.0f;
            moveDirection.z = 0.0f;
            recoverTime -= Time.deltaTime;
        }
        else
        {

            //徐々に加速して、Z方向に常に前線
            float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
            moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);
            //X方向は目標のポジションまでの差分の割合で速度を計算
            float ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth;
            moveDirection.x = ratioX * speedX;
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
    //左のレーンに移動を開始
    public void MoveToLeft()
    {
        if (IsStun()) return;
        if (controller.isGrounded && targetLane > MinLane) targetLane--;

    }
    //右のレーンに移動を開始
    public void MoveToRight()
    {
        if (IsStun()) return;
        if (controller.isGrounded && targetLane < MaxLane) targetLane++;

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
            //ライフを減らして気絶状態に移行
            life--;
            recoverTime = StunDuration;
            //ダメージトリガーを設定
            animator.SetTrigger("damage");
            //ヒットしたオブジェクトは消去
            Destroy(hit.gameObject);
        }
        // 回復アイテムに衝突した場合の処理
        if (hit.gameObject.CompareTag("Recovery"))
        {
            // ライフが上限(DefaultLife)未満の場合のみ回復
            if (life < DefaultLife)
            {
                life++;
            }
            // ヒットした回復アイテムは消去
            Destroy(hit.gameObject);
        }
    }
}
