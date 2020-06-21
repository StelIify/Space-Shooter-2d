using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] int health = 300;
    [Header("Player's Moves and Bounderies")]
    [SerializeField] float moveSpeed = 15f;
    [SerializeField] float xPadding = 0.5f;
    [SerializeField] float yPadding = 0.5f;
    [Header("Lazer Settings")]
    [SerializeField] GameObject playerLaser;
    [SerializeField] float lazerSpeed = 10f;
    [SerializeField] float waitTime = 0.1f;
    [Header("Audio Settings")]
    [SerializeField] AudioClip lazerSound;
    [SerializeField] float lazerVolume = 0.5f;
    [SerializeField] AudioClip playerDeath;
    [SerializeField] [Range(0.1f, 1f)] float deathVolume = 1f;
    [Header("Player VFX")]
    [SerializeField] GameObject playerVFX;
    float durationOfExplosion = 1f;

    Coroutine fireCoroutine;
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    // Start is called before the first frame update
    void Start()
    {
       
        SetUpMoveBounderies();
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
            fireCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine);
        }
    }
    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject lazer = Instantiate(playerLaser, transform.position, Quaternion.identity);
            lazer.GetComponent<Rigidbody2D>().velocity = new Vector2(0, lazerSpeed);
            AudioSource.PlayClipAtPoint(lazerSound, Camera.main.transform.position, lazerVolume);
            yield return new WaitForSeconds(waitTime);
        }
        
    }
    private void SetUpMoveBounderies()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xPadding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + yPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - yPadding;
    }
    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
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
        GameObject playerExplosion = Instantiate(playerVFX, transform.position, Quaternion.identity);
        Destroy(gameObject, durationOfExplosion);
        AudioSource.PlayClipAtPoint(playerDeath, Camera.main.transform.position, deathVolume);
        FindObjectOfType<LevelLoading>().GameOver();
    }

    public int GetHealth()
    {
        return health;
    }
}
