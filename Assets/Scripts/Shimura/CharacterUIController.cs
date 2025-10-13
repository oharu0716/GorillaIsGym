using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterUIController : MonoBehaviour
{
    //第一形態[0]、死亡[1]
    //第二形態[2]、死亡[3]
    //第三形態[4]
    [SerializeField] private GameObject[] images;
    [SerializeField] private float duration = 1.5f;


    public void Blend(int i)
    {
        Debug.Log("Blend内");
        // 最初はAが完全に見えてて、Bは透明
        images[i + 1].SetActive(true);
        // 同時にフェード（Aを下げつつBを上げる）
        images[i].GetComponent<Image>().DOFade(0f, duration);
        images[i + 1].GetComponent<Image>().DOFade(1f, duration)
        .OnComplete(() =>
        {
            images[i].SetActive(false);
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
