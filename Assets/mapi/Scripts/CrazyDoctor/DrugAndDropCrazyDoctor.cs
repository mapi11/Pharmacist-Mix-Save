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
        // Сохраняем начальную позицию объекта
        initialPosition = transform.position;
    }

    void Update()
    {
        // Проверяем, нажата ли левая кнопка мыши
        if (Input.GetMouseButtonDown(0))
        {
            // Получаем мировые координаты мыши
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            // Проверяем, находится ли мышь над объектом
            Collider2D collider = GetComponent<Collider2D>();
            if (collider.OverlapPoint(mousePosition))
            {
                isDragging = true;
            }
        }

        // Проверяем, если левая кнопка мыши отпущена
        if (Input.GetMouseButtonUp(0))
        {
            bgActive.SetActive(false);
            bgDisable.SetActive(true);

            isDragging = false;
            // Возвращаем объект в начальную позицию
            transform.position = initialPosition;
        }

        // Если объект перетаскивается, следим за движением мыши
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
