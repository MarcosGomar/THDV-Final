using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public GameObject bombPrefab;  // Prefab de la bomba que se va a instanciar
    public Transform objectManager;  // Referencia al transform del ObjectManager
    public float explosionDelay = 3.0f;  // Tiempo antes de que explote la bomba
    public GameObject explosionEffectPrefab;  // Prefab del efecto de explosión
    public float explosionEffectDuration = 2.0f;  // Tiempo que el efecto de explosión permanece visible
    public float explosionRadius = 5.0f;  // Radio de la explosión

    void Update()
    {
        // Detectar si se ha hecho clic izquierdo del ratón
        if (Input.GetMouseButtonDown(0))
        {
            SpawnBomb();
        }
    }

    void SpawnBomb()
    {
        // Obtener la posición exacta del ObjectManager
        Vector3 spawnPosition = objectManager.position;

        // Instanciar la bomba en la posición del ObjectManager
        GameObject bomb = Instantiate(bombPrefab, spawnPosition, Quaternion.identity);

        // Iniciar la rutina de explosión
        StartCoroutine(ExplodeAfterDelay(bomb, explosionDelay));
    }

    IEnumerator ExplodeAfterDelay(GameObject bomb, float delay)
    {
        // Esperar la cantidad de tiempo especificada
        yield return new WaitForSeconds(delay);

        // Crear el efecto de explosión en la misma posición que la bomba
        GameObject explosion = Instantiate(explosionEffectPrefab, bomb.transform.position, Quaternion.identity);

        // Destruir la bomba
        Destroy(bomb);

        // Hacer daño a los objetos cercanos (destruir los enemigos)
        Explode(bomb.transform.position);

        // Destruir el efecto de explosión después de que transcurran 2 segundos
        Destroy(explosion, explosionEffectDuration);
    }

    void Explode(Vector3 explosionPosition)
    {
        // Obtener todos los colliders en el radio de la explosión
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            // Verificar si el objeto tiene el tag "enemigo"
            if (nearbyObject.CompareTag("Enemigo"))
            {
                // Destruir el enemigo inmediatamente
                Destroy(nearbyObject.gameObject);
            }
        }
    }
}
