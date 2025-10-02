using UnityEngine;

public class SimpleFollow : MonoBehaviour
{
    Vector3 diff;
    public GameObject target;
    public float followSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        diff = target.transform.position - transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //下ラグなし。完全追従
        // transform.position = target.transform.position - diff;
        //ラグあり追従。実際のカメラ感
        transform.position = Vector3.Lerp(
            transform.position,
            target.transform.position - diff,
            Time.deltaTime * followSpeed

        );
    }
}
