using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    // Referencia al jugador
    public Transform player;

    // Altura de la c�mara sobre el jugador
    public float altura = 20.0f;

    void LateUpdate()
    {
        if (player != null)
        {
            // Colocar la c�mara sobre el jugador a una cierta altura
            Vector3 posicionCamara = player.position + Vector3.up * altura;

            // Ajustar la posici�n de la c�mara
            transform.position = posicionCamara;

            // Fijar la rotaci�n de la c�mara para que siempre mire hacia abajo, sin cambiar.
            transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        }
    }
}
