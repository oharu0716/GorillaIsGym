using UnityEngine;

public class ClearTrigger_kin : MonoBehaviour
{
    GameObject gameController;
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController");
    }
    void OnTriggerExit2D(Collider2D other)
    {
        gameController.SendMessage("IncreaseScore");
    }
}