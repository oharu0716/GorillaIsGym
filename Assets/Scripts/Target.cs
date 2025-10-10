using TMPro;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float lifeTime = 3f;
    public GameObject hitEffectPrefab; // エフェクトのプレハブを指定
    public GameObject scorePopupPrefab; 

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void Hit(int score, Vector3 position)
    {
        // Debug.Log(score+"ポジション"+position);
        // if (hitEffectPrefab != null)
        // {
        //     // エフェクトをターゲット位置に生成
        //     Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);

        //     //オブジェクトの位置にスコアを表示
        //     TextMeshProUGUI scorePopup = Instantiate(
        //         scoreText,
        //         position,
        //         Quaternion.identity
        //     );
        //     Debug.Log(scorePopup);

        //     scorePopup.text = score.ToString();
        // }

        // Destroy(gameObject);

        if (hitEffectPrefab != null)
        Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);

    if (scorePopupPrefab != null)
    {
        // Canvasを出現位置に生成
        GameObject popup = Instantiate(scorePopupPrefab, position + Vector3.up * 0.5f, Quaternion.identity);

        // TextMeshProUGUI を探してスコアを表示
        var text = popup.GetComponentInChildren<TextMeshProUGUI>();
        text.text = $"+{score}";
    }

    Destroy(gameObject);
    }
}



