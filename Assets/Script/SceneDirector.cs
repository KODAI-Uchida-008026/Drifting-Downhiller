using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour
{
    // 車の取得
    [SerializeField] GameObject car;

    // 追従カメラを取得
    [SerializeField] GameObject followCam;

    // 練習シーンであるか確認
    private bool isPractice;


    private void OnTriggerEnter(Collider collider)
    {
        // ゴールラインでの判定
        if (collider.CompareTag("Change"))
        {
            // カメラの位置変更
            followCam.SetActive(false);

            // 車の位置変更
            car.transform.localPosition = new Vector3(-8, 2, 40);
        }
        else if (collider.CompareTag("Finish"))
        {
            // リザルト画面へシーン遷移
            SceneManager.LoadScene("ResultScene");
        }
    }

    private void Start()
    {
        // 練習シーンを毎回呼び出さないようここで代入
        isPractice = SceneManager.GetActiveScene().name == "PracticeScene";
    }

    private void Update()
    {
        // Pキーを押すと再度コース練習
        if (isPractice && Keyboard.current.pKey.wasPressedThisFrame)
        {
            // カメラの変更
            followCam.SetActive(true);

            // 車の位置変更
            car.transform.localPosition = new Vector3(-58, 2, -50);
        }
        else if (isPractice && Keyboard.current.oKey.wasPressedThisFrame)
        {
            // Oキーを押すとフリースペースへ移動
            followCam.SetActive(false);

            // 車の位置変更
            car.transform.localPosition = new Vector3(-8, 2, 40);
        }
        else if (isPractice && Keyboard.current.iKey.wasPressedThisFrame)
        {
            // タイムアタック本番を始めるかの確認

        }
    }
}