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
        cuerpoJugador=arma;
    }
    
    
    void Update()
    {
        //Escalar la entrada del ratón
        float mouseX = entradaRaton.x * sensitivity * Time.deltaTime;
        float mouseY = entradaRaton.y * sensitivity * Time.deltaTime;

        rotacionVertical = mouseY;
        rotacionVertical = Mathf.Clamp(rotacionVertical, -10f, 10f);
        //transform.localRotation = Quaternion.Euler(0f,0f,rotacionVertical);
        
            cuerpoJugador.Rotate(Vector3.left * rotacionVertical);
        


        rotacionHorizontal = mouseX;
        rotacionHorizontal = Mathf.Clamp(rotacionHorizontal, -10f, 10f);
        cuerpoJugador.Rotate(Vector3.up * rotacionHorizontal);
        Vector3 currentRotation = cuerpoJugador.transform.rotation.eulerAngles;
        // Mantener X e Y actuales, y establecer Z a 0
        cuerpoJugador.transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 0f);

    }
}