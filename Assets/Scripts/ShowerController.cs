using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ShowerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("ReturnToMainScene", 4f);

        transform.DOLocalMove(new Vector3(141f, 54f, 0), 0.8f)
            .SetEase(Ease.Linear)
            .SetLoops(4, LoopType.Yoyo);
    }

    void ReturnToMainScene()
    {
        SceneManager.LoadScene("Main"); // Mainシーンに戻る
    }

}
