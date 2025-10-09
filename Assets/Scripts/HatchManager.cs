using UnityEngine;
using UnityEngine.UI;

public class HatchManager : MonoBehaviour
{
    // 状態を表すEnum
    public enum UIState
    {
        Start,      // スタート画面
        ThisEgg,
        Hatch,      // 卵がカタカタ揺れてる
        PopUp,      // 「生まれました！」などのポップアップ
    }

    private UIState currentState;
    public Button startButton;

    // 画面ごとのUIオブジェクト（Inspectorからセット）
    public GameObject[] startUI;
    public GameObject[] thisEggUI;
    public GameObject hatchUI;
    public GameObject[] popUpUI;

    void Start()
    {
        // ボタンのonClickにイベントを登録
        //startButton.onClick.AddListener(() => SetStateThisEgg());
        SetUIState(UIState.Start);
    }

    public void SetStateThisEgg()
    {
        SetUIState(UIState.ThisEgg);
    }

    // 状態を切り替えるメソッド
    public void SetUIState(UIState newState)
    {
        currentState = newState;

        // // 全UIを非表示にしてから該当UIだけ表示
        // startUI[0].SetActive(false);
        // startUI[1].SetActive(false);
        // thisEggUI[0].SetActive(false);
        // thisEggUI[1].SetActive(false);
        // hatchUI.SetActive(false);
        // popUpUI[0].SetActive(false);
        // popUpUI[1].SetActive(false);

        switch (newState)
        {
            case UIState.Start:
                // 全UIを非表示にしてから該当UIだけ表示
                popUpUI[0].SetActive(false);
                popUpUI[1].SetActive(false);
                thisEggUI[0].SetActive(false);
                thisEggUI[1].SetActive(false);
                hatchUI.SetActive(false);
                startUI[0].SetActive(true);
                startUI[1].SetActive(true);
                Debug.Log("Start");
                break;
            case UIState.ThisEgg:
                startUI[0].SetActive(false);
                startUI[1].SetActive(false);
                thisEggUI[0].SetActive(true);
                thisEggUI[1].SetActive(true);
                Debug.Log("ThisEgg");
                break;
            case UIState.Hatch:
                thisEggUI[1].SetActive(false);
                Debug.Log("Hatch");
                break;
            case UIState.PopUp:
                popUpUI[0].SetActive(true);
                Debug.Log("PopUp");
                break;
        }
    }
}