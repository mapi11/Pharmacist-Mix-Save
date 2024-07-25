using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class NextMessageClick : MonoBehaviour, IPointerDownHandler
{
    private DialogSystem _dialogSystem;


    private void Awake()
    {
        _dialogSystem = FindAnyObjectByType<DialogSystem>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        _dialogSystem.PatientSpeaking();
    }
}
