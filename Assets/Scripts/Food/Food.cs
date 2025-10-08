using UnityEngine;

public class Food
{
    string food_name = "リンゴ";
    int up_friendliness_amount;
    int up_manpuku_amount;
    int up_hp_amount;
    int num;

    public Food(string name, int friendliness, int manpuku, int hp)
    {
        food_name = name;
        up_friendliness_amount = friendliness;
        up_manpuku_amount = manpuku;
        up_hp_amount = hp;
        this.num = 0;
    }


    // プロパティ（短く）
    public string Name
    {
        get => food_name;
        set => food_name = value;
    }

    public int Friend  // friendliness
    {
        get => up_friendliness_amount;
        set => up_friendliness_amount = value;
    }

    public int Manpuku  // manpuku
    {
        get => up_manpuku_amount;
        set => up_manpuku_amount = value;
    }

    public int Hp  // HP
    {
        get => up_hp_amount;
        set => up_hp_amount = value;
    }

    public int Num  // num
    {
        get => num;
        set => num = value;
    }
}


