using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PatientMovingController : MonoBehaviour
{
    [SerializeField] private GameObject[] _patients;
    [SerializeField] private Transform[] _movePointsTransform;
    [SerializeField] private float _speed;
    private bool _nextPatient;
    private GameObject _currentPatient;
    private int _patientNumber;
    private bool _patientIsGoingHome;

    
    private void Start()
    {
        _nextPatient = true;
        _patientNumber = 0;
        _patientIsGoingHome = false;
    }

    private void Update()
    {
        if (_patientNumber == _patients.Length)
        {
            _patientNumber = 0;
        }
        CreatePatient();
        PatientGoingToClinic();
        PatientGoHome();
    }

    private void CreatePatient()
    {
        if (_nextPatient == true)
        {
            _currentPatient = Instantiate(_patients[_patientNumber], _movePointsTransform[0].position, Quaternion.identity);
            _nextPatient = false;
        }
    }

    private void PatientGoingToClinic()
    {
        if (_currentPatient != null && _currentPatient.transform.position != _movePointsTransform[1].position)
        {
            _speed += Time.fixedDeltaTime;
            _currentPatient.transform.position = Vector2.MoveTowards(_currentPatient.transform.position, _movePointsTransform[1].position, _speed * 0.0005f);
        }
    }

    public void OnClickPatientSaved()
    {
        _patientIsGoingHome = true;
        _patientNumber++;
        _speed = 0;
    }

    private void PatientGoHome()
    {
        if (_patientIsGoingHome)
        {
            _speed += Time.fixedDeltaTime;
            _currentPatient.transform.position = Vector2.MoveTowards(_currentPatient.transform.position, _movePointsTransform[0].position, _speed * 0.003f);
            if (_currentPatient.transform.position == _movePointsTransform[0].position)
            {
                _patientIsGoingHome = false;
                Destroy(_currentPatient);
                _speed = 0;
                _nextPatient = true;
            }
        }
    }
}
