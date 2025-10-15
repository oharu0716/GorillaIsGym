using UnityEngine;
using System.Collections;

public class TargetSpawner : MonoBehaviour
{
    [Header("ターゲットプレハブ（順番固定推奨）")]
    public GameObject applePrefab;  // 1000点
    public GameObject mikanPrefab;  // 1000点
    public GameObject cheesePrefab; // 3000点
    public GameObject meatPrefab;   // 5000点（レア）

    [Header("出現設定")]
    public float spawnInterval = 1.5f;             // 通常出現間隔
    public Vector3 spawnArea = new Vector3(8f, 2f, 8f); // 出現範囲
    public float rareSpawnDelay = 15f;             // 肉の出現までの時間（秒）

    private bool isSpawning = false;//ターゲットを出しているかどうか
    private bool rareSpawned = false;//レアターゲットをすでに出したか

    //ターゲット出現をスタート
    public void StartSpawning()
    {
        //isSpawningがfalseの時だけ動作
        if (!isSpawning)
        {
            isSpawning = true;
            rareSpawned = false;

            // 通常ターゲットを繰り返し出す
            //1秒後に最初にターゲットを出しそれ以降は
            //spawnIntervalごとに繰り返す
            InvokeRepeating(nameof(SpawnNormalTarget), 1f, spawnInterval);

            // 肉を15秒後に一度だけ出現
            //StartCroutine()で遅延実行
            StartCoroutine(SpawnRareTargetAfterDelay(rareSpawnDelay));
        }
    }

    //ターゲット出現を止める
    public void StopSpawning()
    {
        isSpawning = false;
        //CancelInvoke()で通常ターゲットの出現を止める
        CancelInvoke(nameof(SpawnNormalTarget));
        //StopAllCoroutine()でレアターゲットも停止
        //ゲーム終了時などに呼ばれて出現を完全に止める
        StopAllCoroutines();
    }

    void SpawnNormalTarget()
    {
        //出現中でなければ処理しない
        if (!isSpawning) return;

        //spawnAreaの範囲内でランダムな位置を計算
        Vector3 randomPos = new Vector3(
            Random.Range(-spawnArea.x, spawnArea.x),//X軸：左右の範囲
            Random.Range(1f, spawnArea.y),//Y軸：高さ（1〜spawnArea.yの間）
            Random.Range(-spawnArea.z, spawnArea.z)//Z軸：奥行き方向
        );

        //出すターゲットをChooseNormalTarget（）で決定
        GameObject prefab = ChooseNormalTarget();
        //Instantiate（）で生成
        Instantiate(prefab, randomPos, Quaternion.identity);
    }

    GameObject ChooseNormalTarget()
    {
        // 🎯 出現確率設定（合計100%）
        // apple: 50%、mikan: 30%、cheese: 20%
        float roll = Random.value;

        //Random.valueは0~1の乱数を返す
        //範囲を使って確率をコントロール
        if (roll < 0.5f)
            return applePrefab;
        else if (roll < 0.8f)
            return mikanPrefab;
        else
            return cheesePrefab;
    }

    IEnumerator SpawnRareTargetAfterDelay(float delay)
    {
        //指定時間(rareSpawnDelay)だけ待ってから処理を実行
        yield return new WaitForSeconds(delay);

        //rareSpawnedがfalseの時だけ生成
        if (!rareSpawned && meatPrefab != null)
        {
            //出したらrareSpawnedをtrueにして再出現を防止
            rareSpawned = true;

            // 画面中央（原点）に出す
            Vector3 centerPos = new Vector3(0f, 1.5f, 0f);
            Instantiate(meatPrefab, centerPos, Quaternion.identity);

            Debug.Log("🌟 レアターゲット（肉）が出現！");
        }
    }
}
