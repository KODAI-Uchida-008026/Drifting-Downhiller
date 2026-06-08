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

    // ドリフト判定
    public bool isDrift;

    // カーブ判定
    // public bool isCurve;

    private void Start()
    {
        // リジットボディの参照
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // 速度
        moveSpeed = 0.5f;

        turnSpeed = 50f;

        // グリップの判定
        if (isDrift)
        {
            grip = 0.5f;
        }
        else
        {
            grip = 0.25f;
        }

        // 停止
        if (Keyboard.current.spaceKey.isPressed || Keyboard.current.ctrlKey.isPressed
            || Mouse.current.rightButton.isPressed)
        {
            isDrift = true;

            // Vector3.Lerp(現在の値, 目標の値, 進む割合)
            // Lerpは間をとる AとBの間を少しずつ移動する つまり 0に向かって進む (減速していく)
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, 0.5f * Time.deltaTime);
        }
        else
        {
            isDrift = false;
        }

        // 左右の操作
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed
            || Keyboard.current.zKey.isPressed)
        {
            turn = -1.5f;
        }
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed
            || Keyboard.current.cKey.isPressed)
        {
            turn = 1.5f;
        }
        else
        {
            turn = 0f;
        }

        // 前進
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed
            || Mouse.current.leftButton.isPressed || Keyboard.current.xKey.isPressed)
        {
            rb.AddForce(transform.right * moveSpeed);
        }

        transform.Rotate(0, turn * turnSpeed * grip * Time.deltaTime, 0);
    }
}