using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; //Prefab del enemigo
    public Transform[] spawnPoints; //Array de puntos de spawn
    public float spawnInterval = 2f; //Intervalo entre spawns
    public int maxEnemiesOnScreen = 10; //Máximo de enemigos activos
    private int currentEnemies = 0; //Contador de enemigos activos

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Solo creamos nuevos enemigos si el número actual es menor que el máximo
            if (currentEnemies < maxEnemiesOnScreen)
            {
                // Seleccionamos un punto de spawn aleatorio
                int randomIndex = Random.Range(0, spawnPoints.Length);
                // Instanciamos un nuevo enemigo en la posición del punto de spawn seleccionado
                GameObject enemy = Instantiate(enemyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
                currentEnemies++;

                // Aseguramos que el enemigo tenga acceso al spawner para notificar cuando se destruya
                EnemyType1 enemyScript = enemy.GetComponent<EnemyType1>();
                if (enemyScript != null)
                {
                    // Suscribimos al evento para que se actualice el contador de enemigos al destruirlo
                    enemyScript.onEnemyDestroyed += EnemyDestroyed;
                }
            }

            // Esperamos el intervalo de tiempo antes de generar el siguiente enemigo
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Método que se llama cuando un enemigo se destruye
    public void EnemyDestroyed()
    {
        currentEnemies--; // Reducir el contador de enemigos
    }
}