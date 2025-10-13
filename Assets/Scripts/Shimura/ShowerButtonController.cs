using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowerButtonController : MonoBehaviour
{
    public PlayerStatus ps;

    void Start()
    {
        ps = PlayerStatus.instance;

         // pointがたまっていなければクリック不可にして見た目も少し薄くする
        if(ps.showerPoint < 2)
        {
            this.GetComponent<Button>().interactable = false;
            this.GetComponentInChildren<TextMeshProUGUI>().color = Color.gray;
        }
    }

}
