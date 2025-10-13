using UnityEngine;
using DG.Tweening;

public class CharaScript_kin : MonoBehaviour
{
    SpriteRenderer rend;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        transform.DOMove(new Vector3(7, 0, 0), 2f)
        .OnComplete(Method);
        transform.DOScale(new Vector3(2f, 2f, 0), 2f)
        .SetDelay(2f)
        .SetLoops(-1, LoopType.Yoyo);
        rend.DOColor(Color.red, 1f)
        .SetDelay(4f);
        rend.DOFade(0, 2f)
        .SetDelay(5f);

    }
    void Method()
    {
        Debug.Log("OK");
    }

}
