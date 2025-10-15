using UnityEngine;

public class Gun : MonoBehaviour
{
    public Camera cam;
    public float range = 100f;
    public ScoreManager scoreManager;
    public GameManager gameManager;

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

                int score = 0;
                // 📍 当たったワールド座標
                Vector3 hitPosition = hit.point;

                // CompareTag で安全にタグ判定
                if (hit.transform.CompareTag("1000"))
                {
                    score = 1000;
                    scoreManager.AddScore(1000);
                }
                else if (hit.transform.CompareTag("3000"))
                {
                    score = 3000;
                    scoreManager.AddScore(3000);
                }
                else if (hit.transform.CompareTag("5000"))
                {
                    score = 5000;
                    scoreManager.AddScore(5000);
                }
                else
                {
                    scoreManager.AddScore(100);
                }
                target.Hit(score,hitPosition);
            }
        }
    }
}
