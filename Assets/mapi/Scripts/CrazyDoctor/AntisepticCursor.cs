using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntisepticCursor : MonoBehaviour
{
    public GameObject vfxPrefab; // ������ VFX, ������� ����� ����������
    public float spawnInterval = 2f; // �������� ����� �������� VFX

    private GameObject canvas;

    private void Start()
    {
        canvas = GameObject.Find("VfxContent");

        // ��������� �������� ��� �������������� ������ VFX
        StartCoroutine(SpawnVFX());
    }

    private IEnumerator SpawnVFX()
    {
        // ���� ��������� �������� � 0.5 �������
        yield return new WaitForSeconds(0.1f);

        while (true)
        {
            // ������� VFX �� ����� �������
            Instantiate(vfxPrefab, transform.position, Quaternion.identity, canvas.transform);

            // ���� ��������� �������� �������
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
