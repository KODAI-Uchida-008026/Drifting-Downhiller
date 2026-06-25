using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // シーンごとにBGMを変えるためインスペクターで個別に張り付けていく
    [SerializeField] AudioSource BGM;

    // SE用にミキサーを取得しておく
    public AudioMixer mixer;

    // ドリフト判定を取得するためのカーコントローラースクリプトの参照
    [SerializeField] CarController carController;

    private void Update()
    {
        driftSE();
    }

    // 練習シーン・ゲームシーン以外ではSEの判定はしない
    public void driftSE()
    {
        if (carController == null)
            return;

        // ドリフト起動時のみSEが鳴る
        if (carController.isDrift == true)
        {
            mixer.SetFloat("SE", 1f);
        }
        else
        {
            mixer.SetFloat("SE", -80f);
        }
    }
}