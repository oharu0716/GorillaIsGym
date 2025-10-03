using UnityEngine;

public class Target : MonoBehaviour
{
    //ターゲットが自然消滅するまでの時間
    //Inspectorから自由に変更可能
    public float lifeTime = 3f;
    
    void Start()
    {
        //オブジェクトがlifeTime秒後の削除されるように設定
        Destroy(gameObject, lifeTime);
    }

    public void Hit()
    {
        Destroy(gameObject);
        Debug.Log("Hit!");
    }
}
