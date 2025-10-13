using UnityEngine;
using System.Collections;

public class ClashCamera_kin : MonoBehaviour
{
    public void Clash()
    {
        FlashEffect_kin.Play();
        GetComponent<Animator>().SetTrigger("shake");
    }
}
