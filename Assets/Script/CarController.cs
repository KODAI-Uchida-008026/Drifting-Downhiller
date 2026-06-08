using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    public float moveSpeed;

    public float turnSpeed;

    private Vector3 moveVelocity;

    private Rigidbody rb;

    private void Start()
    {
        moveVelocity = Vector3.one;

        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // 速度
        moveSpeed = 1f;

        turnSpeed = 100f;

        Debug.Log(transform.position);

        // 前進
        if (Keyboard.current.wKey.isPressed)
        {
            // transform.position += Vector3.back * moveSpeed * Time.deltaTime;
            rb.AddForce(transform.right * moveSpeed);
        }

        // 後退
        if (Keyboard.current.sKey.isPressed)
        {
            // transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
            rb.AddForce(-transform.right * moveSpeed);
        }

        // 右
        if (Keyboard.current.dKey.isPressed)
        {
            transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
        }

        // 左
        if (Keyboard.current.aKey.isPressed)
        {
            transform.Rotate(0, -turnSpeed * Time.deltaTime, 0);
        }
    }
}