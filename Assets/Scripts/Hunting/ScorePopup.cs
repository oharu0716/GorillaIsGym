using UnityEngine;
using TMPro;

public class ScorePopup : MonoBehaviour
{
    public float moveSpeed = 1f; //上に上がる速さ
    public float lifeTime = 1f;  //消えるまでの時間
    public float fadeSpeed = 2f; //フェードアウト速度
    private TextMeshProUGUI textMesh;
    private Color startColor;
    private Transform cam;
    
    void Start()
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        startColor = textMesh.color;
        cam = Camera.main.transform;

        //自動的に破棄
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        //カメラ（プレイヤーの方向を向く）
        transform.LookAt(transform.position + cam.forward);

        //上にふわっと移動
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        //徐々にフェードアウト
        startColor.a -= fadeSpeed * Time.deltaTime;
        textMesh.color = startColor;
    }

    //スコアを表示するための関数
    public void SetScoreText(int score)
    {
        textMesh.text = $"+{score}";
    }
}
