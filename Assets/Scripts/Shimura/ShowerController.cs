using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ShowerController : MonoBehaviour
{
    public static bool TookShower = false;
    PlayerStatus ps;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ps = PlayerStatus.instance;
        ps.DecreaseShowerPoint();

        Invoke("ReturnToMainScene", 4f); //4秒たったら自動でMainシーンに戻る

        transform.DOLocalMove(new Vector3(141f, 54f, 0), 0.8f)
            .SetEase(Ease.Linear)
            .SetLoops(4, LoopType.Yoyo);
    }

    void ReturnToMainScene()
    {
        ps.IncreaseFriendliness(0, 0);
        ps.DecreaseStress(0,0);
        ps.IncreaseHp(0, 0);
        SceneManager.LoadScene("Main"); // Mainシーンに戻る
    }

}
