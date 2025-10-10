using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    PlayerStatus ps;
    public HeartUIManager heart;

    void Start()
    {
        ps = PlayerStatus.instance;
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
        ps.SaveCurrentAsPrevious(); //前の値を記録
        ps.DecreaseHp();
        heart.UpdateLife(ps.hp);
        SceneManager.LoadScene("Hunting");
    }

    public void GotoExcercise()
    {
        ps.SaveCurrentAsPrevious();
        ps.DecreaseHp();
        heart.UpdateLife(ps.hp);
        SceneManager.LoadScene("Excercise");
    }
}
