using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    // 1位から5位までの配列
    public TextMeshProUGUI[] ranking;

    // プレイヤーの記録
    public TextMeshProUGUI Your;

    // 最新記録の保存
    private float resultTime;

    // 記録なし (constで値の固定)
    private const float Empty = 999;

    // リストでデータを管理
    List<float> ranks = new List<float>();

    [Serializable]  // JSONでデータを管理
    public class SaveList
    {
        // JSONと連動させるリスト
        public List<float> ranks;
    }

    private void Start()
    {
        // セーブデータの読み込み
        DataLoad();

        // 今回の記録
        if (PlayerPrefs.HasKey("NewTime"))
        {
            ranks.Add(PlayerPrefs.GetFloat("NewTime"));
        }

        // 個別で保持　(左が無ければ右が出てくる)
        resultTime = PlayerPrefs.GetFloat("NewTime", Empty);

        // 並び替え
        ranks.Sort();

        // ５位未満の記録を削除しておく　(6位から上位5つを除いた記録を削除する)
        if (ranks.Count > 5)
        {
            ranks.RemoveRange(5, ranks.Count - 5);
        }

        // データ更新
        for (int i = 0; i < ranking.Length; i++)
        {
            if (ranks[i] == Empty)
            {
                ranking[i].text = "-----";
            }
            else
            {
                ranking[i].text = ranks[i].ToString("F1");
            }
        }

        // NewTimeの削除 (resultTimeには残っている)
        PlayerPrefs.DeleteKey("NewTime");

        // 最新記録の確認
        if (resultTime == Empty)
        {
            // 記録が無ければ表示なし
            Your.text = "-----";
        }
        else
        {
            // 今回の記録を表示
            Your.text = resultTime.ToString("F1");
        }

        // 記録保持
        DataSave();
    }

    void DataSave()
    {
        // JSONのセーブデータクラス
        SaveList data = new SaveList();

        // JSONのデータの中にリスト代入
        data.ranks = ranks;

        // データをJSON文字列に変換する
        string json = JsonUtility.ToJson(data, true);

        // 保存先はUnityが管理してくれる
        string path = Application.persistentDataPath + "/save.json";

        // WriteAllだから書き込んで出力
        File.WriteAllText(path, json);

        // 確認
        Debug.Log(json);
    }

    void DataLoad()
    {
        // Unityが作成してくれる保存管理先の確認用データパス
        string path = Application.persistentDataPath + "/save.json";

        // パスの確認
        Debug.Log(path);

        // File.Existsで初回クラッシュ対策
        if (File.Exists(path))
        {
            // ReadAllだから読み込んで入力
            string saveData = File.ReadAllText(path);

            // データに変換
            SaveList save = JsonUtility.FromJson<SaveList>(saveData);

            // もしパスはあってもデータがなかった場合の対策 (フェイルセーフ処理のnullチェック)
            if (save != null && save.ranks != null)
            {
                // 何も問題なければリストに代入
                ranks = save.ranks;
            }
            else
            {
                // データが無ければ表示なし
                ranks = new List<float>() { Empty, Empty, Empty, Empty, Empty };
            }
        }
        else
        {
            // データが無ければ表示なし
            ranks = new List<float>() { Empty, Empty, Empty, Empty, Empty };
        }
    }
}