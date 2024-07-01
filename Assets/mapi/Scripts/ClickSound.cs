using UnityEngine;

public class ClickSound : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        // �������� ��������� AudioSource � �������
        audioSource = GetComponent<AudioSource>();

        // ���� ���������� AudioSource ���, ������� �������������� � �������
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource component not found on GameObject. Please add an AudioSource component.");
        }
    }

    void OnMouseDown()
    {
        // ��������� ������� ���������� AudioSource
        if (audioSource != null)
        {
            // ������������� ����, ���� AudioSource ��� �������� �� ���� �������
            audioSource.Play();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // ������������� ���� ��� �������� �������
        PlayClickSound();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // ������������� ���� ��� ����� � �������
        PlayClickSound();
    }

    private void PlayClickSound()
    {
        // ��������� ������� ���������� AudioSource
        if (audioSource != null)
        {
            // ������������� ����
            audioSource.Play();
        }
    }
}