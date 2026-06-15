using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    // 移動速度
    public float moveSpeed;

    // 曲がる勢い
    public float turnSpeed;

    // 曲がる向き
    public float turn;

    // グリップの強さ(地面との摩擦力がドリフトに影響)
    public float grip;

    // リジットボディの取得
    private Rigidbody rb;

    // ブレーキ判定 (厳密にはグリップの判定)
    public bool isBrake;

    // ドリフト判定
    public bool isDrift;

    private void Start()
    {
        // リジットボディの参照
        rb = GetComponent<Rigidbody>();

        // 速度
        moveSpeed = 5f;
    }

    private void Update()
    {
        // グリップ判定 (曲がりやすさ)
        if (isBrake)
        {
            grip = 0.75f;
        }
        else
        {
            grip = 0.25f;
        }

        // ドリフト判定 (曲がる勢い)
        if (isDrift)
        {
            turnSpeed = 50f;
        }
        else
        {
            turnSpeed = 25f;
        }

        // 停止 (減速)  ブレーキはスペースキーとマウスの右クリック
        if (Keyboard.current.spaceKey.isPressed || Mouse.current.rightButton.isPressed)
        {
            isBrake = true;

            // Vector3.Lerp(現在の値, 目標の値, 進む割合)
            // Lerpは間をとる AとBの間を少しずつ移動する つまり 0に向かって進む (減速していく)
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, 0.5f * Time.deltaTime);
        }
        else
        {
            isBrake = false;
        }

        // 左右の操作 Aキーと左　　Dキーと右
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            turn = -2f;
        }
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            turn = 2f;
        }
        else
        {
            turn = 0f;
        }

        // 前進 (加速) Wキーと上
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
        {
            // リジットボディの加速
            rb.AddForce(transform.right * moveSpeed);

            // ドリフト判定
            if (isBrake == true)
            {
                isDrift = true;
            }
            else
            {
                isDrift = false;
            }
        }
        else
        {
            // 加速中にブレーキした時だけ回転速度が上がる
            isDrift = false;
        }

        // ドリフトの動き
        transform.Rotate(0, turn * turnSpeed * grip * Time.deltaTime, 0);

        // 回転が過度に加速しないよう修正(直進のlinearVelocity同様 減速させる)
        rb.angularVelocity *= 0.75f;
    }
}