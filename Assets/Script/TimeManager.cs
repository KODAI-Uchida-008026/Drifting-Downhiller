using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // ゲームオブジェクトの参照 (アサイン)
    [SerializeField] TextMeshProUGUI Timer;

    // 時間
    public float time = 0.0f;

    // 記録タイム
    public float newTime;

    // 時間判定
    [SerializeField] private bool isPlaying;

    void Start()
    {
        Timer = GetComponent<TextMeshProUGUI>();
        isPlaying = true;
    }

    void Update()
    {
        // 時間経過
        if (isPlaying)
        {
            this.time += Time.deltaTime;
        }

        // 時間経過に合わせてテキストの更新
        Timer.text = this.time.ToString("F1");
    }

    public void timerStop()
    {
        // タイマーストップ
        isPlaying = false;

        // 記録タイム
        newTime = this.time;

        // タイム表示
        Debug.Log("記録タイム： " + newTime.ToString("F1"));

        // データ保存
        PlayerPrefs.SetFloat("NewTime", newTime);

        // データセーブ
        PlayerPrefs.Save();
        Debug.Log("セーブ完了");
    }
}