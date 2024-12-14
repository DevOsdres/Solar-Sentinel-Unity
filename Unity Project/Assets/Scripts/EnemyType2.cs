using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType2 : MonoBehaviour
{
    public float speed = 1.5f;
    public float zigzagFrequency = 1.5f;
    public float zigzagAmplitude = 2f;
    
    private bool shieldActive = true;  // Estado del escudo
    private Transform shieldTransform; // Referencia al escudo
    private Vector3 startPosition;

    private EnemySpawner spawner; // Referencia al EnemySpawner

    void Start()
    {
        startPosition = transform.position;
        shieldTransform = transform.Find("Shield");
        spawner = FindObjectOfType<EnemySpawner>();
    }

    void Update()
    {
        MoveInZigZag();
    }

    // Movimiento en zig-zag
    void MoveInZigZag()
    {
        float zigzagOffset = Mathf.Sin(Time.time * zigzagFrequency) * zigzagAmplitude;
        Vector3 newPosition = transform.position;
        newPosition += Vector3.left * (speed * Time.deltaTime); // Movimiento hacia la izquierda
        newPosition.y = startPosition.y + zigzagOffset; // Movimiento en zig-zag
        transform.position = newPosition;
    }

    // Detectar colisiones con el jugador y las balas
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
                    case 1: // Bala tipo 1
                    case 2: // Bala tipo 2
                        if (!shieldActive) // Si el escudo está inactivo, aplicar daño al enemigo
                        {
                            Debug.Log("Bala tipo 1 o 2, daño al enemigo");
                            DestroyEnemy(); // Destruir al enemigo
                        }
                        Destroy(collision.gameObject); // Destruir la bala
                        break;

                    case 3: // Bala tipo 3
                        if (shieldActive) // Si el escudo está activo
                        {
                            Debug.Log("Impacto con Bala Tipo 3, destruyendo escudo");
                            Destroy(shieldTransform.gameObject); // Destruir el escudo
                            shieldActive = false; // Desactivar el escudo
                        }
                        Destroy(collision.gameObject); // Destruir la bala
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