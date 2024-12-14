using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1 : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 2f; // Velocidad de movimiento
    public float waveFrequency = 2f; // Frecuencia de la onda
    public float waveAmplitude = 1f; // Amplitud de la onda

    private Vector3 startPosition;
    private EnemySpawner spawner; // Referencia al EnemySpawner

    public GameObject powerUpPrefab; // Prefab del power-up de escudo
    public float dropChance = 0.2f; // Probabilidad de soltar el power-up (20%)

    // Definir el evento que se activará cuando el enemigo sea destruido
    public delegate void EnemyDestroyedHandler();
    public event EnemyDestroyedHandler onEnemyDestroyed;

    void Start()
    {
        startPosition = transform.position;
        spawner = FindObjectOfType<EnemySpawner>();
        Destroy(gameObject, 10f);
    }

    void Update()
    {
        MoveInSineWave();
    }

    // Movimiento en forma de onda senoidal
    void MoveInSineWave()
    {
        float waveOffset = Mathf.Sin(Time.time * waveFrequency) * waveAmplitude;
        Vector3 newPosition = transform.position; // Inicia desde la posición actual
        newPosition += Vector3.left * (speed * Time.deltaTime); // Movimiento hacia la izquierda
        newPosition.y = startPosition.y + waveOffset; // Asegura el movimiento vertical basado en el punto inicial
        transform.position = newPosition;
    }

    // Detectar colisiones con balas
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Impacto con Player");

            // Obtener el componente del jugador y reducir su vida
            PlayerShip playerShip = collision.GetComponentInParent<PlayerShip>();
            if (playerShip != null)
            {
                Debug.Log("Jugador encontrado, aplicando daño");
                playerShip.TakeDamage();
            }
            else
            {
                Debug.LogError("No se encontró PlayerShip en la colisión.");
            }

            // Destruir al enemigo
            DestroyEnemy();
        }

        if (collision.CompareTag("Bullet"))
        {
            Bullets bullet = collision.GetComponent<Bullets>();

            if (bullet != null)
            {
                switch (bullet.bulletType)
                {
                    case 1: // Bala tipo 1 - Destruye al enemigo y la bala
                        Debug.Log("Impacto con bala tipo 1");
                        DestroyEnemy();
                        Destroy(collision.gameObject); // Destruir bala
                        break;

                    case 2: // Bala tipo 2 - Solo destruye al enemigo
                        Debug.Log("Impacto con bala tipo 2");
                        DestroyEnemy();
                        break;

                    case 3: // Bala tipo 3 - Solo destruye la bala
                        Debug.Log("Impacto con bala tipo 3");
                        Destroy(collision.gameObject); // Destruir bala
                        break;

                    default:
                        Debug.Log("Tipo de bala no reconocido");
                        break;
                }
            }
        }
    }

    // Destruir al enemigo y notificar al spawner
    void DestroyEnemy()
    {
        // Disparar el evento antes de destruir el enemigo
        if (onEnemyDestroyed != null)
        {
            onEnemyDestroyed.Invoke(); // Invocar el evento
        }

        if (Random.value <= dropChance)
        {
            Instantiate(powerUpPrefab, transform.position, Quaternion.identity); // Crear el power-up en la posición del enemigo
        }

        if (spawner != null)
        {
            spawner.EnemyDestroyed(); // Notificar al spawner
        }

        // Llamar a la función para agregar puntaje cuando el enemigo sea destruido
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(1); // Añadir 1 punto al puntaje por cada enemigo destruido
        }

        // Destruir el enemigo
        Destroy(gameObject);
    }
}