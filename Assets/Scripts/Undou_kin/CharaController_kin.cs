using UnityEngine;

public class CharaController_kin : MonoBehaviour
{
    // プレイヤーのジャンプ状態を定義
    private enum JumpStatus { GROUND, UP, DOWN }

    [Header("ジャンプの設定（自前物理演算）")]
    public float initialJumpSpeed = 25.0f; // ジャンプの初速
    public float customGravity = 120.0f;   // 自前の重力の強さ
    public float minJumpDuration = 0.05f;  // 最低でもジャンプする時間

    [Header("参照するオブジェクト")]
    public GameObject sprite;

    // 内部で使う変数
    private Rigidbody2D rb2d;
    private Animator animator;
    private bool isDead;

    private JumpStatus playerStatus = JumpStatus.GROUND; // 現在のジャンプ状態
    private float jumpTimer = 0f;    // ジャンプしてからの経過時間
    private bool jumpKeyPressed = false; // ジャンプキーが押されているか
    private bool lockKeyInput = false; // 着地直後のキー入力受付をロックするフラグ

    public bool IsDead()
    {
        return isDead;
    }

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = sprite.GetComponent<Animator>();

        // ★★★ 最重要 ★★★
        // 自前で重力計算をするため、Unityの重力を無効化します
        rb2d.gravityScale = 0;
    }

    void Update()
    {
        if (isDead) return;

        // キー入力の受付
        if (Input.GetButton("Fire1")) // スペースキーや左クリックなど
        {
            // キーロック中でなければ、キーが押されていると判断
            jumpKeyPressed = !lockKeyInput;
        }
        else
        {
            // キーが離されたら、押されていない状態にし、キーロックも解除
            jumpKeyPressed = false;
            lockKeyInput = false;
        }
    }

    void FixedUpdate()
    {
        if (isDead || rb2d.bodyType != RigidbodyType2D.Dynamic)
        {
            // 物理演算が不要な状態なら何もしない
            if(rb2d.bodyType == RigidbodyType2D.Kinematic) rb2d.linearVelocity = Vector2.zero;
            return;
        }

        // 現在の速度をベースに、Y軸方向の速度を計算していく
        Vector2 newVelocity = rb2d.linearVelocity;
        newVelocity.x = 0; // このキャラクターは横移動しない

        // 状態に応じて処理を切り替え
        switch (playerStatus)
        {
            // 【状態：GROUND】地面にいるとき
            case JumpStatus.GROUND:
                if (jumpKeyPressed)
                {
                    // ジャンプキーが押されたら、状態を「上昇」へ
                    playerStatus = JumpStatus.UP;
                    jumpTimer = 0f; // タイマーリセット
                }
                else
                {
                    newVelocity.y = 0; // 地面にいるときはピッタリ止まる
                }
                break;

            // 【状態：UP】ジャンプして上昇しているとき
            case JumpStatus.UP:
                jumpTimer += Time.fixedDeltaTime;

                // キーを押し続けているか、最低ジャンプ時間に達していない間は上昇
                if (jumpKeyPressed || jumpTimer < minJumpDuration)
                {
                    // 速度 = 初速 - (重力 * 時間^2)
                    newVelocity.y = initialJumpSpeed - (customGravity * Mathf.Pow(jumpTimer, 2));
                }
                else
                {
                    // キーを離したら、即座に上昇を打ち切る
                    newVelocity.y = 0;
                }

                // Y速度がマイナスに転じたら、状態を「落下」へ
                if (newVelocity.y <= 0)
                {
                    playerStatus = JumpStatus.DOWN;
                    jumpTimer = 0f; // 落下用にタイマーリセット
                }
                break;

            // 【状態：DOWN】落下しているとき
            case JumpStatus.DOWN:
                jumpTimer += Time.fixedDeltaTime;
                // 速度 = -(重力 * 時間^2) でどんどん加速して落ちる
                newVelocity.y = -(customGravity * Mathf.Pow(jumpTimer, 2));
                break;
        }

        // 計算した速度をRigidbodyに適用
        rb2d.linearVelocity = newVelocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        // ぶつかった相手のタグが "underGrounds" だったら
        if (collision.gameObject.CompareTag("underGrounds"))
        {
            // 落下中に地面に接触した場合のみ、「接地」状態に戻す
            // これにより、天井や壁に頭をぶつけても接地状態になるのを防ぐ
            if (playerStatus == JumpStatus.DOWN)
            {
                playerStatus = JumpStatus.GROUND;
                jumpTimer = 0f;
                lockKeyInput = true; // 着地した瞬間はキー入力をロックし、暴発を防ぐ
            }
        }
        else // "underGrounds" 以外のオブジェクト（岩など）に衝突した場合
        {
            Camera.main.SendMessage("Clash");
            isDead = true;
        }
    }

    public void SetSteerActive(bool active)
    {
        rb2d.bodyType = active ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;
    }
}