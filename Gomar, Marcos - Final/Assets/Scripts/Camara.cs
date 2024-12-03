using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    // Referencia al jugador
    public Transform player;

    // Altura de la cámara sobre el jugador
    public float altura = 20.0f;

    void LateUpdate()
    {
        if (player != null)
        {
            // Colocar la cámara sobre el jugador a una cierta altura
            Vector3 posicionCamara = player.position + Vector3.up * altura;

            // Ajustar la posición de la cámara
            transform.position = posicionCamara;

            // Fijar la rotación de la cámara para que siempre mire hacia abajo, sin cambiar.
            transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        }
    }
}
