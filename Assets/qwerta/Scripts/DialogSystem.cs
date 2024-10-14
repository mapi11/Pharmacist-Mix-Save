using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class DialogSystem : MonoBehaviour
{
    private GameObject _longTableCanvas;
    [SerializeField] private GameObject _messagePanel;
    [SerializeField] private string[] _messageList;
    private int _messageIndex;
    private GameObject _currentMessage;
    private TextMeshProUGUI _message_Text;
    private Transform _finishPoint;
    private Transform _messagePoint;
    [SerializeField] private GameObject _giveMedsButton;

    [Header ("Анимация диалоговых окон")]
    [SerializeField, Range(0, 3)] public float ScaleSpeed;
    [SerializeField, Range(5, 15)] public float PositionSpeed;
    private bool _startAnimation = false;
    private float _spaleParametr = 0;


    private void Awake()
    {
        _giveMedsButton.SetActive(false);
        _longTableCanvas = GameObject.Find("TestTableCanvas");
        _finishPoint = GameObject.Find("FinishPoint").transform;
        _messagePoint = GameObject.Find("MessagePoint").transform;
    }

    private void Update()
    {
        if (transform.position == _finishPoint.position && _messageIndex == 0)
        {
            PatientSpeaking();
        }
        
        if (_startAnimation)
        {
            DialogAnimationScale();
            DialogAnimationPosition();
        }

        if (_spaleParametr >= 1)
        {
            _startAnimation = false;
            _spaleParametr = 0;
        }
    }

    public void PatientSpeaking()
    {
        if (_messageIndex == 0)
        {
            _currentMessage = Instantiate(_messagePanel, _finishPoint.position, Quaternion.identity, _longTableCanvas.transform);
            _startAnimation = true;
            DialogAnimationScale();
            MessageTextController();
        }
        else
        {
            Destroy(_currentMessage);
            _currentMessage = Instantiate(_messagePanel, _finishPoint.position, Quaternion.identity, _longTableCanvas.transform);
            _startAnimation = true;
            DialogAnimationScale();
            MessageTextController();
        }
        _messageIndex++;
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
            if (_messageIndex == _messageList.Length)
            {
                _giveMedsButton.SetActive(true);
            }
        }
    }

    private void DialogAnimationScale()
    {
        if (_currentMessage != null)
        {
            _currentMessage.transform.localScale = new Vector2(_spaleParametr, _spaleParametr);
            _spaleParametr += Time.deltaTime * ScaleSpeed;
        }
    }

    private void DialogAnimationPosition()
    {
        if (_currentMessage != null)
        {
            _currentMessage.transform.position = Vector2.MoveTowards(_currentMessage.transform.position, _messagePoint.position, Time.deltaTime * PositionSpeed);
        }
    }
}