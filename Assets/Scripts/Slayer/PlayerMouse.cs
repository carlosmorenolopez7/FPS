using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMouse : MonoBehaviour
{
    public float sensitivity = 10f;
    public Transform cuerpoJugador;
    private float rotacionVertical;
    private float rotacionHorizontal;
    private Vector2 entradaRaton;

    public void OnLook(InputValue value)
    {
        entradaRaton = value.Get<Vector2>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void CambiarArma(Transform arma)
    {
        Cursor.lockState = CursorLockMode.Locked;
        cuerpoJugador = arma;
    }

    void Update()
    {
        float mouseX = entradaRaton.x * sensitivity * Time.deltaTime;
        float mouseY = entradaRaton.y * sensitivity * Time.deltaTime;
        rotacionVertical -= mouseY;
        rotacionVertical = Mathf.Clamp(rotacionVertical, -80f, 80f);
        rotacionHorizontal += mouseX;
        cuerpoJugador.localRotation = Quaternion.Euler(rotacionVertical, rotacionHorizontal, 0f);
    }
}