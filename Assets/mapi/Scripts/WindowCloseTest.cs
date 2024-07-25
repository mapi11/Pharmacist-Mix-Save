using DG.Tweening;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class WindowCloseTest : MonoBehaviour
{
    [SerializeField] private Button btnClose;
    [Space]
    [SerializeField] private Vector2 PosClose;

    WindowAnimation windowAnimation;

    private void Start()
    {
        btnClose.onClick.AddListener(() => windowCloseAnim(PosClose));

        windowAnimation = FindAnyObjectByType<WindowAnimation>();
    }

    private void windowCloseAnim(Vector2 vector)
    {
        RectTransform windowTransform = gameObject.GetComponent<RectTransform>();

        // Анимация ухода окна с экрана
        windowTransform.DOAnchorPos(vector, 1f).SetEase(Ease.InBack).OnComplete(() => DestroyImmediate(windowTransform.gameObject, windowAnimation.isOpen = false));
    }
}
