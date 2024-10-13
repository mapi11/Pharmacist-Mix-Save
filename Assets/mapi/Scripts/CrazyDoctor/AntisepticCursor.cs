using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntisepticCursor : MonoBehaviour
{
    public GameObject vfxPrefab; // Префаб VFX, который будет спавниться
    public float spawnInterval = 2f; // Интервал между спавнами VFX

    private GameObject canvas;

    private void Start()
    {
        canvas = GameObject.Find("VfxContent");

        // Запускаем корутину для периодического спавна VFX
        StartCoroutine(SpawnVFX());
    }

    private IEnumerator SpawnVFX()
    {
        // Ждем начальную задержку в 0.5 секунды
        yield return new WaitForSeconds(0.1f);

        while (true)
        {
            // Спавним VFX на месте объекта
            Instantiate(vfxPrefab, transform.position, Quaternion.identity, canvas.transform);

            // Ждем указанный интервал времени
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
