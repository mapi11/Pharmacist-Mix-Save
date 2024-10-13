using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Antiseptic : MonoBehaviour
{
    public GameObject background; // Фон
    public Image toolImage; // Изображение инструмента
    public GameObject cursorFollowerPrefab; // Префаб, который будет следовать за курсором

    public Transform canvas;

    public bool toolActive = false; // Булевая переменная ToolActive
    private bool isAnimating = false; // Флаг для проверки выполнения анимации
    private GameObject currentFollower; // Текущий объект, следующий за курсором

    InstrumentsController instrumentsController;
    void Start()
    {
        instrumentsController = FindAnyObjectByType<InstrumentsController>();

        // Изначально фон не активен
        background.SetActive(false);
    }

    void Update()
    {
        // Проверка нажатия ЛКМ и активации инструмента
        if (toolActive && Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            // Если объект, следующий за курсором, не существует, создаём новый
            if (currentFollower == null)
            {
                currentFollower = Instantiate(cursorFollowerPrefab, mousePosition, Quaternion.identity, canvas);
            }
            else
            {
                // Обновляем позицию объекта, чтобы он следовал за курсором
                currentFollower.transform.position = mousePosition;
            }
        }
        else
        {
            // Удаляем объект, когда кнопка мыши отпущена
            if (currentFollower != null)
            {
                Destroy(currentFollower);
            }
        }
    }

    // Метод для обработки нажатия на кнопку
    public void OnToolButtonPressed()
    {
        if (isAnimating) return; // Если анимация уже идёт, выходим из метода

        isAnimating = true; // Устанавливаем флаг, чтобы предотвратить повторные вызовы

        toolActive = !toolActive;

        // Анимация изображения инструмента
        toolImage.transform.DOShakeScale(0.3f, 0.3f, 20, 90, true).OnComplete(() => isAnimating = false); // Сброс флага после завершения анимации

        // Активация или деактивация фона
        background.SetActive(toolActive);

        if (toolActive)
        {
            instrumentsController.AntisepticActive();

        }
    }

    public void ToolDisable()
    {
        toolActive = false;

        background.SetActive(false);
    }
}
