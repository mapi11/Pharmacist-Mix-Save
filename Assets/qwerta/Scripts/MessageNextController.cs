using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MessageNextController : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        if (PatientMovingController.PatientIsSpeak == false)
        {
            MessageSpawnController.DeleteMessages = true;
        }
        else
        {
            MessageSpawnController._nextMessage = true;
        }
    }
}
