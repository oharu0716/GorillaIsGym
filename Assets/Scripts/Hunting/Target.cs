using UnityEngine;

public class Target : MonoBehaviour
{
    //ターゲットが自動的に消えるまでの時間
    public float lifeTime = 3f;
    public GameObject hitEffectPrefab;
    public GameObject scorePopupPrefab;

    AudioManager am;
    public AudioClip hitsound;
    void Start()
    {
        am = AudioManager.Instance;
        Destroy(gameObject, lifeTime);
    }

    public void Hit(int score, Vector3 position)
    {
        if (hitEffectPrefab != null)
            Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);

        if (scorePopupPrefab != null)
        {
            GameObject popup = Instantiate(scorePopupPrefab, position, Quaternion.identity);
            var text = popup.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            text.text = $"+{score}";
        }

        // 🍖もし5000点（肉）だったらレアアイテム取得判定！
        if (CompareTag("5000"))
        {
            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null)
            {
                gm.GetRareItem(); // ← レアアイテム取得を報告
            }
        }

        am.PlaySE(hitsound,3f);
        Destroy(gameObject);
    }
}




