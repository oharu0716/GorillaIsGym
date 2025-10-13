// GohanSelector.cs
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;

public class FoodListManager : MonoBehaviour
{
    public GameObject buttonPrefab;     // プレハブ
    public Transform buttonParent;      // 配置先（例：ScrollViewのContent）
    public PlayerStatus playerStatus;
    public const int down_stress_amount = 10;

    private List<Food> foodList;
    List<GameObject> buttons = new List<GameObject>();

    void Start()
    {
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
                buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = gohan.Name;

                // ボタンにクリックイベントを追加（ラムダ式）
                buttonObj.GetComponent<Button>().onClick.AddListener(() => OnFoodSelected(gohan));

                buttons.Add(buttonObj);
            }
        }
    }

    void OnFoodSelected(Food gohan)
    {
        Debug.Log(gohan.Name);
        PlayerStatus.instance.IncreaseFriendliness(gohan.Friend, 1);
        PlayerStatus.instance.IncreaseManpuku(gohan.Manpuku);
        PlayerStatus.instance.IncreaseHp(gohan.Hp, 1);
        PlayerStatus.instance.DecreaseStress(down_stress_amount, 1);
        gohan.Num -= 1;
        ShowFood();
    }
}

