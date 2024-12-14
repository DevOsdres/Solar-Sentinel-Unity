using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject shieldPrefab;
    public float shieldDuration = 5f;
    public float speed = 3f;
    public float lifetime = 10f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Cuando el jugador recoge el power-up
        {
            PlayerShip player = other.GetComponentInParent<PlayerShip>(); // Referencia al jugador
            if (player != null)
            {
                player.ActivateShield(shieldDuration); // Activar el escudo en el jugador
                Destroy(gameObject); // Destruir el power-up después de ser recogido
            }
        }
    }
}
