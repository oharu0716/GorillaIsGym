using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public GaugeUIController ui;


    //各ゲージの初期値
    public int friendliness = 0;
    public int manpuku = 100;
    public int stress = 0;
    public int hp = 0;
    public List<Food> food_list;

    bool isDeath = false;
    public bool isEvolution1;
    public bool isEvolution2;
    public bool isEffect;

    //各ゲージのMAX
    const int max_friendliness = 300;
    const int max_manpuku = 100;
    const int max_stress = 100;
    const int max_hp = 5;

    //増減の調整用の値
    public int friendlinessIncreaseAmount; //おふろで友情度を上げる値
    public int manpukuDecreaseAmount; //運動と狩りで満腹度を減らす値
    public int manpukuDecreasePerSec; //１秒ごとに満腹度を減らす値
    public int stressIncreaseAmount; //狩りでストレスが上がる値
    public int stressIncreasePerSec; //１秒ごとにストレスが上がる値
    public int stressDecreaseAmount; //ご飯とお風呂でストレスを下げる値
    public int hpIncreaseAmount; //お風呂でHPを増やす値
    public int hpDecreaseAmount; //狩りと運動でHPを減らす値
    public int showerPoint; //2pointたまったらお風呂にはいれる。シャワったら０になる

    // 前回の状態
    public int prev_friendliness;
    public int prev_manpuku;
    public int prev_stress;
    public int prev_hp;

    //シングルトン用
    public static PlayerStatus instance;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        food_list = new List<Food>();
        food_list.Add(new Apple());
        food_list.Add(new Fish());
        food_list.Add(new Cheese());
        food_list.Add(new Meet());
        food_list.Add(new FamiChick());

        //テスト用に一個ずつ食べ物追加
        food_list[0].Num++;
        food_list[1].Num++;
        food_list[2].Num++;
        food_list[3].Num++;
        food_list[4].Num++;
    }

    public void RefreshUI()
    {
        ui = FindFirstObjectByType<GaugeUIController>();
    }

    //シーン遷移前の値を保持
    public void SaveCurrentAsPrevious()
    {
        prev_friendliness = friendliness;
        prev_manpuku = manpuku;
        prev_stress = stress;
        prev_hp = hp;
    }


    public void AddFood(int score, bool Fami)
    {
        //スコアはまだ仮
        if (score < 100)
        {

        }
        else if (score < 200)
        {

        }
        else if (score < 300)
        {

        }

        if (Fami)
        {
            food_list[4].Num++; //ファミチキを一個追加
        }

    }

    public void IncreaseFriendliness(int amount, int tag)
    {
        //ご飯から呼び出すときはtag==1で呼び出す。
        // tag == 0ならお風呂からだから固定値で加算。
        if (tag == 1)
        {
            friendliness += amount;
            Debug.Log($"友情度UP!現在の友情度 {friendliness}" + "増えた値" + amount);
        }
        else if (tag == 0)
        {
            friendliness += friendlinessIncreaseAmount;
            Debug.Log($"おふろで友情度UP!現在の友情度 {friendliness}" + friendlinessIncreaseAmount);
        }
    }

    public void IncreaseManpuku(int amount)
    {
        //ご飯でのみご飯によってamout分増加
        manpuku = Mathf.Min(max_manpuku, manpuku + amount);
        Debug.Log($"満腹度UP!現在の満腹度 {manpuku}" + "増えた値" + amount);
    }

    public void DecreaseManpuku(int amount)
    {
        //狩りと運動で固定値減少
        manpuku = Mathf.Max(0, manpuku - amount);
        Debug.Log($"満腹度Down!現在の満腹度 {manpuku}" + "減った値" + amount);

        //死亡処理
        if (manpuku == 0 && isDeath == false)
        {
            isDeath = true;

            if (friendliness < 100)
            {
                Debug.Log("Blend呼ぶ");
                ui.Blend(0);
            }
            else if (friendliness < 200)
            {
                ui.Blend(2);
            }
        }
    }

    public void DecreaseManpukuPerSec()
    {
        manpuku = Mathf.Max(0, manpuku - manpukuDecreasePerSec);

        //死亡処理
        if (manpuku == 0 && isDeath == false)
        {
            isDeath = true;

            if (friendliness < 100)
            {
                Debug.Log("Blend呼ぶ");
                ui.Blend(0);
            }
            else if (friendliness < 200)
            {
                ui.Blend(2);
            }
        }
    }

    public void IncreaseStress()
    {
        //狩りでのみストレス固定値増加
        stress = Mathf.Min(max_stress, stress + stressIncreaseAmount);
        Debug.Log($"ストレスUP! 現在のストレス度 {stress}" + "増えた値" + stressIncreaseAmount);

        //死亡処理
        if (stress >= 100 && isDeath == false)
        {
            isDeath = true;

            if (friendliness < 100)
            {
                Debug.Log("Blend呼ぶ");
                ui.Blend(0);
            }
            else if (friendliness < 200)
            {
                ui.Blend(2);
            }
        }
    }

    public void IncreaseStressPerSec()
    {
        stress = Mathf.Min(max_stress, stress + stressIncreasePerSec);

        //死亡処理
        if (stress >= 100 && isDeath == false)
        {
            isDeath = true;

            if (friendliness < 100)
            {
                Debug.Log("Blend呼ぶ");
                ui.Blend(0);
            }
            else if (friendliness < 200)
            {
                ui.Blend(2);
            }
        }
    }

    public void DecreaseStress(int amount, int tag)
    {
        //ご飯とお風呂は固定値(tag == 0)。
        //運動ではスコア次第のamount減少(tag == 1)。
        if (tag == 0)
        {
            stress = Mathf.Max(0, stress - stressDecreaseAmount);
            Debug.Log($"ストレス回復！ 現在のストレス: {stress}" + "減った値" + stressDecreaseAmount);
        }
        else if (tag == 1)
        {
            stress = Mathf.Max(0, stress - amount);
            Debug.Log($"ストレス回復！ 現在のストレス: {stress}" + "減った値" + amount);
        }

    }

    public void IncreaseHp(int amount, int tag)
    {
        //お風呂なら固定値回復。(tag == 0)
        //ご飯ならamountにより回復。(tag == 1)
        if (tag == 0)
        {
            hp = Mathf.Min(max_hp, hp + hpIncreaseAmount);
            Debug.Log($"HP回復！ 現在のHP: {hp}" + "増えた値" + hpIncreaseAmount);
        }
        else if (tag == 1)
        {
            hp = Mathf.Min(max_hp, hp + amount);
            Debug.Log($"HP回復！ 現在のHP: {hp}" + "増えた値" + amount);
        }

    }

    public void DecreaseHp()
    {
        //狩りと運動で固定値減少
        hp = Mathf.Max(0, hp - hpDecreaseAmount);
        Debug.Log($"HP減少！ 現在のHP: {hp}" + "減った値" + hpDecreaseAmount);

        //死亡処理
        if (hp <= 0 && isDeath == false)
        {
            isDeath = true;

            if (friendliness < 100)
            {
                Debug.Log("Blend呼ぶ");
                ui.Blend(0);
            }
            else if (friendliness < 200)
            {
                ui.Blend(2);
            }
        }

    }

    public void IncreaseShowerPoint()
    {
        if (showerPoint < 2)
        {
            showerPoint++;
        }
    }
    
    public void DecreaseShowerPoint()
    {
        showerPoint = 0;
    }
}