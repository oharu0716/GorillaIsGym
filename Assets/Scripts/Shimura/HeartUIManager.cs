using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HeartUIManager : MonoBehaviour
{
    public GameObject[] hearts;


    //遷移先のシーンで増えた分だけアニメーションで表示
    public void UpdateLife(int life)
    {
      
        for (int i = 0; i < hearts.Length; i++)
        {
            RectTransform rt = hearts[i].GetComponent<RectTransform>();
            rt.DOKill(true);

            if (i < life)
            {
                // ハートを表示（増えたとき）
                if (!hearts[i].activeSelf)
                {
                    hearts[i].SetActive(true);
                    rt.localScale = Vector3.one * 0.002f; // 小さくしておく
                    rt.DOScale(0.28f, 0.4f).SetEase(Ease.OutBack);

                    
                }
                // 既に表示されてるものは何もしない
            }
            else
            {
                // ハートを非表示（減ったとき）
                if (hearts[i].activeSelf)
                {
                    var target = hearts[i]; // ←ここで固定！
                    rt.DOScale(0.002f, 0.4f)
                      .SetEase(Ease.InBack)
                      .OnComplete(() => target.SetActive(false)); // ←iではなくtargetを使う
                }
                // 既に非表示なら何もしない
            }
        }
    }

    //アニメーションなしの表示用
    public void ShowPrevHearts(int life)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            bool active = i < life;
            hearts[i].SetActive(active);
            hearts[i].GetComponent<RectTransform>().localScale = active ? Vector3.one * 0.28f : Vector3.one * 0.002f;
        }
    }

}
