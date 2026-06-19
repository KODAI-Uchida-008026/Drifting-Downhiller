using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour
{
    // 追従カメラを取得
    [SerializeField] GameObject followCam;

    // パネルの取得
    [SerializeField] GameObject panel;

    // TimeManageの取得
    [SerializeField] TimeManager tm;

    // 練習シーンであるか確認
    private bool isPractice;

    // ボタンクリックでタイトルから練習シーンへ
    // シーン遷移してからカラー選択のゲットint


    public void OnTriggerEnter(Collider collider)
    {
        // ゴールラインでの判定
        if (collider.CompareTag("Change"))
        {
            // カメラの位置変更
            followCam.SetActive(false);

            // 車の位置変更
            transform.position = new Vector3(-8, 2, 40);

            // タイマーストップ
            tm.timerPause();
        }
        else if (collider.CompareTag("Finish"))
        {
            // タイムアタック終了
            tm.timerStop();

            // リザルト画面へシーン遷移
            SceneManager.LoadScene("ResultScene");
        }
    }

    private void Start()
    {
        // 練習シーンを毎回呼び出さないようここで代入
        isPractice = SceneManager.GetActiveScene().name == "PracticeScene";

        // コルーチンでリザルト画面からタイトルに戻る

    }

    private void Update()
    {
        // Pキーを押すと再度コース練習
        if (isPractice && Keyboard.current.pKey.wasPressedThisFrame)
        {
            // カメラの変更
            followCam.SetActive(true);

            // 車の位置変更
            transform.position = new Vector3(-58, 2, -50);
            transform.rotation = Quaternion.identity;

            // コース練習に戻ればタイマー再開
            tm.timerReset();
        }
        else if (isPractice && Keyboard.current.oKey.wasPressedThisFrame)
        {
            // Oキーを押すとフリースペースへ移動
            followCam.SetActive(false);

            // 車の位置変更 (向きも直す)
            transform.position = new Vector3(-8, 2, 40);

            // タイマー停止
            tm.timerPause();
        }
        else if (isPractice && Keyboard.current.iKey.wasPressedThisFrame)
        {
            // Iキーを押すとタイムアタック本番を始めるかの確認
            panel.SetActive(!panel.activeSelf);
            // activeSelfは現在の状態　これを! 否定するため もう一度押せば消せる
        }
    }
}