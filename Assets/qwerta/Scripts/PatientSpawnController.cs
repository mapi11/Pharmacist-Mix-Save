using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientSpawnController : MonoBehaviour
{
    [SerializeField] private GameObject[] _patients;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private GameObject _mainCanvas;

    private GameObject _currentPatient;

    private void Start()
    {
        SpawnPatient(0);
    }

    public void SpawnPatient(int patientID)
    {
        _currentPatient = Instantiate(_patients[patientID], _spawnPosition.position, Quaternion.identity, _mainCanvas.transform);
    }
}
