using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public AudioClip soundToPlay; // Assigned via Inspector
    public float volume = 1f;
    public int damage = 10;
    public Vector2 moveSpeed = new Vector2(3f, 0);
    public Vector2 knockback = Vector2.zero;

    public float destroyDelay = 5f;

    public GameObject hitEffectPrefab; // Prefab for the hit explosion effect

    private Rigidbody2D rb;

    public string blockTag = "blocktag"; // Tag for the blocking object

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (soundToPlay == null)
        {
            Debug.Log("Sound does not exist");
        }
    }

    void Start()
    {
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);       
        Destroy(gameObject, destroyDelay);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(blockTag))
        {
            Debug.Log("Attack blocked by " + collision.name);
            Instantiate(hitEffectPrefab, transform.position, transform.rotation);
            if (soundToPlay != null)
            {
                AudioSource.PlayClipAtPoint(soundToPlay, transform.position, volume);
            }
            Destroy(gameObject);
            return;
        }

        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            bool gotHit = damageable.Hit(damage, knockback);

            if (gotHit)
            {
                Debug.Log(collision.name + " hit for " + damage);
                Instantiate(hitEffectPrefab, transform.position, transform.rotation);
                if (soundToPlay != null)
                {
                    AudioSource.PlayClipAtPoint(soundToPlay, transform.position, volume);
                }
                Destroy(gameObject);
            }
        }
    }
}
