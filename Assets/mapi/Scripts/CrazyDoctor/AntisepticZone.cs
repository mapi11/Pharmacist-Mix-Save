using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntisepticZone : MonoBehaviour
{
    public GameObject prefabToSpawn; // ������, ������� ����� ����������
    public float requiredTimeInTrigger = 5f; // �����, ������� ������ ������ �������� � ��������

    public float timeInTrigger = 0f; // ������� �������, ������������ � ��������
    public bool isObjectInside = false; // ���� ��� �������� ���������� ������� � ��������
    public bool objectSpawned = false; // ����, ��� ������ ��� ���������

    private void Update()
    {
        // ���� ������ ������ �������� � ������ ��� �� ��� ���������
        if (isObjectInside && !objectSpawned)
        {
            timeInTrigger += Time.deltaTime;

            // ���� ����� � �������� �������� ����������, ������� ������
            if (timeInTrigger >= requiredTimeInTrigger)
            {
                Instantiate(prefabToSpawn, transform.position, Quaternion.identity,gameObject.transform);
                objectSpawned = true; // ��������, ��� ������ ��� ���������
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isObjectInside = true;
            //timeInTrigger = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isObjectInside = false;
            //timeInTrigger = 0f;
        }
    }
}
