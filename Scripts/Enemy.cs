using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header ("Audio Settings")] 
    [SerializeField] AudioClip lazerSound;
    [SerializeField] float lazerVolume = 1f;
    [SerializeField] AudioClip destroySound;
    [SerializeField] float deathVolume = 1f;

    [Header ("Enemy Settings")]
    [SerializeField] float health = 200;
    int scoreValue = 42;
    [SerializeField] GameObject enemyLazer;
    [SerializeField] float lazerSpeed = 10f;
    [SerializeField] GameObject explosionVFX;
    
    float lifeTimeOfExplosion = 1f;
    float shotCounter;
    float minTimeBetweenShots = 0.1f;
    float maxTimeBetweenShots = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;

        if(shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }
    private void Fire()
    {
        GameObject lazer = Instantiate(enemyLazer, transform.position, Quaternion.identity);
        lazer.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -lazerSpeed);
        AudioSource.PlayClipAtPoint(lazerSound, Camera.main.transform.position, lazerVolume);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer)
            return;
        ProcessHit(damageDealer);

    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(explosionVFX, transform.position, Quaternion.identity);
        Destroy(explosion, lifeTimeOfExplosion);
        AudioSource.PlayClipAtPoint(destroySound, Camera.main.transform.position, deathVolume);
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
    }
}
