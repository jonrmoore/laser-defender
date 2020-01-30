using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;

    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 2f;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float explosionDuration = 1f;
    [SerializeField] int pointsAwardedOnDestroy = 53;

    [Header("Audio")]
    [SerializeField] AudioClip deathSFX;
    [Range(0.0f, 1.0f)] [SerializeField] float deathVolume = 1f;
    [SerializeField] AudioClip laserSFX;
    [Range(0.0f, 1.0f)] [SerializeField] float laserVolume = 0.25f;

    void Start() { shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots); }

    void Update() { CountDownAndShoot(); }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        { 
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        AudioSource.PlayClipAtPoint(laserSFX, Camera.main.transform.position, laserVolume);
        GameObject laser = Instantiate(
            laserPrefab,
            transform.position,
            Quaternion.identity
        ) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity =
            new Vector2(0, -projectileSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);
        Destroy(other.gameObject);
    }

    private void ProcessHit(DamageDealer dd)
    {
        health -= dd.GetDamage();
        if (health <= 0)
            Die();
    }

    private void Die()
    {
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathVolume);
        Destroy(gameObject);
        GameObject explosion = Instantiate(
            deathVFX,
            transform.position,
            transform.rotation
        ) as GameObject;
        FindObjectOfType<GameSession>().AddToScore(pointsAwardedOnDestroy);
        Destroy(explosion, explosionDuration);
    }
}
