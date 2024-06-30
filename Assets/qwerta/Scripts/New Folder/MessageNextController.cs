using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MessageNextController : MonoBehaviour, IPointerDownHandler
{
    private PatientMoveAndSpeakController _patientMoveAndSpeakController;

    public void OnPointerDown(PointerEventData eventData)
    {
        _patientMoveAndSpeakController = FindAnyObjectByType<PatientMoveAndSpeakController>();
        _patientMoveAndSpeakController.SpawnMessage();
        _patientMoveAndSpeakController.patientSpeaking = true;
    }
}
