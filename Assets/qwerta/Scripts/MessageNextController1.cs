using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MessageNextController1 : MonoBehaviour, IPointerDownHandler
{
    private Patient1MoveAndSpeakController _patient1MoveAndSpeakController;


    public void OnPointerDown(PointerEventData eventData)
    {
        _patient1MoveAndSpeakController = FindAnyObjectByType<Patient1MoveAndSpeakController>();
        _patient1MoveAndSpeakController.SpawnMessage();
        _patient1MoveAndSpeakController.patientSpeaking = true;
    }
}
