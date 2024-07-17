using UnityEngine;

public class ClickSound : MonoBehaviour
{
    private AudioSource audioSource;
    private float lastPlayTime = 0f;
    private float playCooldown = 0.25f;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnMouseDown()
    {
        if (audioSource != null)
        {
            PlayClickSound();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        PlayClickSound();
    }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     // Воспроизводим звук при входе в триггер
    //     PlayClickSound();
    // }

    private void PlayClickSound()
    {
        if (audioSource != null && Time.time >= lastPlayTime + playCooldown)
        {
            audioSource.Play();

            lastPlayTime = Time.time;
        }
    }
}