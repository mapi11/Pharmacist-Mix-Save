using DG.Tweening;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class WindowAnimation : MonoBehaviour
{
    [Header("Window")]
    [Space]
    [SerializeField] private GameObject window1;
    [SerializeField] private Vector2 PosWindow1;
    [SerializeField] private float showSpeed = 1.5f;

    private GameObject newWindow1;

    [HideInInspector]
    public bool isOpen;

    [Header("Button")]
    [Space]
    [SerializeField] private Button btn1;

    [Space]
    [SerializeField] private Transform canvas;

    private void Awake()
    {
        btn1.onClick.AddListener(() => windowOpenAnim(window1, PosWindow1));
    }

    private void windowOpenAnim(GameObject window, Vector2 vector)
    {
        if (isOpen == false)
        {
            GameObject newWindow = Instantiate(window, vector, Quaternion.identity, canvas);
            RectTransform windowTransform = newWindow.GetComponent<RectTransform>();

            // �������� ������ ���� �� �����
            windowTransform.anchoredPosition = vector;
            windowTransform.DOAnchorPos(Vector2.zero, showSpeed).SetEase(Ease.OutBack);

            isOpen = true;
            newWindow1 = newWindow;
        }
    }

    private void windowCloseAnim(GameObject window, Vector2 vector)
    {

        if (isOpen == true)
        {
            RectTransform windowTransform = newWindow1.GetComponent<RectTransform>();

            // �������� ����� ���� � ������
            windowTransform.DOAnchorPos(vector, showSpeed).SetEase(Ease.InBack).OnComplete(() => DestroyImmediate(windowTransform.gameObject));

            isOpen = false;
        }
    }
}