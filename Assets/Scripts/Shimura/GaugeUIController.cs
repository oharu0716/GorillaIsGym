using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEditor;
using System.Collections;
using UnityEngine.TextCore.Text;
using TMPro;
using UnityEngine.Rendering;

public class GaugeUIController : MonoBehaviour
{
    //第一形態[0]、死亡[1]
    //第二形態[2]、死亡[3]
    //第三形態[4]
    [SerializeField] private GameObject[] images;
    [SerializeField] private float blend_duration = 1.5f;
    [SerializeField] private GameObject popUp;
    [SerializeField] private GameObject UIs;
    [SerializeField] private GameObject restartButton;

    //三つのゲージ
    public Image friendlinessGauge;
    public Image manpukuGauge;
    public Image stressGauge;


    float timer = 0;
    public float duration = 0.5f; // アニメーション時間

    PlayerStatus ps;
    public HeartUIManager heart;
    public EvolutionManager evolutionManager;
    AudioManager am;

    public AudioClip gaugeSound;


    void Start()
    {
        am = AudioManager.Instance; 
        ps = PlayerStatus.instance;
        ps.RefreshUI(); // ← ここで ui を再取得

        // 前の状態をそのまま表示（アニメーションなし）
        if (ps.prev_friendliness < 100)
        {
            friendlinessGauge.fillAmount = ps.prev_friendliness / 100f;
            images[0].SetActive(true);
        }
        else if (ps.prev_friendliness < 200)
        {
            friendlinessGauge.fillAmount = (ps.prev_friendliness - 100) / 100f;
            images[2].SetActive(true);
        }

        manpukuGauge.fillAmount = ps.prev_manpuku / 100f;
        stressGauge.fillAmount = ps.prev_stress / 100f;
        heart.ShowPrevHearts(ps.prev_hp);

        DOVirtual.DelayedCall(1.7f, () => UpdateAllGauges());
    }

    void UpdateAllGauges()
    {
        am.PlaySE(gaugeSound);
        heart.UpdateLife(ps.hp);
        if (ps.prev_friendliness < 100)
        {
            Debug.Log("友情アニメーション");
            friendlinessGauge.DOFillAmount(ps.friendliness / 100f, duration);
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
                ps.isEffect = true;
                ps.isEvolution1 = true;
                evolutionManager.Evolution();
            }
            else if (ps.friendliness >= 200 && ps.isEvolution2 == false)
            {
                ps.isEffect = true;
                ps.isEvolution2 = true;
                evolutionManager.Evolution();
            }
        }); ;
    }

    void Update()
    {

        timer += Time.deltaTime; // 経過時間を加算

        if (timer >= 1f) // 1秒経過したら
        {
            if (ps.isEffect == false)
            {
                timer = 0f; // タイマーリセット
                ps.DecreaseManpukuPerSec();
                manpukuGauge.DOFillAmount(ps.manpuku / 100f, duration);
                ps.IncreaseStressPerSec();
                stressGauge.DOFillAmount(ps.stress / 100f, duration);
            }
        }
    }

    public void Blend(int i)
    {
        foreach (var img in UIs.GetComponentsInChildren<Image>())
        {
            img.DOFade(0f, blend_duration);
        }
        foreach (var txt in UIs.GetComponentsInChildren<TextMeshProUGUI>())
        {
            txt.DOFade(0f, blend_duration);
        }


        Debug.Log("Blend内");
        // 最初はAが完全に見えてて、Bは透明
        images[i + 1].SetActive(true);
        // 同時にフェード（Aを下げつつBを上げる）
        images[i].GetComponent<Image>().DOFade(0f, blend_duration);
        images[i + 1].GetComponent<Image>().DOFade(1f, blend_duration)
        .OnComplete(() =>
        {
            images[i].SetActive(false);
            UIs.SetActive(false);
            popUp.SetActive(true);
            popUp.GetComponentInChildren<TextMeshProUGUI>().text = "たまポンは死んでしまった・・・・";
            Invoke("Restart", 2f);
        }); ;

    }

    public void BlendEvolution(int i)
    {
        Debug.Log("BlendEvo内");
        // 最初はAが完全に見えてて、Bは透明
        images[i + 2].SetActive(true);
        // 同時にフェード（Aを下げつつBを上げる）
        images[i].GetComponent<Image>().DOFade(0f, blend_duration);
        images[i + 2].GetComponent<Image>().DOFade(1f, blend_duration)
        .OnComplete(() =>
        {
            images[i].SetActive(false);
        }); ;

    }

    public void Restart()
    {
        restartButton.SetActive(true);
    }
}
