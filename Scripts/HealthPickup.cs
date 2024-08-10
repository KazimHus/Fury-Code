using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthRestore = 20;
    public float volume = 1f;
    public Vector3 spinRotationSpeed = new Vector3(0, 100, 0);

    public AudioClip soundToPlay;  // Assign this in the Unity Inspector

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        
        if (!soundToPlay)
        {
            Debug.LogWarning("Sound not assigned in the Inspector.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable)
        {
            bool wasHealed = damageable.Heal(healthRestore);

            if (wasHealed)
            {
                if (soundToPlay)
                {
                    AudioSource.PlayClipAtPoint(soundToPlay, gameObject.transform.position, volume);
                }
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
}
