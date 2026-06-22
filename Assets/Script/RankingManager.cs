using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    // 1位から5位までの配列
    public TextMeshProUGUI[] ranking;

    // プレイヤーの記録
    public TextMeshProUGUI Your;

    // リストでデータを管理
    List<float> ranks = new List<float>();

    // 最新記録
    private float resultTime;

    [Serializable]
    public class SaveData
    {
        public List<float> ranks;
    }

    private void Start()
    {
        // セーブデータの読み込み
        PrefsLoad();

        // JSONのセーブデータクラス
        SaveData data = new SaveData();

        // JSONのデータの中にリスト代入
        data.ranks = ranks;

        // データをJSON文字列に変換する
        string json = JsonUtility.ToJson(data);

        Debug.Log(json);

        // 今回の記録
        if (PlayerPrefs.HasKey("NewTime"))
        {
            ranks.Add(PlayerPrefs.GetFloat("NewTime"));
        }
        else
        {
            Your.text = "-----";
        }

        // 個別で保持　(左が無ければ右が出てくる)
        resultTime = PlayerPrefs.GetFloat("NewTime", 999);

        // 並び替え
        ranks.Sort();

        // ５位未満の記録を削除しておく　(6位から上位5つを除いた記録を削除する)
        if (ranks.Count >= 5)
        {
            ranks.RemoveRange(5, ranks.Count - 5);
        }

        // データ更新
        for (int i = 0; i < ranking.Length; i++)
        {
            if (ranks[i] == 999)
            {
                ranking[i].text = "-----";
            }
            else
            {
                ranking[i].text = ranks[i].ToString("F1");
            }
        }



        // JSONデータの読み込み
        // SaveData save = JsonUtility.FromJson<SaveData>(json);

        // リストにセーブデータを代入
        // ranks = save.ranks;

        // Debug.Log(json);

        // 記録保持
        RankSave();

        // 今回の記録
        Your.text = resultTime.ToString("F1");
    }

    // セーブデータの読み込み
    void PrefsLoad()
    {
        // クリアしてから読み込む
        ranks.Clear();

        // リストに追加していく　(左が無ければ右が出てくる)
        ranks.Add(PlayerPrefs.GetFloat("First", 999));
        ranks.Add(PlayerPrefs.GetFloat("Second", 999));
        ranks.Add(PlayerPrefs.GetFloat("Third", 999));
        ranks.Add(PlayerPrefs.GetFloat("Fourth", 999));
        ranks.Add(PlayerPrefs.GetFloat("Fifth", 999));

    }

    // 記録保持
    void RankSave()
    {
        PlayerPrefs.SetFloat("First", ranks[0]);
        PlayerPrefs.SetFloat("Second", ranks[1]);
        PlayerPrefs.SetFloat("Third", ranks[2]);
        PlayerPrefs.SetFloat("Fourth", ranks[3]);
        PlayerPrefs.SetFloat("Fifth", ranks[4]);

        // 保存
        PlayerPrefs.Save();
    }


}