using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    private GameObject _mainCanvas;
    [SerializeField] private GameObject _messagePanel;
    [SerializeField] private string[] _messageList;
    private int _messageIndex;
    private GameObject _currentMessage;
    private TextMeshProUGUI _message_Text;
    [SerializeField] private Transform _finishPoint;
    [SerializeField] private GameObject _playerMessage;
    [SerializeField] private GameObject _giveMedsButton;


    private void Awake()
    {
        _giveMedsButton.SetActive(false);
        _mainCanvas = GameObject.Find("MainCanvas");
    }

    private void Update()
    {
        if (transform.position == _finishPoint.position && _messageIndex < _messageList.Length + 1)
        {
            _playerMessage.SetActive(true);
        }
        else
        {
            _playerMessage.SetActive(false);
        }
    }

    public void PatientSpeaking()
    {
        if (_messageIndex == 0)
        {
            _currentMessage = Instantiate(_messagePanel, Vector2.zero, Quaternion.identity, _mainCanvas.transform);
            MessageTextController();
            _messageIndex++;
        }
        else
        {
            Destroy(_currentMessage);
            _currentMessage = Instantiate(_messagePanel, Vector2.zero, Quaternion.identity, _mainCanvas.transform);
            MessageTextController();
            _messageIndex++;
        }
    }

    private void MessageTextController()
    {
        _message_Text = _currentMessage.transform.Find("Message_Text").GetComponent<TextMeshProUGUI>();
        if (_messageIndex < _messageList.Length)
        {
            _message_Text.text = _messageList[_messageIndex];
        }
        else
        {
            Destroy(_currentMessage);
            _giveMedsButton.SetActive(true);
        }
    }
}
