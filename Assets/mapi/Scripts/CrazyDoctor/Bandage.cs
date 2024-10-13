using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bandage : MonoBehaviour
{
    public Transform canvas; // Канвас для размещения линий
    public GameObject linePrefab; // Префаб линии
    public GameObject lineBandagePrefab; // Префаб линии

    public Button toggleButton; // Кнопка для управления рисованием
    public Transform[] triggerZones; // Массив триггер зон

    [Space]
    public GameObject background; // Фон
    public Image toolImage; // Изображение инструмента

    public bool canDraw = false; // Флаг, указывающий, можно ли рисовать
    private bool isAnimating = false; // Флаг для проверки выполнения анимации
    private LineRenderer lineRenderer;
    private bool isDrawing = false;
    private List<Transform> touchedTriggers = new List<Transform>();
    private HashSet<string> existingConnections = new HashSet<string>();

    InstrumentsController instrumentsController;

    void Start()
    {
        instrumentsController = FindAnyObjectByType<InstrumentsController>();

        // Создаём объект для линии и настраиваем его
        GameObject lineObject = Instantiate(linePrefab, canvas);
        lineRenderer = lineObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;

        // Назначаем метод ToggleDrawingMode на событие нажатия кнопки
        toggleButton.onClick.AddListener(ToggleDrawingMode);
    }

    void Update()
    {
        if (canDraw && Input.GetMouseButtonDown(0))
        {
            StartDrawing();
        }

        if (isDrawing)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            if (lineRenderer.positionCount == 0 || Vector3.Distance(lineRenderer.GetPosition(lineRenderer.positionCount - 1), mousePosition) > 0.1f)
            {
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, mousePosition);
            }

            // Проверяем пересечения с триггерными зонами
            foreach (var trigger in triggerZones)
            {
                if (trigger.GetComponent<Collider2D>().OverlapPoint(mousePosition) && !touchedTriggers.Contains(trigger))
                {
                    touchedTriggers.Add(trigger);
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && isDrawing)
        {
            StopDrawing();
        }
    }

    // Метод для начала рисования линии
    private void StartDrawing()
    {
        isDrawing = true;
        touchedTriggers.Clear();
        lineRenderer.positionCount = 0;
    }

    // Метод для остановки рисования линии
    private void StopDrawing()
    {
        isDrawing = false;
        ClearLine();

        // Создаем соединительные линии между задействованными точками
        if (touchedTriggers.Count >= 2)
        {
            for (int i = 0; i < touchedTriggers.Count - 1; i++)
            {
                Transform start = touchedTriggers[i];
                Transform end = touchedTriggers[i + 1];

                // Проверяем типы точек
                if (CanConnect(start, end))
                {
                    string connectionKey = GetConnectionKey(start, end);

                    if (!existingConnections.Contains(connectionKey))
                    {
                        CreateConnector(start.position, end.position);
                        existingConnections.Add(connectionKey);
                    }
                }
            }
        }
    }

    // Удаляет линию
    private void ClearLine()
    {
        lineRenderer.positionCount = 0;
    }

    // Создание линии между точками
    private void CreateConnector(Vector3 startPoint, Vector3 endPoint)
    {
        GameObject connector = Instantiate(lineBandagePrefab, canvas);
        LineRenderer connectorLine = connector.GetComponent<LineRenderer>();
        connectorLine.positionCount = 2;
        connectorLine.SetPosition(0, startPoint);
        connectorLine.SetPosition(1, endPoint);
    }

    // Проверка, можно ли соединять точки
    private bool CanConnect(Transform start, Transform end)
    {
        string startTag = start.tag;
        string endTag = end.tag;

        // Левую с правой и наоборот
        return (startTag == "BandageLeft" && endTag == "BandageRight") || (startTag == "BandageRight" && endTag == "BandageLeft");
    }

    // Генерация ключа для соединения
    private string GetConnectionKey(Transform start, Transform end)
    {
        string startName = start.name;
        string endName = end.name;
        return string.Compare(startName, endName) < 0 ? startName + "-" + endName : endName + "-" + startName;
    }

    // Переключает режим рисования
    private void ToggleDrawingMode()
    {
        if (isAnimating) return; // Если анимация уже идёт, выходим из метода

        isAnimating = true; // Устанавливаем флаг, чтобы предотвратить повторные вызовы

        canDraw = !canDraw;

        // Анимация изображения инструмента
        toolImage.transform.DOShakeScale(0.3f, 0.3f, 20, 90, true)
            .OnComplete(() => isAnimating = false); // Сброс флага после завершения анимации

        // Активация или деактивация фона
        background.SetActive(canDraw);

        if (canDraw)
        {
            instrumentsController.BandageActive();

        }
    }


    public void ToolDisable()
    {
        canDraw = false;

        background.SetActive(false);
    }
}
