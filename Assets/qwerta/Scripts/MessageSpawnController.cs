using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using TMPro;

public class MessageSpawnController : MonoBehaviour
{
    [SerializeField] private GameObject _messageObject;
    [SerializeField] private GameObject _messageText;
    [SerializeField] private string[] _messages;
    [SerializeField] private Transform[] _routePoints;
    public static bool _nextMessage;
    private GameObject _currentMessageObject;
    private GameObject _prevMessageObject;
    private GameObject _mainCanvas;
    private int _messageIndex = 0;
    public static bool DeleteMessages;

    void Start()
    {
        _nextMessage = true;
        _mainCanvas = GameObject.Find("MainCanvas");
        DeleteMessages = false;
    }

    private void Update()
    {
        MessageSpawn();
        DeliteMessages();
    }

    private void MessageSpawn()
    {
        if (_messageIndex < _messages.Length)
        {
            if (PatientMovingController.PatientIsSpeak && _nextMessage && _messageIndex == 0)
            {
                _currentMessageObject = Instantiate(_messageObject, _routePoints[0].position, Quaternion.identity, _mainCanvas.transform);
                _nextMessage = false;
                _messageText = GameObject.Find("messageText");
                TextMeshProUGUI messageText = _currentMessageObject.GetComponentInChildren<TextMeshProUGUI>();
                messageText.text = _messages[_messageIndex];
                _messageIndex++;
                Transform _curMessTrans = _currentMessageObject.GetComponent<Transform>();
                _curMessTrans.DOMove(_routePoints[1].position, 2f);
            }
            if (PatientMovingController.PatientIsSpeak && _nextMessage && _messageIndex > 0)
            {
                Destroy(_prevMessageObject);
                _prevMessageObject = _currentMessageObject;
                _prevMessageObject.transform.DOMove(_routePoints[2].position, 2f);
                _currentMessageObject = Instantiate(_messageObject, _routePoints[0].position, Quaternion.identity, _mainCanvas.transform);
                _nextMessage = false;
                _messageText = GameObject.Find("messageText");
                TextMeshProUGUI messageText = _currentMessageObject.GetComponentInChildren<TextMeshProUGUI>();
                messageText.text = _messages[_messageIndex];
                _messageIndex++;
                Transform _curMessTrans = _currentMessageObject.GetComponent<Transform>();
                _curMessTrans.DOMove(_routePoints[1].position, 2f);
            }
        }
        else
        {
            PatientMovingController.PatientIsSpeak = false;
        }
    }

    private void DeliteMessages()
    {
        if (DeleteMessages)
        {
            Destroy(_prevMessageObject);
            Destroy(_currentMessageObject);
            Destroy(_messageObject);
        }
    }
}
