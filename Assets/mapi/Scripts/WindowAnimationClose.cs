using DG.Tweening;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class WindowAnimationClose : MonoBehaviour
{
    [SerializeField] private Button btnClose;
    [Space]
    [SerializeField] private Vector2 PosClose;
    [SerializeField] private float closeSpeed = 1.5f;
    [Space]
    [SerializeField] private bool Done;
    [Header("Other window")]
    [SerializeField] private GameObject OtherWindow;


    WindowAnimation windowAnimation;

    private void Start()
    {
        btnClose.onClick.AddListener(() => windowCloseAnim(PosClose));

        windowAnimation = FindAnyObjectByType<WindowAnimation>();
    }

    private void windowCloseAnim(Vector2 vector)
    {
        Done = true;
        if (OtherWindow != null)
        {
            OtherWindow.SetActive(true);
        }
        

        RectTransform windowTransform = gameObject.GetComponent<RectTransform>();

        // �������� ����� ���� � ������
        windowTransform.DOAnchorPos(vector, closeSpeed).SetEase(Ease.InBack).OnComplete(() => DestroyImmediate(windowTransform.gameObject, windowAnimation.isOpen = false));

    }
}