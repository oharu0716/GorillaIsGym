using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowerButtonController : MonoBehaviour
{
    public PlayerStatus ps;
    public GameObject comment;

    void Start()
    {
        ps = PlayerStatus.instance;
        comment.SetActive(false);

         // pointがたまっていなければクリック不可にして見た目も少し薄くする
        if(ps.showerPoint < 2)
        {
            comment.SetActive(true);
            this.GetComponent<Button>().interactable = false;
            this.GetComponentInChildren<TextMeshProUGUI>().color = Color.gray;
        }
    }

}
