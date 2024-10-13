using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntisepticZone : MonoBehaviour
{
    public GameObject prefabToSpawn; // Префаб, который будет спавниться
    public float requiredTimeInTrigger = 5f; // Время, которое объект должен провести в триггере

    public float timeInTrigger = 0f; // Счетчик времени, проведенного в триггере
    public bool isObjectInside = false; // Флаг для проверки нахождения объекта в триггере
    public bool objectSpawned = false; // Флаг, что объект был заспавнен

    private void Update()
    {
        // Если объект внутри триггера и объект ещё не был заспавнен
        if (isObjectInside && !objectSpawned)
        {
            timeInTrigger += Time.deltaTime;

            // Если время в триггере достигло требуемого, спавним объект
            if (timeInTrigger >= requiredTimeInTrigger)
            {
                Instantiate(prefabToSpawn, transform.position, Quaternion.identity,gameObject.transform);
                objectSpawned = true; // Отмечаем, что объект был заспавнен
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
