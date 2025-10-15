// GohanSelector.cs
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;
using System.Collections;

public class FoodListManager : MonoBehaviour
{
    public GameObject buttonPrefab;     // プレハブ
    public Transform buttonParent;      // 配置先（例：ScrollViewのContent）
    public PlayerStatus playerStatus;
    public const int down_stress_amount = 10;

    //効果音
    public AudioClip eatSound;
    public AudioClip nomikomi;

    //食べ物の絵
    public GameObject[] foodSprites;

    AudioManager am;

    private List<Food> foodList;
    List<GameObject> buttons = new List<GameObject>();

    void Start()
    {
        am = AudioManager.Instance;
        foodList = PlayerStatus.instance.food_list;

        ShowFood();
    }

    void ShowFood()
    {
        foreach (GameObject button in buttons)
        {
            Destroy(button);
        }
        foreach (Food gohan in foodList)
        {
            if (gohan.Num != 0)
            {
                GameObject buttonObj = Instantiate(buttonPrefab, buttonParent);
                buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = gohan.Name + " " + gohan.Num + "個";

                // ボタンにクリックイベントを追加（ラムダ式）
                buttonObj.GetComponent<Button>().onClick.AddListener(() => OnFoodSelected(gohan));

                buttons.Add(buttonObj);
            }
        }
    }

    void OnFoodSelected(Food gohan)
    {
        StartCoroutine(ShowFoodAnimation(gohan)); //ご飯を食べるアニメーションのコルーチン発動
        am.ButtonClick(); //ボタンクリック音を鳴らす
        am.StopSE(); //音が重ならないように今なっている効果音を止める
        am.PlaySE(eatSound, 3f); //そしゃく音
        Invoke(nameof(StopSE), 1.5f); //eatSoundが長すぎるので止めて、飲み込みを流す。
        Debug.Log(gohan.Name);
        //フィールド値更新
        PlayerStatus.instance.IncreaseFriendliness(gohan.Friend, 1);
        PlayerStatus.instance.IncreaseManpuku(gohan.Manpuku);
        PlayerStatus.instance.IncreaseHp(gohan.Hp, 1);
        PlayerStatus.instance.DecreaseStress(down_stress_amount, 1);
        //ご飯を一つ減らす
        gohan.Num -= 1;
        //ボタン表示しなおし
        ShowFood();
    }

    void StopSE()
    {
        am.StopSE();
        am.PlaySE(nomikomi, 5f);
    }

    IEnumerator ShowFoodAnimation(Food gohan)
    {
        GameObject selectFood = gohan.Name switch
        {
            "リンゴ" => foodSprites[0],
            "おさかな" => foodSprites[1],
            "チーズ" => foodSprites[2],
            "ほねつき肉" => foodSprites[3],
            "ファミチキ" => foodSprites[4],
            _ => foodSprites[0]
        };

       
        selectFood.SetActive(true);

        yield return new WaitForSeconds(2.0f);
        selectFood.SetActive(false);
    }
}

