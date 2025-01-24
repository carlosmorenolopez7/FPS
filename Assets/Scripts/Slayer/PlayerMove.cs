using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 20f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    private Vector2 entradaMovimiento;
    private Transform cameraTransform;
    private CharacterController characterController;
    private Vector3 velocity;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        characterController = GetComponent<CharacterController>();
    }

    public void OnMove(InputValue valor)
    {
        entradaMovimiento = valor.Get<Vector2>();
    }

    public void OnJump()
    {
        if (characterController.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void Update()
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        Vector3 movimiento = forward * entradaMovimiento.y + right * entradaMovimiento.x;
        characterController.Move(movimiento * speed * Time.deltaTime);

        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}
