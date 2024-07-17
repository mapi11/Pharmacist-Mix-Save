using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientMovingController : MonoBehaviour
{
    [SerializeField] private GameObject[] _patients;
    private int _currentPatient;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _finishPoint;
    [SerializeField, Range(0, 1)] private float _speed;
    private bool _callPatient = false;
    private bool _patientGoHome = false;


    private void Update()
    {
        if (_callPatient)
        {
            CallPatient();
        }
        if (_patientGoHome)
        {
            PatientGoHome();
        }
    }

    public void CallPatient()
    {
        if (_currentPatient < _patients.Length)
        {
            _callPatient = true;
            if (_patients[_currentPatient].transform.position == _finishPoint.position)
            {
                _callPatient = false;
            }
            else
            {
                _patients[_currentPatient].SetActive(true);
                _patients[_currentPatient].transform.position = Vector2.MoveTowards(_patients[_currentPatient].transform.position, _finishPoint.position, Time.fixedDeltaTime * _speed);
            }
        }
        else
        {
            Debug.Log("Вы приняли всех пациентов на сегодня!");
        }
    }

    public void PatientGoHome()
    {
        _patientGoHome = true;
        if (_patients[_currentPatient].transform.position == _startPoint.position)
        {
            _patientGoHome = false;
            Destroy(_patients[_currentPatient]);
            ++_currentPatient;
        }
        else
        {
            _patients[_currentPatient].transform.position = Vector2.MoveTowards(_patients[_currentPatient].transform.position, _startPoint.position, Time.fixedDeltaTime * _speed);
        }
    }
}
