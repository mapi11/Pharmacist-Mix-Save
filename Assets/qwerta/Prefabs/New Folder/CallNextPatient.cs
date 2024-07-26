using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallNextPatient : MonoBehaviour
{
    private PatientMovingController _patientMovingController;


    private void Awake()
    {
        _patientMovingController = FindAnyObjectByType<PatientMovingController>();
    }

    public void OnClickCallButton()
    {
        _patientMovingController.CallPatient();
    }
}
