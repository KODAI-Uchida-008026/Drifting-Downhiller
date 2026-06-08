using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    public float moveSpeed;

    public float turnSpeed;

    public float turn;

    public float grip;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // 速度
        moveSpeed = 0.5f;

        turnSpeed = 50f;

        turn = 0f;

        // 位置情報
        // Debug.Log(transform.position);

        // 速度の大きさを取得
        // Debug.Log(rb.linearVelocity.magnitude);

        // 回転量取得
        // Debug.Log(rb.angularVelocity.magnitude);


        // 前進
        if (Keyboard.current.wKey.isPressed)
        {
            rb.AddForce(transform.right * moveSpeed);
        }

        // 停止
        if (Keyboard.current.spaceKey.isPressed)
        {
            rb.linearVelocity *= 0.999f;
            grip = 0.75f;
        }
        else
        {
            grip = 0.25f;
        }

        // 右
        if (Keyboard.current.dKey.isPressed)
        {
            turn = 1.5f;
            grip = 0.5f;
        }

        // 左
        if (Keyboard.current.aKey.isPressed)
        {
            turn = -1.5f;
            grip = 0.5f;
        }

        transform.Rotate(0, turn * turnSpeed * grip * Time.deltaTime, 0);
    }
}