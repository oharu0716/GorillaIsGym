using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class EvolutionManager : MonoBehaviour
{
    [SerializeField] private Image whiteImage; // Canvas全体に置いた白Image
    [SerializeField] private GameObject UIs;
    [SerializeField] private GameObject popUp;
    public CharacterUIController characterUIController;
    public ChangeScene scene;
    PlayerStatus ps;

    void Start()
    {
        ps = PlayerStatus.instance;
    }

    public void Evolution(int friendliness)
    {
        UIs.SetActive(false);
        StartCoroutine(WaitAndDoSomething(friendliness));
    }

    public void PlayWhiteOut(int friendliness)
    {
        float scaleDuration = 0.2f;
        float fadeDuration = 3f;
        // 初期状態
        whiteImage.transform.localScale = Vector3.zero;
        whiteImage.color = new Color(1, 1, 1, 0);
        whiteImage.gameObject.SetActive(true);

        // 1. 中心からすばやく四角を広げる
        whiteImage.transform.DOScale(3f, scaleDuration).SetEase(Ease.OutCubic);

        // 2. ゆっくりホワイトアウト
        whiteImage.DOFade(1f, fadeDuration).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            //キャラクターを差し替える
            if (friendliness >= 100)
            {
                characterUIController.BlendEvolution(0);
            }
            else if (friendliness >= 200)
            {
                characterUIController.BlendEvolution(2);
            }

            // 3. ゆっくり透明に戻す
            whiteImage.DOFade(0f, fadeDuration).SetEase(Ease.InCubic)
                      .OnComplete(() => whiteImage.gameObject.SetActive(false));
        });
    }

    IEnumerator WaitAndDoSomething(int friendliness)
    {
        // 指定秒数待つ
        yield return new WaitForSeconds(2f);
        popUp.SetActive(true);

        yield return new WaitForSeconds(2f);
        PlayWhiteOut(friendliness);

        yield return new WaitForSeconds(6f);
        popUp.GetComponentInChildren<TextMeshProUGUI>().text = "たまポンが進化しました！";

        yield return new WaitForSeconds(3f);
        if (ps.isEvolution2 == true)
        {
            popUp.GetComponentInChildren<TextMeshProUGUI>().text = "おめでとうございます！ゲームクリアです！";
        }
        else
        {
            popUp.SetActive(false);
            UIs.SetActive(true);
        }

    }

}
