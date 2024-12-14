using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerShip : MonoBehaviour
{
    public int maxLives = 3;
    public int currentLives;

    private GameHUD gameHUD;
    private bool isDead = false;

    public float moveSpeed = 3;

    public float minX = -0.5f, maxX = 14.5f;
    public float minY = -1.5f, maxY = 7f;

    public GameObject[] bulletPrefabs; // Array de prefabs de balas
    public Transform firePoint;       // Punto desde donde se disparan las balas
    private int currentBulletIndex = 0; // Índice de la bala seleccionada

    public AudioClip[] bulletSounds;
    public AudioSource audioSource;
    public string[] weaponNames = { "Blaster", "Shock Wave", "Shield Disruptor" };
    private string currentWeaponName;

    public float fireRate = 0.5f; //Tiempo entre disparos
    private float nextFireTime = 0f;

    public GameObject shield;
    private bool isShieldActive = false;
    private float shieldTimeRemaining = 0f;

    public GameObject ship;
    public GameObject explosion;
    public AudioClip deathSound;
    public float blinkDuration = 1.0f;
    public float blinkInterval = 0.1f;
    public float explosionDuration = 2.0f;

    public GameObject gameOverCanvas;  // Canvas de Game Over
    public GameObject HUDCanvas;      // Canvas de HUD
    public TMP_Text playerNameText;
    public TMP_Text scoreText;
    public TMP_Text levelText;

    void Start()
    {
        // Inicializando vidas
        currentLives = maxLives;

        shield.SetActive(false);

        // Obtener referencia al HUD
        gameHUD = FindObjectOfType<GameHUD>();
        if (gameHUD != null)
        {
            gameHUD.UpdateLives(currentLives);
        }

        // Iniciar con el HUD visible y Game Over oculto
        HUDCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);

        currentWeaponName = weaponNames[currentBulletIndex];
        if (gameHUD != null)
        {
            gameHUD.UpdateActiveWeapon(currentWeaponName);
        }
    }

    void Update()
    {
        if (isDead) return;

        // Cambiar entre tipos de balas con Tab
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ChangeBullet();
        }

        // Disparar la bala actual con Espacio
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Shoot();
        }

        if (isShieldActive)
        {
            shieldTimeRemaining -= Time.deltaTime;

            if (shieldTimeRemaining <= 0f)
            {
                DeactivateShield();
            }
        }
    }

    void FixedUpdate()
    {
        if (isDead) return; // No permitir movimiento si está muerto

        float moveX = (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) ? -1 : 0) +
                      (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) ? 1 : 0);

        float moveY = (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) ? 1 : 0) +
                      (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) ? -1 : 0);

        Vector2 move = new Vector2(moveX, moveY).normalized * moveSpeed * Time.fixedDeltaTime;

        Vector3 newPosition = transform.position + (Vector3)move;

        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        transform.position = newPosition;
    }

    public void ActivateShield(float duration)
    {
        if (isShieldActive) return; //Evitar que se active si ya está activo

        shieldTimeRemaining = duration;
        shield.SetActive(true); //Activar el escudo
        isShieldActive = true;
    }

    private void DeactivateShield()
    {
        isShieldActive = false;
        shield.SetActive(false); //Desactivar el escudo
    }

    void ChangeBullet()
    {
        currentBulletIndex = (currentBulletIndex + 1) % bulletPrefabs.Length; // Cambia al siguiente tipo de bala
        currentWeaponName = weaponNames[currentBulletIndex];
        if (gameHUD != null)
        {
            gameHUD.UpdateActiveWeapon(currentWeaponName); // Actualizar nombre del arma en el HUD
        }
        Debug.Log("Tipo de bala actual: " + bulletPrefabs[currentBulletIndex].name);
    }

    void Shoot()
    {
        if (bulletPrefabs.Length == 0 || bulletSounds.Length == 0) return;

        nextFireTime = Time.time + fireRate;

        GameObject bullet = Instantiate(bulletPrefabs[currentBulletIndex], firePoint.position, firePoint.rotation);

        Bullets bulletScript = bullet.GetComponent<Bullets>();
        if (bulletScript != null)
        {
            bulletScript.bulletType = currentBulletIndex + 1; // Tipo de bala basado en el índice
        }

        audioSource.clip = bulletSounds[currentBulletIndex];
        audioSource.Play();
    }

    public void TakeDamage()
    {
        if (isShieldActive) return;
        if (currentLives > 0)
        {
            currentLives--; // Reducir vidas
            Debug.Log("Daño recibido");
            gameHUD.UpdateLives(currentLives); // Actualizar HUD

            StartCoroutine(BlinkEffect());

            if (currentLives <= 0)
            {
                // Al quedarse sin vidas (destrucción, game over, etc.)
                Debug.Log("Jugador destruido");
                Die();
            }
        }
    }

    private IEnumerator BlinkEffect()
    {
        SpriteRenderer spriteRenderer = ship.GetComponent<SpriteRenderer>();
        float elapsedTime = 0f;

        while (elapsedTime < blinkDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSecondsRealtime(blinkInterval);
            elapsedTime += blinkInterval;
        }

        spriteRenderer.enabled = true;
    }

    private IEnumerator HandleDeath()
    {
        yield return new WaitForSecondsRealtime(explosionDuration);

        Time.timeScale = 0f;
    }

    private void Die()
    {
        isDead = true;
        ship.SetActive(false);
        explosion.SetActive(true);

        AudioSource.PlayClipAtPoint(deathSound, transform.position, 1.0f);

        StartCoroutine(ShowGameOverAfterExplosion());
    }

    private IEnumerator ShowGameOverAfterExplosion()
    {
        // Espera la duración de la animación de explosión
        yield return new WaitForSecondsRealtime(explosionDuration);

        // Mostrar el canvas de Game Over y ocultar el HUD
        HUDCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);

        // Mostrar el nombre del jugador, puntuación y nivel en la pantalla de Game Over
        playerNameText.text = UserManager.playerName;
        scoreText.text = "Score: " + UserManager.playerScore.ToString();
        levelText.text = "Level: " + UserManager.playerLevel.ToString();

        // Pausar el juego después de la animación
        Time.timeScale = 0f;
    }
}