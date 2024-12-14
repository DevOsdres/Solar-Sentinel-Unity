using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{

    public Vector2 direction;
    public float speed = 10f;
    public Vector2 velocity;

    public int bulletType;
    public string shooterTag;

    // Start is called before the first frame update
    void Start()
    {
        velocity = direction.normalized * speed;
        Destroy(gameObject, 3);
    }


    void FixedUpdate()
    {
        transform.position += (Vector3)(velocity * Time.fixedDeltaTime); //Actualiza la posición
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && shooterTag == "Enemy")
        {
            //PlayerShip player = collision.GetComponent<PlayerShip>();
            PlayerShip player = collision.GetComponentInParent<PlayerShip>();
            if (player != null)
            {
                player.TakeDamage();
            }
            Destroy(gameObject);
        }
    }

}