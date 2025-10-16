using UnityEngine;
using UnityEngine.EventSystems;

public class ClickBlocker : MonoBehaviour
{
    public bool disableInput = true;

    public void ChangeClickAccept()
    {
        if (EventSystem.current != null)
        {
            // disableInput が true のとき、入力を無効化する
            EventSystem.current.enabled = !disableInput;
        }
    }
}
