using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PatientMovingController : MonoBehaviour
{
    [Header ("Кнопка вызова пациента")]
    [SerializeField] private GameObject _callPatientButtonPrefab;
    private GameObject _callPatientButton;
    [SerializeField] private Transform _buttonPosition;
    
    private Transform _startPoint;
    private Transform _finishPoint;
    [SerializeField] private GameObject[] _patients;
    [SerializeField, Range(0, 1)] private float _speed;
    private bool _callPatient = false;
    private int _currentPatient;

    [HideInInspector]
    public bool _patientGoHome = false;


    private void Awake()
    {
        _callPatientButton = Instantiate(_callPatientButtonPrefab, _buttonPosition.position, Quaternion.identity, gameObject.transform);
        _startPoint = GameObject.Find("StartPoint").transform;
        _finishPoint = GameObject.Find("FinishPoint").transform;
    }

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
            _callPatientButton.SetActive(false);
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
    }

    public void PatientGoHome()
    {
        _patientGoHome = true;
        if (_patients[_currentPatient].transform.position == _startPoint.position)
        {
            _patientGoHome = false;
            _callPatientButton.SetActive(true);
            Destroy(_patients[_currentPatient]);
            ++_currentPatient;
        }
        else
        {
            _patients[_currentPatient].transform.position = Vector2.MoveTowards(_patients[_currentPatient].transform.position, _startPoint.position, Time.fixedDeltaTime * _speed);
        }
    }
}
