using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientSpawnController : MonoBehaviour
{
    [SerializeField] private GameObject[] _patients;
    private GameObject _currentPatient;
    [SerializeField] private Transform _spawnPosition;
    public static int _indexPatient;
    [SerializeField] private GameObject _mainCanvas;


    private void Start()
    {
        _indexPatient = 0;
        SpawnPatient();
    }

    private void SpawnPatient()
    {
        _currentPatient = Instantiate(_patients[_indexPatient], _spawnPosition.position, Quaternion.identity, _mainCanvas.transform);
    }
}
