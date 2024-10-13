using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Antiseptic : MonoBehaviour
{
    public GameObject background; // ���
    public Image toolImage; // ����������� �����������
    public GameObject cursorFollowerPrefab; // ������, ������� ����� ��������� �� ��������

    public Transform canvas;

    public bool toolActive = false; // ������� ���������� ToolActive
    private bool isAnimating = false; // ���� ��� �������� ���������� ��������
    private GameObject currentFollower; // ������� ������, ��������� �� ��������

    InstrumentsController instrumentsController;
    void Start()
    {
        instrumentsController = FindAnyObjectByType<InstrumentsController>();

        // ���������� ��� �� �������
        background.SetActive(false);
    }

    void Update()
    {
        // �������� ������� ��� � ��������� �����������
        if (toolActive && Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            // ���� ������, ��������� �� ��������, �� ����������, ������ �����
            if (currentFollower == null)
            {
                currentFollower = Instantiate(cursorFollowerPrefab, mousePosition, Quaternion.identity, canvas);
            }
            else
            {
                // ��������� ������� �������, ����� �� �������� �� ��������
                currentFollower.transform.position = mousePosition;
            }
        }
        else
        {
            // ������� ������, ����� ������ ���� ��������
            if (currentFollower != null)
            {
                Destroy(currentFollower);
            }
        }
    }

    // ����� ��� ��������� ������� �� ������
    public void OnToolButtonPressed()
    {
        if (isAnimating) return; // ���� �������� ��� ���, ������� �� ������

        isAnimating = true; // ������������� ����, ����� ������������� ��������� ������

        toolActive = !toolActive;

        // �������� ����������� �����������
        toolImage.transform.DOShakeScale(0.3f, 0.3f, 20, 90, true).OnComplete(() => isAnimating = false); // ����� ����� ����� ���������� ��������

        // ��������� ��� ����������� ����
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
