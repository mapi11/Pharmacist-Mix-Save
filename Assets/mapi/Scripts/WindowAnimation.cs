using DG.Tweening;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class WindowAnimation : MonoBehaviour
{
    [Header("Window")]
    [Space]
    [SerializeField] private GameObject window1;
    [SerializeField] private Vector2 PosWindow1;
    private GameObject newWindow1;
    private bool isOpen;
    private bool isClosing;

    [Header("Button")]
    [Space]
    [SerializeField] private Button btn1;
    [SerializeField] private Button closeBtn1;

    [Space]
    [SerializeField] private Transform canvas;

    private void Awake()
    {
        btn1.onClick.AddListener(() => windowOpenAnim(window1, PosWindow1));

        closeBtn1.onClick.AddListener(() => windowCloseAnim(window1, PosWindow1));
    }

    private void windowOpenAnim(GameObject window, Vector2 vector)
    {
        if (isOpen == false)
        {
            GameObject newWindow = Instantiate(window, vector, Quaternion.identity, canvas);
            RectTransform windowTransform = newWindow.GetComponent<RectTransform>();

            // Анимация выезда окна на экран
            windowTransform.anchoredPosition = vector;
            windowTransform.DOAnchorPos(Vector2.zero, 1f).SetEase(Ease.OutBack);

            isOpen = true;
            newWindow1 = newWindow;
        }
    }

    private void windowCloseAnim(GameObject window, Vector2 vector)
    {

        if (isOpen == true)
        {
            RectTransform windowTransform = newWindow1.GetComponent<RectTransform>();

            // Анимация ухода окна с экрана
            windowTransform.DOAnchorPos(vector, 1f).SetEase(Ease.InBack).OnComplete(() => DestroyImmediate(windowTransform.gameObject));

            isOpen = false;
        }
    }
}