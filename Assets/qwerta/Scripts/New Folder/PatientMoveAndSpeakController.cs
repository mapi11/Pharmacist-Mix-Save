using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PatientMoveAndSpeakController : MonoBehaviour
{
    [SerializeField] private string[] _messagesOfPatient;
    [SerializeField] private GameObject _messageImg;
    [SerializeField] private GameObject _messageText;
    
    private int _messageIndex = 0;
    private GameObject _currentMessage;
    private GameObject _currentText;

    [HideInInspector]
    [SerializeField] private GameObject _pointB;
    [HideInInspector]
    [SerializeField] private GameObject _mainCanvas;
    [HideInInspector]
    public bool patientSpeaking = true;
    [HideInInspector]
    [SerializeField] private GameObject _messagePos1;
    [HideInInspector]
    [SerializeField] private GameObject _messagePos2;


    private void Start()
    {
        _pointB = GameObject.Find("Finish");
        _mainCanvas = GameObject.Find("MainCanvas");
        _messagePos1 = GameObject.Find("Point 1");
        _messagePos2 = GameObject.Find("Point 2");
        
        gameObject.transform.DOMove(_pointB.transform.position, 2f);
    }

    private void Update()
    {
        if (gameObject.transform.position == _pointB.transform.position)
        {
            SpawnMessage();
        }
    }

    public void SpawnMessage()
    {
        if (_messageIndex < _messagesOfPatient.Length)
        {
            if (patientSpeaking)
            {
                Destroy(_currentMessage);
                Destroy(_currentText);

                _currentMessage = Instantiate(_messageImg, _messagePos1.transform.position, Quaternion.identity, _mainCanvas.transform);
                _currentText = Instantiate(_messageText, _messagePos1.transform.position, Quaternion.identity, _mainCanvas.transform);
                _currentMessage.transform.DOMove(_messagePos2.transform.position, 2f);
                _currentText.transform.DOMove(_messagePos2.transform.position, 2f);
                TextMeshProUGUI _message = _currentText.GetComponent<TextMeshProUGUI>();
                _message.text = _messagesOfPatient[_messageIndex];
                _messageIndex++;
            }
            patientSpeaking = false;
        }
        if (_messageIndex == _messagesOfPatient.Length)
        {
            if (patientSpeaking)
            {
                Destroy(_currentMessage);
                Destroy(_currentText);

                _currentMessage = Instantiate(_messageImg, _messagePos1.transform.position, Quaternion.identity, _mainCanvas.transform);
                _currentText = Instantiate(_messageText, _messagePos1.transform.position, Quaternion.identity, _mainCanvas.transform);
                _currentMessage.transform.DOMove(_messagePos2.transform.position, 2f);
                _currentText.transform.DOMove(_messagePos2.transform.position, 2f);
                _messageIndex++;
            }
            patientSpeaking = false;
        }
        if (_messageIndex > _messagesOfPatient.Length)
        {
            Destroy(_currentMessage);
            Destroy(_currentText);
            patientSpeaking = false;
        }
    }

    public void PatientVerdict()
    {

    }
}
