using Unity.Mathematics;
using UnityEngine;

public class SimpleCameraLook : MonoBehaviour
{
    //マウスを動かしたときの視点移動の速さ 大きいほど敏感に動く
    public float sensitivity = 200f;
    //縦方向の回転量
    float xRotation = 0f;
    //横方向の回転量
    float yRotation = 0f;

    void Start()
    {
        //マウスカーソルを画面中央に固定して非表示にする
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //感度（sensitivity）と時間補正(Time.deltaTime)を掛けて動きを滑らかに
        //マウスの左右移動量を取得
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        //マウスの上下移動量を取得  
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        //左右移動した分だけカメラを回す
        yRotation += mouseX;
        //上下移動 マウスを上に動かしたら視点は下に向くので符号を逆にしている
        xRotation -= mouseY;
        //上下の回転を制限　真上から真下まで これをしないと
        //マウスを動かしすぎたときにカメラの挙動がおかしくなる
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //実際にカメラにその回転を適用
        //オイラー角を使って回転を指定
        //X軸回転(上下の見上げ/見下ろし)
        //Y軸回転（左右の見回し）
        //Z軸回転(横に傾く)は常に0固定
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
