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
    public EvolutionManager evolutionManager;

    float timer = 0;

    PlayerStatus ps;
    public HeartUIManager heart;

    public float duration = 0.5f; // アニメーション時間

    void Start()
    {
        ps = PlayerStatus.instance;

        // 前の状態をそのまま表示（アニメーションなし）
        if (ps.prev_friendliness < 100)
        {
            friendlinessGauge.fillAmount = ps.prev_friendliness / 100f;
        }
        else if (ps.prev_friendliness < 200)
        {
            friendlinessGauge.fillAmount = (ps.prev_friendliness - 100) / 100f;
        }

        manpukuGauge.fillAmount = ps.prev_manpuku / 100f;
        stressGauge.fillAmount = ps.prev_stress / 100f;
        heart.ShowPrevHearts(ps.prev_hp);

        DOVirtual.DelayedCall(1.7f, () => UpdateAllGauges());
    }

    void UpdateAllGauges()
    {
        heart.UpdateLife(ps.hp);
        if (ps.prev_friendliness < 100)
        {
            friendlinessGauge.fillAmount = ps.prev_friendliness / 100f;
        }
        else if (ps.prev_friendliness < 200)
        {
            friendlinessGauge.DOFillAmount((ps.friendliness - 100) / 100f, duration);
        }
        manpukuGauge.DOFillAmount(ps.manpuku / 100f, duration);
        stressGauge.DOFillAmount(ps.stress / 100f, duration)
        .OnComplete(() =>
        {
            if (ps.friendliness >= 100 && ps.isEvolution1 == false)
            {
                evolutionManager.Evolution(ps.friendliness);
                ps.isEvolution1 = true;
            }
            else if (ps.friendliness >= 200 && ps.isEvolution2 == false)
            {
                evolutionManager.Evolution(ps.friendliness);
                ps.isEvolution2 = true;
            }
        }); ;
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
