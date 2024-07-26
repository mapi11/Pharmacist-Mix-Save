using DG.Tweening;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class WindowAnimationAwake : MonoBehaviour
{
    [Header("Window")]
    [Space]
    [SerializeField] private Vector2 PosWindow;
    [SerializeField] private float showSpeed = 1f;

    [HideInInspector]
    public bool isOpen;

    //[Space]
    //[SerializeField] private Transform canvas;

    private void Awake()
    {
        windowOpenAnim(PosWindow);
    }

    private void windowOpenAnim(Vector2 vector)
    {
        if (isOpen == false)
        {
            RectTransform windowTransform = gameObject.GetComponent<RectTransform>();

            // Анимация выезда окна на экран
            windowTransform.anchoredPosition = vector;
            windowTransform.DOAnchorPos(Vector2.zero, showSpeed).SetEase(Ease.OutBack);

            isOpen = true;
        }
    }

    private void windowCloseAnim(Vector2 vector)
    {
        if (isOpen == true)
        {
            RectTransform windowTransform = gameObject.GetComponent<RectTransform>();

            // Анимация ухода окна с экрана
            windowTransform.DOAnchorPos(vector, 1f).SetEase(Ease.InBack).OnComplete(() => DestroyImmediate(gameObject));

            isOpen = false;
        }
    }
}
