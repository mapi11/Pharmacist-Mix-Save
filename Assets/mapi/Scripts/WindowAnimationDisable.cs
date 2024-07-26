using DG.Tweening;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class WindowAnimationDisable : MonoBehaviour
{
    [Header("Window")]
    [Space]
    [SerializeField] private GameObject windowClose;
    [SerializeField] private Vector2 PosWindow;
    [SerializeField] private float hideSpeed = 1f;

    //[HideInInspector]
    public bool isOpen = true;

    [Header("Button")]
    [Space]
    [SerializeField] private Button btnDisable;


    private void Awake()
    {
        if (windowClose == null)
        {
            windowClose = gameObject;
        }

        btnDisable.onClick.AddListener(() => windowDisableAnim(PosWindow));
    }

    private void windowDisableAnim(Vector2 vector)
    {
        Debug.Log("A");

        if (isOpen == true)
        {
            RectTransform windowTransform = windowClose.GetComponent<RectTransform>();

            // Анимация ухода окна с экрана
            windowTransform.DOAnchorPos(vector, hideSpeed).SetEase(Ease.InBack).OnComplete(() => windowTransform.gameObject.SetActive(false));

            isOpen = false;
    }
}