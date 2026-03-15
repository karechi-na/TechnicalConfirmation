using UnityEngine;
using UnityEngine.InputSystem;

public class CapsuleController : MonoBehaviour
{
    private const float NORMAL_MOVE_SPEED = 5.0f;

    Rigidbody rb;

    private float moveSpeed = 0.0f;
    private Vector3 capsuleVelocity = Vector3.zero;
    private bool isDash = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        moveSpeed = NORMAL_MOVE_SPEED;
    }

    private void Update()
    {
        transform.position += capsuleVelocity * moveSpeed * Time.deltaTime;
    }

    private void OnMove(InputValue value)
    {

        var axis = value.Get<Vector2>();

        capsuleVelocity = new Vector3(axis.x, 0.0f, axis.y);

        Debug.Log("OnMove : " + moveSpeed);
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            rb.AddForce(Vector3.up * 3.0f, ForceMode.Impulse);
        }
    }

    private void OnDash(InputValue value)
    {
        if (value.isPressed)
        {
            if (capsuleVelocity == Vector3.zero)
            {
                isDash = false;
                moveSpeed = NORMAL_MOVE_SPEED;
                return;
            }

            if (!isDash)
            {
                moveSpeed *= 2.0f;
                isDash = true;
            }
        }
        Debug.Log("OnDash : " + moveSpeed);
    }

    private void OnCrouch(InputValue value)
    {
        if (value.isPressed)
        {
            transform.localScale =
                new Vector3
                (
                    transform.localScale.x,
                    0.5f,
                    transform.localScale.z
                );
        }
    }
}
