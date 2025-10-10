using UnityEngine;

public class Block_kin : MonoBehaviour
{
    public float minHeihgt;
    public float maxHeight;
    public GameObject root;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ChangeHeight();
    }
    void ChangeHeight()
    {
        float height = Random.Range(minHeihgt, maxHeight);
        root.transform.localPosition = new Vector3(0.0f, height, 0.0f);
    }
    void OnScrollEnd()
    {
        ChangeHeight();
    }
}
