using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MessageNextController0 : MonoBehaviour, IPointerDownHandler
{
    private Patient0MoveAndSpeakController _patient0MoveAndSpeakController;


    public void OnPointerDown(PointerEventData eventData)
    {
        _patient0MoveAndSpeakController = FindAnyObjectByType<Patient0MoveAndSpeakController>();
        _patient0MoveAndSpeakController.SpawnMessage();
        _patient0MoveAndSpeakController.patientSpeaking = true;
    }
}
