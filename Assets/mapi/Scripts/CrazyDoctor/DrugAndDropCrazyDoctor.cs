using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugAndDropCrazyDoctor : MonoBehaviour
{
    private Vector3 initialPosition;
    public bool isDragging = false;

    [Space]
    public GameObject bgActive;
    public GameObject bgDisable;

    InstrumentsController instrumentsController;

    void Start()
    {
        instrumentsController = FindAnyObjectByType<InstrumentsController>();
        // ��������� ��������� ������� �������
        initialPosition = transform.position;
    }

    void Update()
    {
        // ���������, ������ �� ����� ������ ����
        if (Input.GetMouseButtonDown(0))
        {
            // �������� ������� ���������� ����
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            // ���������, ��������� �� ���� ��� ��������
            Collider2D collider = GetComponent<Collider2D>();
            if (collider.OverlapPoint(mousePosition))
            {
                isDragging = true;
            }
        }

        // ���������, ���� ����� ������ ���� ��������
        if (Input.GetMouseButtonUp(0))
        {
            bgActive.SetActive(false);
            bgDisable.SetActive(true);

            isDragging = false;
            // ���������� ������ � ��������� �������
            transform.position = initialPosition;
        }

        // ���� ������ ���������������, ������ �� ��������� ����
        if (isDragging)
        {
            instrumentsController.ScalpelActive();

            bgActive.SetActive(true);
            bgDisable.SetActive(false);

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = mousePosition;
        }
    }
}
