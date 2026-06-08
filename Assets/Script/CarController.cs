using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    public float moveSpeed;

    private Vector3 moveVelocity;

    private void Start()
    {
        moveVelocity = Vector3.one;
    }

    private void Update()
    {
        // 速度
        moveSpeed = 1f;

        Debug.Log(transform.position);

        // 前進
        if (Keyboard.current.wKey.isPressed)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }

        // 後退
        if (Keyboard.current.sKey.isPressed)
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }

        // 右
        if (Keyboard.current.dKey.isPressed)
        {
            transform.position += Vector3.back * moveSpeed * Time.deltaTime;
        }

        // 左
        if (Keyboard.current.aKey.isPressed)
        {
            transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
        }
    }
}