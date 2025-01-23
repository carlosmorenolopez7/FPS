using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 20f;

    private Vector2 entradaMovimiento;
    private Transform cameraTransform;

    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    public void OnMove(InputValue valor)
    {
        entradaMovimiento = valor.Get<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 movimiento = forward * entradaMovimiento.y + right * entradaMovimiento.x;
        transform.Translate(movimiento * speed * Time.deltaTime, Space.World);
    }
}