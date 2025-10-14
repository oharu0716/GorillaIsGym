using UnityEngine;

public class ShowCharacter : MonoBehaviour
{
    public PlayerStatus ps;
    public GameObject[] characters;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ps = PlayerStatus.instance;

        if (ps.isEvolution1 == false)
        {
            characters[0].SetActive(true);
        }
        else if (ps.isEvolution1 == true)
        {
            characters[1].SetActive(true);
        }
    }
}
