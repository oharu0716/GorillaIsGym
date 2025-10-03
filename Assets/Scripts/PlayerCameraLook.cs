using UnityEngine;

public class PlayerCameraLook : MonoBehaviour
{
    //マウス感度。大きいほど少しのマウス移動で大きく視点が動く。
    public float sensiivity = 100f;
    //上下の視点（縦回転）の角度を保存するための変数。
    float xRotation = 0f;

    //ゲーム開始時にマウスカーソルを「画面中央に固定」し、「非表示」にする
    //FPSゲームでよくある「マウスカーソルが出ず、画面上で視点移動だけできる状態」にする処理
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //sensitivity を掛けて「感度調整」し、さらに Time.deltaTime を掛けて「フレームレートによらず同じ速度」になる
        //Input.GetAxis("Mouse X") … 水平方向のマウス移動量
        float mouseX = Input.GetAxis("Mouse X") * sensiivity * Time.deltaTime;
        //Input.GetAxis("Mouse Y") … 垂直方向のマウス移動量
        float mouseY = Input.GetAxis("Mouse Y") * sensiivity * Time.deltaTime;

        //xRotation にマウスの縦移動を反映。‐= なのは「マウスを上に動かすと視点が上がる」ようにするため
        xRotation -= mouseY;
        //Mathf.Clamp で -90°〜90° の範囲に制限。つまり真上と真下を向く限界を設定
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //カメラ（このスクリプトがついているオブジェクト）の回転を「上下方向だけ」に反映
        //Quaternion.Euler を使って角度を回転に変換。ここでは y軸 と z軸 はいじらず、上下の回転だけ反映
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //カメラの親オブジェクト（たとえば「Player」というキャラ本体）を左右に回転させている
        //これにより、上下はカメラ、左右はプレイヤー全体が回転するようになる
        transform.parent.Rotate(Vector3.up * mouseX);
    }
}
