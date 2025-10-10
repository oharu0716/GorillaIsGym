
using UnityEngine;
using UnityEngine.LightTransport;

public class TargetSpawner : MonoBehaviour
{
    public GameObject[] targetPrefabs; //ターゲットのプレハブ
    public float spawnInterval = 2f; //出現間隔
    public Vector3 spawnArea = new Vector3(8f, 2f, 8f); //出現範囲

    void Start()
    {
        // 1秒後から「SpawnTarget」を呼び出し、以後spawnIntervalごとに繰り返す
        InvokeRepeating(nameof(SpawnTarget), 1f, spawnInterval);
    }

    void SpawnTarget()
    {
        //Random.Range(a, b) a〜b の範囲でランダムな値を返す関数。
        Vector3 randomPos = new Vector3(
            //X … -spawnArea.x から spawnArea.x
            Random.Range(-spawnArea.x, spawnArea.x), 
            //Y … 1f から spawnArea.y（地面に埋まらないように下限を 1 に設定）
            Random.Range(1f, spawnArea.y),           
            //Z … -spawnArea.z から spawnArea.z
            Random.Range(-spawnArea.z, spawnArea.z)  
        );

        //プレハブが１つも登録されていなければ何もしない
        if (targetPrefabs.Length == 0) return;
        GameObject selectedPrefab = targetPrefabs[Random.Range(0, targetPrefabs.Length)];
        //第1引数 … 選ばれたプレハブ
        //第2引数 … 出現位置（randomPos）
        //第3引数 … 回転情報。Quaternion.identity は「回転なし（デフォルト）」
        Instantiate(selectedPrefab, randomPos, Quaternion.identity);
    }
    
}
