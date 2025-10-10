using UnityEngine;

// ファイル名は「CharaController_kin.cs」にしてください
public class CharaController_kin : MonoBehaviour
{
    // --- public変数（UnityエディタのInspectorウィンドウで調整可能）---
    [Header("ジャンプの設定")]
    public float jumpForce = 15f; // ジャンプの初期パワー
    public float jumpCutMultiplier = 0.5f; // ボタンを離した時にジャンプを弱める割合

    [Header("参照するオブジェクト")]
    public GameObject sprite; // キャラクターのスプライト
    // --------------------------------------------------------------------

    private Rigidbody2D rb2d;
    private Animator animator;
    private bool isDead;
    private bool isGrounded; // 地面に接地しているか

    public bool IsDead()
    {
        return isDead;
    }

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = sprite.GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) return;

        // 地面にいて、ジャンプボタンが押された瞬間の処理
        if (Input.GetButtonDown("Fire1") && isGrounded)
        {
            // 上向きに力を加える
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpForce);
            isGrounded = false; // ジャンプしたので接地判定をOFFに
        }

        // ジャンプボタンが離された瞬間の処理
        if (Input.GetButtonUp("Fire1"))
        {
            // キャラがまだ上昇中（Y軸の速度がプラス）の場合
            if (rb2d.linearVelocity.y > 0)
            {
                // 上昇の勢いを減衰させる（短いジャンプになる）
                rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, rb2d.linearVelocity.y * jumpCutMultiplier);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;
        
        // 衝突した相手のタグが "underGrounds" だったら
        if (collision.gameObject.CompareTag("underGrounds"))
        {
            // 接地フラグをtrueにする
            isGrounded = true; 
        }
        else // "underGrounds" 以外のオブジェクトに衝突した場合
        {
            Camera.main.SendMessage("Clash");
            isDead = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // 離れた相手のタグが "underGrounds" だったら
        if (collision.gameObject.CompareTag("underGrounds"))
        {
            // 接地フラグをfalseにする
            isGrounded = false; 
        }
    }

    public void SetSteerActive(bool active)
    {
        rb2d.bodyType = active ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;
    }
}