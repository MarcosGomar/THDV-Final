using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // Importar TextMeshPro
using UnityEngine.SceneManagement;  // Importar SceneManager para cambiar de escenas

public class BombSpawner : MonoBehaviour
{
    public GameObject bombPrefab;
    public Transform objectManager;
    public float explosionDelay = 3.0f;
    public GameObject explosionEffectPrefab;
    public float explosionEffectDuration = 2.0f;
    public float explosionRadius = 5.0f;
    public float explosionForce = 500f;
    public float explosionUpwardModifier = 1.0f;

    public TextMeshProUGUI vidaText;
    public TextMeshProUGUI objetivoText;

    public int vida = 5;  // Vida del jugador (ahora pública)
    private int enemigosDerrotados = 0;
    private int objetivoTotal = 10;

    void Start()
    {
        UpdateVidaText();
        UpdateObjetivoText();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnBomb();
        }
    }

    void SpawnBomb()
    {
        Vector3 spawnPosition = objectManager.position;
        GameObject bomb = Instantiate(bombPrefab, spawnPosition, Quaternion.identity);
        StartCoroutine(ExplodeAfterDelay(bomb, explosionDelay));
    }

    IEnumerator ExplodeAfterDelay(GameObject bomb, float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject explosion = Instantiate(explosionEffectPrefab, bomb.transform.position, Quaternion.identity);
        Destroy(bomb);
        Explode(bomb.transform.position);
        Destroy(explosion, explosionEffectDuration);
    }

    void Explode(Vector3 explosionPosition)
    {
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.CompareTag("Enemigo"))
            {
                Destroy(nearbyObject.gameObject);
                enemigosDerrotados++;
                UpdateObjetivoText();

                // Verificar si se completaron los objetivos
                if (enemigosDerrotados >= objetivoTotal)
                {
                    GanarJuego();
                }
            }
            if (nearbyObject.CompareTag("Destruible"))
            {
                Destroy(nearbyObject.gameObject);
            }
            if (nearbyObject.CompareTag("Bala"))
            {
                Destroy(nearbyObject.gameObject);
            }
            if (nearbyObject.CompareTag("Jugador"))
            {
                vida--;
                UpdateVidaText();

                if (vida <= 0)
                {
                    PerderJuego();
                }

                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 direction = (nearbyObject.transform.position - explosionPosition).normalized;
                    Vector3 force = direction * explosionForce;
                    force.y = 0;
                    rb.AddForce(force, ForceMode.Impulse);
                }
            }
        }
    }

    public void Daño(int cantidad)
    {
        vida -= cantidad;
        UpdateVidaText();

        if (vida <= 0)
        {
            PerderJuego();
        }
    }

    void UpdateVidaText()
    {
        vidaText.text = $"Vida: {vida}/5";
    }

    void UpdateObjetivoText()
    {
        objetivoText.text = $"Enemigos derrotados: {enemigosDerrotados}/{objetivoTotal}";
    }

    void GanarJuego()
    {
        Debug.Log("¡Has ganado! Regresando al menú principal...");
        SceneManager.LoadScene("Main menu");  // Asegúrate de que la escena de menú principal esté agregada en las Build Settings y tenga el nombre "MainMenu"
    }

    void PerderJuego()
    {
        Debug.Log("Game Over. Regresando al menú principal...");
        SceneManager.LoadScene("Main menu");  // Asegúrate de que la escena de menú principal esté agregada en las Build Settings y tenga el nombre "MainMenu"
    }
}
