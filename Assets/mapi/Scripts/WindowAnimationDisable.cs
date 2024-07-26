using DG.Tweening;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class WindowAnimationDisable : MonoBehaviour
{
    [Header("Window")]
    [SerializeField] private GameObject windowAnim;
    [SerializeField] private Vector2 PosWindow;

    [Space]
    [SerializeField] private Button btnDisable;
    [SerializeField] private Button btnEnable;

    [Space]
    [SerializeField] private float hideSpeed = 1.5f;

    //[HideInInspector]
    public bool isOpen = false;

    private void Awake()
    {

        btnDisable.onClick.AddListener(() => windowDisableAnim(PosWindow));
        btnEnable.onClick.AddListener(() => windowEnableAnim());
    }

    private void windowEnableAnim()
    {
        if (isOpen == false)
        {
            windowAnim.SetActive(true);
            btnEnable.interactable = false;
            btnDisable.interactable = true;

            RectTransform windowTransform = windowAnim.GetComponent<RectTransform>();

            // Анимация выезда окна на экран
            //windowTransform.anchoredPosition = vector;
            windowTransform.DOAnchorPos(Vector2.zero, hideSpeed).SetEase(Ease.OutBack);

            isOpen = true;
        }
    }


    private void windowDisableAnim(Vector2 vector)
    {
        if (isOpen == true)
        {
            btnEnable.interactable = true;
            btnDisable.interactable = false;

            RectTransform windowTransform = windowAnim.GetComponent<RectTransform>();

            // Анимация ухода окна с экрана
            windowTransform.DOAnchorPos(vector, hideSpeed).SetEase(Ease.InBack).OnComplete(() => windowTransform.gameObject.SetActive(false));

            isOpen = false;
        }
    }
}