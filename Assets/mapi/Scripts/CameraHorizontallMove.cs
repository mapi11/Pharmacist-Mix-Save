using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHorizontallMove : MonoBehaviour
{
    [Header("Min/max speed")]
    public float maxMoveSpeed = 10f;
    public float minMoveSpeed = 1f;

    [Header("Left/right border")]
    public Transform leftBoundary; // ����� ������
    public Transform rightBoundary; // ������ ������

    [Header("MouseZoneWidth")]
    public float edgeZoneWidth = 200f;

    public bool _canMoove;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (_canMoove == true)
        {
         Vector3 mousePosition = Input.mousePosition;
                float screenWidth = Screen.width;

                // ���������, ��������� �� ������ � ����� ����
                if (mousePosition.x < edgeZoneWidth)
                {
                    // �������� ������ �� ����� �������
                    float distanceFromEdge = edgeZoneWidth - mousePosition.x;
                    float moveSpeed = Mathf.Lerp(minMoveSpeed, maxMoveSpeed, distanceFromEdge / edgeZoneWidth);

                    // �������� ������ �� ����� �������
                    if (transform.position.x > leftBoundary.position.x)
                    {
                        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
                    }
                }
                // ���������, ��������� �� ������ � ������ ����
                else if (mousePosition.x > screenWidth - edgeZoneWidth)
                {
                    // ��������� �������� �� ������ ���������� �� ������ �������
                    float distanceFromEdge = mousePosition.x - (screenWidth - edgeZoneWidth);
                    float moveSpeed = Mathf.Lerp(minMoveSpeed, maxMoveSpeed, distanceFromEdge / edgeZoneWidth);

                    // �������� ������ �� ������ �������
                    if (transform.position.x < rightBoundary.position.x)
                    {
                        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
                    }
                }
        }
    }
}
