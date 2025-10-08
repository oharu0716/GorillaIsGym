using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEditor;
using System.Collections;

public class GaugeUIController : MonoBehaviour
{
    public Image friendlinessGauge;
    public Image manpukuGauge;
    public Image stressGauge;

    float timer = 0;

    PlayerStatus ps;
    public HeartUIManager heart;

    public float duration = 0.5f; // アニメーション時間

    void Start()
    {
        ps = PlayerStatus.instance;

        // 前の状態をそのまま表示（アニメーションなし）
        friendlinessGauge.fillAmount = ps.prev_friendliness / 100f;
        manpukuGauge.fillAmount = ps.prev_manpuku / 100f;
        stressGauge.fillAmount = ps.prev_stress / 100f;
        heart.ShowPrevHearts(ps.prev_hp);

        DOVirtual.DelayedCall(1.7f, () => UpdateAllGauges());
    }

    void UpdateAllGauges()
    {
        friendlinessGauge.DOFillAmount(ps.friendliness / 100f, duration);
        manpukuGauge.DOFillAmount(ps.manpuku / 100f, duration);
        stressGauge.DOFillAmount(ps.stress / 100f, duration);
        heart.UpdateLife(ps.hp);
    }

    void Update()
    {

        timer += Time.deltaTime; // 経過時間を加算

        if (timer >= 1f) // 1秒経過したら
        {
            timer = 0f; // タイマーリセット
            ps.DecreaseManpukuPerSec();
            manpukuGauge.DOFillAmount(ps.manpuku / 100f, duration);
            ps.IncreaseStressPerSec();
            stressGauge.DOFillAmount(ps.stress / 100f, duration);
        }
    }
}
