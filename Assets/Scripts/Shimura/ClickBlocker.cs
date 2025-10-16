using UnityEngine;
using UnityEngine.EventSystems;

public class ClickBlocker : MonoBehaviour
{

    public void NotClickAccept()
    {
        if (EventSystem.current != null)
        {
            Debug.Log("クリック停止");
            // disableInput が true のとき、入力を無効化する
            EventSystem.current.enabled = false;
        }
    }

    public void ClickAccept()
    {
        if (EventSystem.current != null)
        {
            Debug.Log("クリックOK");
            // disableInput が true のとき、入力を無効化する
            EventSystem.current.enabled = true;
        }
    }
}
