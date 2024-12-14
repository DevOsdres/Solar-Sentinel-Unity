using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public float nebulaSpeed = 0.1f; // Velocidad del fondo Nebula
    public float starsSpeed = 0.2f;  // Velocidad de las estrellas pequeñas
    public Transform nebula;         // Referencia al objeto Nebula
    public Transform stars;          // Referencia al objeto Stars

    private Vector3 nebulaStartPos;
    private Vector3 starsStartPos;

    void Start()
    {
        // Guardar las posiciones iniciales para el reposicionamiento
        nebulaStartPos = nebula.position;
        starsStartPos = stars.position;
    }

    void Update()
    {
        // Mover el fondo de la nebulosa hacia la izquierda
        nebula.position += Vector3.left * nebulaSpeed * Time.deltaTime;

        // Mover las estrellas hacia la izquierda
        stars.position += Vector3.left * starsSpeed * Time.deltaTime;

        // Si la nebulosa ha salido completamente de la pantalla, reposicionar al inicio
        if (nebula.position.x <= -nebula.GetComponent<SpriteRenderer>().bounds.size.x)
        {
            nebula.position = nebulaStartPos;
        }

        // Si las estrellas han salido completamente de la pantalla, reposicionar al inicio
        if (stars.position.x <= -stars.GetComponent<SpriteRenderer>().bounds.size.x)
        {
            stars.position = starsStartPos;
        }
    }
}
