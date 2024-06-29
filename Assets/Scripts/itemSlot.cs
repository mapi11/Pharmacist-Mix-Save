using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class itemSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private Rigidbody2D rb2d;


    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("дропнул");
        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        rb2d.bodyType = RigidbodyType2D.Static;
    }
}
