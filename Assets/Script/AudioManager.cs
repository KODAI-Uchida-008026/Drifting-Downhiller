using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // シーンごとにBGMを変えるためインスペクターで個別に張り付けていく
    [SerializeField] AudioSource BGM;

    // オーディオソースの取得
    public AudioSource bgmAudio;
    public AudioSource seAudio;

    // ドリフト判定を取得するためのカーコントローラースクリプトの参照
    [SerializeField] CarController carController;

    private void Start()
    {
        bgmAudio.Play();

        // SEはアサインされていなければ起動しない
        if (seAudio != null)
        {
            seAudio.Stop();
        }
    }

    private void Update()
    {
        driftSE();
    }


    public void driftSE()
    {
        // 練習シーン・ゲームシーン以外ではSEの判定はしない
        if (carController == null)
            return;

        // ドリフト起動時であればSEが鳴り すでになっていればさらに鳴らないように制限
        if (carController.isDrift == true)
        {
            if (!seAudio.isPlaying)
                seAudio.Play();
        }
        else
        {
            seAudio.Stop();
        }
    }
}