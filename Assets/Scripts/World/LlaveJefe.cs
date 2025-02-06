using UnityEngine;

public class LlaveJefe : MonoBehaviour
{
    public GameObject puerta;
    public GameObject mentira;
    private bool objetoRecogido = false;
    public GameObject deteccion2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !objetoRecogido)
        {
            objetoRecogido = true;
            AbrirPuerta();
            AbrirMentira();
            ActivarMisionCanvas();
            ActivarDeteccion2();
            GameManager.Instance.TransitionMusic();
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

    void AbrirMentira()
    {
        if (mentira != null)
        {
            MeshRenderer renderer = mentira.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }
        }
    }

    void ActivarMisionCanvas()
    {
        Mision mision = FindObjectOfType<Mision>();
        if (mision != null)
        {
            mision.ActivarCanvas();
        }
    }

    void ActivarDeteccion2()
    {
        if (deteccion2 != null)
        {
            deteccion2.SetActive(true);
        }
    }
}