using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Health")]
    [SerializeField] int playerHealth = 100;

    [Header("Player Movement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 30f;
    [SerializeField] float projectileFiringPeriod = 0.05f;

    [Header("Audio")]
    [SerializeField] AudioClip deathSFX;
    [Range(0.0f, 1.0f)] [SerializeField] float deathVolume = 0.75f;
    [SerializeField] AudioClip laserSFX;
    [Range(0.0f, 1.0f)] [SerializeField] float laserVolume = 0.25f;
    // [Header("Level Loader")]
    // [SerializeField] GameObject levelLoader;

    Coroutine firingCoroutine;

    private float xMin, xMax, yMin, yMax;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            AudioSource.PlayClipAtPoint(laserSFX, Camera.main.transform.position, laserVolume);
            GameObject laser = Instantiate(
                laserPrefab,
                transform.position,
                Quaternion.identity
            ) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity =
                new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer dd)
    {
        playerHealth -= dd.GetDamage();
        dd.Hit();
        if (playerHealth <= 0)
            Die();
    }
    
    private void Die()
    {
        playerHealth = 0;
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathVolume);
        FindObjectOfType<Level>().LoadGameOver();
        // levelLoader.GetComponent<Level>().LoadGameOver();
        Destroy(gameObject);
    }

    public int GetPlayerHealth()
    {
        return playerHealth;
    }
}
