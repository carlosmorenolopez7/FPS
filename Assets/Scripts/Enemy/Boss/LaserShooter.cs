using UnityEngine;

public class LaserShooter : MonoBehaviour
{
    public GameObject laserPrefab; // El prefab del rayo láser.
    public Transform firePoint; // El punto desde donde se dispara el rayo.
    public float laserSpeed = 20f; // Velocidad del láser.

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Ejemplo: al presionar la tecla de espacio
        {
            FireLaser();
        }
    }

    void FireLaser()
    {
        // Crear el rayo láser en la posición del firePoint.
        GameObject laser = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);

        // Mover el rayo hacia adelante.
        Rigidbody rb = laser.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * laserSpeed; // Ajusta la velocidad.
        }
    }

    /*void FireLaser()
    {
        RaycastHit hit;

        // Disparar un raycast desde el punto de disparo hacia adelante.
        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, Mathf.Infinity))
        {
            // Mostrar el láser visualmente
            GameObject laser = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = laser.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = firePoint.forward * laserSpeed;
            }

            // Si el rayo impacta algo
            if (hit.collider != null)
            {
                Debug.Log("Impactó con " + hit.collider.name);
                // Aquí puedes agregar lo que sucede cuando el láser impacta (daño, efectos, etc.)
            }
        }
    }*/
}