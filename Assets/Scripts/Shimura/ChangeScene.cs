using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    PlayerStatus ps;
    public HeartUIManager heart;

    //効果音
    public AudioClip buttonclick;

    //AudioManager取得
    AudioManager am;

    void Start()
    {
        ps = PlayerStatus.instance;
        am = AudioManager.Instance;
    }

    public void LoadScene(string sceneName)
    {
        ps.SaveCurrentAsPrevious();
        SceneManager.LoadScene(sceneName);
    }

    public void GotoMain()
    {
        SceneManager.LoadScene("Main");
    }


    public void GotoHunt()
    {
        am.PlaySE(buttonclick);
        ps.SaveCurrentAsPrevious(); //前の値を記録
        ps.DecreaseHp();
        ps.IncreaseShowerPoint();
        heart.UpdateLife(ps.hp);
        SceneManager.LoadScene("Hunting");
    }

    public void GotoExcercise()
    {
        am.PlaySE(buttonclick);
        ps.SaveCurrentAsPrevious();
        ps.DecreaseHp();
        ps.IncreaseShowerPoint();
        heart.UpdateLife(ps.hp);
        SceneManager.LoadScene("Excercise");
    }

    public void GotoHatch()
    {
        Destroy(PlayerStatus.instance.gameObject);
        SceneManager.LoadScene("Hatch");
    }
}
