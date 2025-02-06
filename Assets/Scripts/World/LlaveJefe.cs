using UnityEngine;

public class LlaveJefe : MonoBehaviour
{
    public GameObject puerta;
    private bool objetoRecogido = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !objetoRecogido)
        {
            objetoRecogido = true;
            AbrirPuerta();
            Destroy(gameObject);
        }
    }

    void AbrirPuerta()
    {
        if (puerta != null)
        {
            puerta.GetComponent<MeshCollider>().enabled = false;
        }
    }
}
