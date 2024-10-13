using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bandage : MonoBehaviour
{
    public Transform canvas; // ������ ��� ���������� �����
    public GameObject linePrefab; // ������ �����
    public GameObject lineBandagePrefab; // ������ �����

    public Button toggleButton; // ������ ��� ���������� ����������
    public Transform[] triggerZones; // ������ ������� ���

    [Space]
    public GameObject background; // ���
    public Image toolImage; // ����������� �����������

    public bool canDraw = false; // ����, �����������, ����� �� ��������
    private bool isAnimating = false; // ���� ��� �������� ���������� ��������
    private LineRenderer lineRenderer;
    private bool isDrawing = false;
    private List<Transform> touchedTriggers = new List<Transform>();
    private HashSet<string> existingConnections = new HashSet<string>();

    InstrumentsController instrumentsController;

    void Start()
    {
        instrumentsController = FindAnyObjectByType<InstrumentsController>();

        // ������ ������ ��� ����� � ����������� ���
        GameObject lineObject = Instantiate(linePrefab, canvas);
        lineRenderer = lineObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;

        // ��������� ����� ToggleDrawingMode �� ������� ������� ������
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

            // ��������� ����������� � ����������� ������
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

    // ����� ��� ������ ��������� �����
    private void StartDrawing()
    {
        isDrawing = true;
        touchedTriggers.Clear();
        lineRenderer.positionCount = 0;
    }

    // ����� ��� ��������� ��������� �����
    private void StopDrawing()
    {
        isDrawing = false;
        ClearLine();

        // ������� �������������� ����� ����� ���������������� �������
        if (touchedTriggers.Count >= 2)
        {
            for (int i = 0; i < touchedTriggers.Count - 1; i++)
            {
                Transform start = touchedTriggers[i];
                Transform end = touchedTriggers[i + 1];

                // ��������� ���� �����
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

    // ������� �����
    private void ClearLine()
    {
        lineRenderer.positionCount = 0;
    }

    // �������� ����� ����� �������
    private void CreateConnector(Vector3 startPoint, Vector3 endPoint)
    {
        GameObject connector = Instantiate(lineBandagePrefab, canvas);
        LineRenderer connectorLine = connector.GetComponent<LineRenderer>();
        connectorLine.positionCount = 2;
        connectorLine.SetPosition(0, startPoint);
        connectorLine.SetPosition(1, endPoint);
    }

    // ��������, ����� �� ��������� �����
    private bool CanConnect(Transform start, Transform end)
    {
        string startTag = start.tag;
        string endTag = end.tag;

        // ����� � ������ � ��������
        return (startTag == "BandageLeft" && endTag == "BandageRight") || (startTag == "BandageRight" && endTag == "BandageLeft");
    }

    // ��������� ����� ��� ����������
    private string GetConnectionKey(Transform start, Transform end)
    {
        string startName = start.name;
        string endName = end.name;
        return string.Compare(startName, endName) < 0 ? startName + "-" + endName : endName + "-" + startName;
    }

    // ����������� ����� ���������
    private void ToggleDrawingMode()
    {
        if (isAnimating) return; // ���� �������� ��� ���, ������� �� ������

        isAnimating = true; // ������������� ����, ����� ������������� ��������� ������

        canDraw = !canDraw;

        // �������� ����������� �����������
        toolImage.transform.DOShakeScale(0.3f, 0.3f, 20, 90, true)
            .OnComplete(() => isAnimating = false); // ����� ����� ����� ���������� ��������

        // ��������� ��� ����������� ����
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
