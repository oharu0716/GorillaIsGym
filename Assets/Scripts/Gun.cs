using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Camera cam;
    public float range = 100f;
    public ScoreManager scoreManager;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.Hit();

                //タグに応じてスコアを加算
                if (hit.transform.CompareTag("1000"))
                {
                    scoreManager.AddScore(1000);
                }
                else if (hit.transform.CompareTag("3000"))
                {
                    scoreManager.AddScore(3000);
                }
                else if (hit.transform.CompareTag("5000"))
                {
                    scoreManager.AddScore(5000);
                }
            }
        }
    }
}
