using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ShowerController : MonoBehaviour
{
    public static bool TookShower = false;
    PlayerStatus ps;
    AudioManager am;

    public AudioClip showerSound;
    public AudioClip bubbleSound;

    public GameObject[] characters;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        am = AudioManager.Instance;
        ps = PlayerStatus.instance;
        ps.DecreaseShowerPoint();
        am.PlaySE(showerSound);
        am.PlaySE(bubbleSound);

         if (ps.isEvolution1 == false)
        {
            characters[0].SetActive(true);
        }
        else if (ps.isEvolution1 == true)
        {
            characters[1].SetActive(true);
        }

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
        am.StopSE();
        SceneManager.LoadScene("Main"); // Mainシーンに戻る
    }

}
