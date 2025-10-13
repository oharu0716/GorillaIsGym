using UnityEngine;

public class Block_kin : MonoBehaviour
{
    // ★変更: 変数名を役割に合わせて変更
    public float minX; // ランダムなX座標の最小値
    public float maxX; // ランダムなX座標の最大値
    public GameObject root;
    
    void Start()
    {
        // ★変更: メソッド名を変更
        ChangePosition();
    }

    // ★変更: メソッド名を役割に合わせて変更
    void ChangePosition()
    {
        // ★変更: X座標をランダムに決める
        float xPosition = Random.Range(minX, maxX);
        // ★変更: X座標にランダムな値を、Y座標は0に設定
        root.transform.localPosition = new Vector3(xPosition, 0.0f, 0.0f);
    }

    void OnScrollEnd()
    {
        // ★変更: メソッド名を変更
        ChangePosition();
    }
}