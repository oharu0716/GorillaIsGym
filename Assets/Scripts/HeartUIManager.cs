using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HeartUIManager : MonoBehaviour
{
    public GameObject[] hearts;

    public void UpdateLife(int life)
    {
        Debug.Log("HearManager");
        Debug.Log(life);
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < life) hearts[i].SetActive(true);
            else hearts[i].SetActive(false);
        }
    }
}
