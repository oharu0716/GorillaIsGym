using Unity.VisualScripting;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public HeartUIManager heartUIManager;

    //各ゲージの初期値
    public int friendliness = 0;
    public int manpuku = 100;
    public int stress = 0;
    public int hp = 5;

    //増減の調整用の値
    public int friendlinessIncreaseAmount; //おふろで友情度を上げる値
    public int manpukuDecreaseAmount; //運動と狩りで満腹度を減らす値
    public int manpukuDecreasePerSec; //１秒ごとに満腹度を減らす値
    public int stressIncreaseAmount; //狩りでストレスが上がる値
    public int stressDecreaseAmount; //ご飯とお風呂でストレスを下げる値
    public int hpIncreaseAmount; //お風呂でHPを増やす値
    public int hpDecreaseAmount; //狩りと運動でHPを減らす値


    //各ゲージのMAX
    const int max_friendliness = 300;
    const int max_manpuku = 100;
    const int max_stress = 100;
    const int max_hp = 5;

    private static PlayerStatus instance;

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
    }

    public void IncreaseFriendliness(int amount, int tag)
    {
        //ご飯から呼び出すときはtag==1で呼び出す。
        // tag == 0ならお風呂からだから固定値で加算。
        if (tag == 1)
        {
            friendliness += amount;
            if (amount >= 100)
            {
                //第1進化を伝える処理
            }
            else if (amount >= 200)
            {
                //第2進化を伝える処理
            }
        }
        else if (tag == 0)
        {
            friendliness += friendlinessIncreaseAmount;
        }

        Debug.Log($"友情度UP!現在の友情度 {friendliness}");
    }

    public void IncreaseManpuku(int amount)
    {
        //ご飯でのみご飯によってamout分増加
        manpuku = Mathf.Min(max_manpuku, manpuku + amount);
        Debug.Log($"満腹度UP!現在の満腹度 {manpuku}");
    }

    public void DecreaseManpuku()
    {
        //狩りと運動で固定値減少
        manpuku = Mathf.Max(0, manpuku - manpukuDecreaseAmount);
        Debug.Log($"満腹度UP!現在の満腹度 {manpuku}");

        if (manpuku <= 0)
        {
            //死亡処理
        }
    }

    public void IncreaseStress()
    {
        //狩りでのみストレス固定値増加
        stress = Mathf.Min(max_hp, hp + stressIncreaseAmount);
        Debug.Log($"ストレスUP! 現在のストレス度 {stress}");

        if (stress >= 100)
        {
            //死亡処理
        }
    }

    public void DecreaseStress(int amount, int tag)
    {
        //ご飯とお風呂は固定値(tag == 0)。
        //運動ではスコア次第のamount減少(tag == 1)。
        if (tag == 0)
        {
            stress = Mathf.Max(0, stress - stressDecreaseAmount);
        }
        else if (tag == 1)
        {
            stress = Mathf.Max(0, stress - amount);
        }
        Debug.Log($"ストレス回復！ 現在のストレス: {stress}");
    }

    public void IncreaseHp(int amount, int tag)
    {
        //お風呂なら固定値回復。(tag == 0)
        //ご飯ならamountにより回復。(tag == 1)
        if (tag == 0)
        {
            hp = Mathf.Min(max_hp, hp + hpIncreaseAmount);
        }
        else if (tag == 1)
        {
            hp = Mathf.Min(max_hp, hp + amount);
        }
        Debug.Log($"HP回復！ 現在のHP: {hp}");
    }

    public void DecreaseHp()
    {
        //狩りと運動で固定値減少
        hp = Mathf.Max(0, hp - hpDecreaseAmount);

        if (hp <= 0)
        {
            //死亡処理
        }

    }

    void Update()
    {
        //テスト用の処理
        if (Input.GetMouseButtonDown(0))
        {
            hp--;
            heartUIManager.UpdateLife(hp);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            hp++;
            heartUIManager.UpdateLife(hp);
        }
    }




}