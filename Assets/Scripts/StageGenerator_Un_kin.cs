using System.Collections.Generic;
using UnityEngine;

// 無限に続くステージを自動で生成するためのプログラム
public class StageGenerator_Un_kin : MonoBehaviour
{
    // --- 変数（プログラムの設定） ---

    // ステージの部品（チップ）1つあたりのZ方向の長さ。定数(const)なので後から変更できない。
    const int StageChipSize = 30;

    // どこまでステージを生成したかを記録しておくための番号
    int currentChipIndex;

    // 主人公（プレイヤーキャラクター）の位置を知るために設定する
    public Transform character;

    // ステージの部品として使うプレハブ（設計図）を複数設定できる配列
    public GameObject[] stageChips;

    // どの番号のチップから生成を始めるか
    public int startChipIndex;

    // キャラクターの何個先にまでステージをあらかじめ作っておくか
    public int preInstantiate;

    // 生成したステージの情報を入れておくためのリスト。後で消すときに使う。
    public List<GameObject> generatedStageList = new List<GameObject>();


    // --- メソッド（プログラムの動き） ---

    // ゲームが開始されたときに1度だけ実行される処理
    void Start()
    {
        // まだ何も生成していないので、現在のチップ番号を開始番号より1つ前にしておく
        currentChipIndex = startChipIndex - 1;
        
        // 最初にキャラクターの前に必要な分だけステージを生成する
        UpdateStage(preInstantiate);
    }

    // ゲーム中に毎フレーム（1秒間に何十回も）ずっと実行される処理
    void Update()
    {
        // キャラクターのZ座標をチップのサイズで割って、今キャラクターが何番目のチップにいるかを計算する
        int charaPositionIndex = (int)(character.position.z / StageChipSize);

        // 「キャラクターがいるチップ番号」+「先に作っておく数」が、「生成済みの最新チップ番号」を超えたかどうかをチェック
        // もし超えていたら、新しいステージを作る必要がある
        if (charaPositionIndex + preInstantiate > currentChipIndex)
        {
            // 新しいステージを作る処理（UpdateStage）を呼び出す
            UpdateStage(charaPositionIndex + preInstantiate);
        }
    }

    // 新しいステージの生成と、古いステージの破棄を管理する処理
    void UpdateStage(int toChipIndex)
    {
        // もし指定された生成先の番号が、既に生成済みの番号以下なら、何もしないで処理を終える
        if (toChipIndex <= currentChipIndex) return;

        // 「最後に生成したチップの次の番号」から「今回生成すべき目標の番号」まで、ループ処理で1つずつステージを作る
        for (int i = currentChipIndex + 1; i <= toChipIndex; i++)
        {
            // GenerateStageメソッドを呼び出して、i番目のステージを1つ生成する
            GameObject stageObject = GenerateStage(i);
            // 生成したステージを、管理用のリスト（generatedStageList）に追加する
            generatedStageList.Add(stageObject);
        }

        // ステージを作り終えた後、管理リストに入っているステージの数が「先に作っておく数 + 2」より多くなっていたら
        // （+2は、消しすぎないようにするための少しの余裕）
        while (generatedStageList.Count > preInstantiate + 2)
        {
            // 一番古いステージを消す処理（DestroyOldestStage）を呼び出す
            DestroyOldestStage();
        }

        // 最新の生成済みチップ番号を、今回の目標番号で更新する
        currentChipIndex = toChipIndex;
    }

    // 実際にステージを1つ生成する処理
    GameObject GenerateStage(int chipIndex)
    {
        // どのステージ部品を使うか、配列の中からランダムで番号を選ぶ
        int nextStageChip = Random.Range(0, stageChips.Length);

        // 選ばれたステージ部品のプレハブを、指定された場所に生成する
        GameObject stageObject = Instantiate(
            stageChips[nextStageChip], // どのプレハブを使うか
            new Vector3(0, 0, chipIndex * StageChipSize), // どこに作るか (X, Y, Z座標)
            Quaternion.identity // 回転させずにそのままの向きで
        );

        // 生成したステージの情報を返す
        return stageObject;
    }

    // 一番古いステージを1つ破壊する処理
    void DestroyOldestStage()
    {
        // 管理リストの先頭（0番目）にあるステージが一番古いものなので、それを取り出す
        GameObject oldStage = generatedStageList[0];
        // 管理リストから、その一番古いステージの情報を削除する
        generatedStageList.RemoveAt(0);
        // ゲームの世界から、そのステージのオブジェクト自体を破壊して消す
        Destroy(oldStage);
    }
}