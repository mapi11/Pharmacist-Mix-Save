using UnityEngine;

public class ClickSound : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        // Получаем компонент AudioSource с объекта
        audioSource = GetComponent<AudioSource>();

        // Если компонента AudioSource нет, выводим предупреждение в консоль
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource component not found on GameObject. Please add an AudioSource component.");
        }
    }

    void OnMouseDown()
    {
        // Проверяем наличие компонента AudioSource
        if (audioSource != null)
        {
            // Воспроизводим звук, если AudioSource уже настроен на этом объекте
            audioSource.Play();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Воспроизводим звук при коллизии объекта
        PlayClickSound();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Воспроизводим звук при входе в триггер
        PlayClickSound();
    }

    private void PlayClickSound()
    {
        // Проверяем наличие компонента AudioSource
        if (audioSource != null)
        {
            // Воспроизводим звук
            audioSource.Play();
        }
    }
}