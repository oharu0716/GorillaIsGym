using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class CharacterUIController : MonoBehaviour
{
    //第一形態[0]、死亡[1]
    //第二形態[2]、死亡[3]
    //第三形態[4]
    [SerializeField] private GameObject[] images;
    [SerializeField] private float duration = 1.5f;
    [SerializeField] private GameObject popUp;
    [SerializeField] private GameObject UIs;


    public void Blend(int i)
    {
        foreach (var img in UIs.GetComponentsInChildren<Image>())
        {
            img.DOFade(0f, duration);
        }
        foreach (var txt in UIs.GetComponentsInChildren<TextMeshProUGUI>())
        {
            txt.DOFade(0f, duration);
        }


        Debug.Log("Blend内");
        // 最初はAが完全に見えてて、Bは透明
        images[i + 1].SetActive(true);
        // 同時にフェード（Aを下げつつBを上げる）
        images[i].GetComponent<Image>().DOFade(0f, duration);
        images[i + 1].GetComponent<Image>().DOFade(1f, duration)
        .OnComplete(() =>
        {
            images[i].SetActive(false);
            UIs.SetActive(false);
            popUp.SetActive(true);
            popUp.GetComponentInChildren<TextMeshProUGUI>().text = "たまポンは死んでしまった・・・・";
        }); ;

    }

    public void BlendEvolution(int i)
    {
        Debug.Log("Blend内");
        // 最初はAが完全に見えてて、Bは透明
        images[i + 2].SetActive(true);
        // 同時にフェード（Aを下げつつBを上げる）
        images[i].GetComponent<Image>().DOFade(0f, duration);
        images[i + 2].GetComponent<Image>().DOFade(1f, duration)
        .OnComplete(() =>
        {
            images[i].SetActive(false);
        }); ;

    }
}
