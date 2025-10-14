using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Animator))]
public class EggClicker : MonoBehaviour
{
    public Animator animator;
    public ParticleSystem effectObject;       // エフェクトのプレハブ or Image
    public GameObject eggObject;          // 卵オブジェクト
    public GameObject characterObject;    // 生まれるキャラ

    public ChangeScene scene;
    public HatchManager hatch;

    void Start()
    {
        // Animator取得
        animator = eggObject.GetComponent<Animator>();

        // Image の透明部分はクリックを無効化（アルファ閾値を設定）
        var image = eggObject.gameObject.GetComponent<Image>();
        image.alphaHitTestMinimumThreshold = 0.5f;

        // Button にクリックイベント登録
        var button = eggObject.GetComponentInChildren<Button>();
        button.onClick.AddListener(OnClickEgg);
    }

    void OnClickEgg()
    {
        // AnimatorにTriggerを送る（Shakeなど）
        animator.SetTrigger("Shake");

        //HatchManagerのSetUIStateでポップアップを消してもらう
        hatch.SetUIState(HatchManager.UIState.Hatch);

        // 生まれる演出を数秒後に開始
        StartCoroutine(HatchSequence());
    }

    IEnumerator HatchSequence()
    {
        yield return new WaitForSeconds(4.7f); // カタカタ揺れ終わる時間

        // エフェクト表示（アニメ or パーティクル再生）
        effectObject.Play();

        // 卵を非表示にする
        eggObject.SetActive(false);

        // 数秒後にキャラ出現
        Debug.Log("キャラ出現");
        characterObject.SetActive(true);

        //HatchManagerのSetUIStateでポップアップを消してもらう
        yield return new WaitForSeconds(5f);
        hatch.SetUIState(HatchManager.UIState.PopUp);

    }
}