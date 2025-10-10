using DG.Tweening;
using UnityEngine;

public class PanelScript_kin : MonoBehaviour
{
    RectTransform rect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rect = GetComponent<RectTransform>();
        rect.DOAnchorPos(new Vector2(200f, 50f), 0.6f)
        .SetEase(Ease.OutBack);
        rect.DOLocalRotate(new Vector3(360f, 0, 0), 0.6f,
        RotateMode.FastBeyond360)
        .SetEase(Ease.OutCubic);
        rect.localScale = Vector3.one * 0.2f;
        rect.DOScale(1f, 0.6f)
        .SetEase(Ease.OutBack, 5f);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
