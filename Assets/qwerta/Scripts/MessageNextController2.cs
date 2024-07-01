using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MessageNextController2 : MonoBehaviour, IPointerDownHandler
{
    private Patient2MoveAndSpeakController _patient2MoveAndSpeakController;

    public void OnPointerDown(PointerEventData eventData)
    {
        _patient2MoveAndSpeakController = FindAnyObjectByType<Patient2MoveAndSpeakController>();
        _patient2MoveAndSpeakController.SpawnMessage();
        _patient2MoveAndSpeakController.patientSpeaking = true;
    }
}
