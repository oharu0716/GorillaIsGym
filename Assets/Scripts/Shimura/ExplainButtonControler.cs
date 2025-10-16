using TMPro;
using UnityEngine;

public class ExplainButtonControler : MonoBehaviour
{
    int page = 1;
    public TextMeshProUGUI text;
    public ChangeScene scene;
    public GameObject button;

    void Start()
    {
        button.SetActive(false);
    }

    public void NextPage()
    {
        if(page == 1)
        {
            button.SetActive(true);
        }
        page++;
        ShowText(page);
    }

    public void PreviousPage()
    {
        if (page == 2)
        {
            button.SetActive(false);
        }
        page--;
        ShowText(page);
    }

    void ShowText(int page)
    {
        if (page == 1)
        {
            text.text = "『友情ゲージ』 をためて、進化させよう！\n『満腹ゲージ』が0になったり、『ストレスゲージ』が満タンになると死んでしまうぞ！\n『満腹ゲージ』はご飯を上げると回復！\n『ストレスゲージ』はお風呂や運動で回復！\nご飯によっても回復できるかも？";
            text.fontSize = 19.7f;
        }
        else if (page == 2)
        {
            text.text = "『狩りに行く（体力消費1）』\n少しお腹が減るけど、ご飯が手に入るぞ！でも、とてもストレスがたまっちゃいます・・・。\n『運動する（体力消費1）』\n少しお腹が減るけど、ストレス解消になるぞ！\n『ご飯をあげる』\nご飯によっていろいろな効果があるよ！\n『お風呂に入る』\n運動や狩りをしてお風呂に入ろう！ゆっくり休んでストレスも体力も回復！もっと仲良くなれるよ♪";
            text.fontSize = 14f;
        }
        else if (page == 3)
        {
            text.text = "ヒント\n時間経過でお腹が空いたりストレスがたまるので注意しよう！『狩りに行く』や『運動する』前は特に注意！帰ってきたら、そのまま死んでしまうかも・・・。\n見事、最終進化までいけばゲームクリア♪\n頑張ってね！";
            text.fontSize = 19f;
            text.alignment = TextAlignmentOptions.Center;
        }
        else if(page == 4)
        {
            scene.GotoMain();
        }
    }
}
