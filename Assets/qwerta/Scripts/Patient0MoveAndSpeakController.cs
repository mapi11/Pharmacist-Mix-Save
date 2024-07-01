using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Patient0MoveAndSpeakController : MonoBehaviour
{
    [SerializeField] private string[] _messagesOfPatient;
    [SerializeField] private string[] _messageAnswers;
    [SerializeField] private GameObject _messageImg;
    [SerializeField] private GameObject _messageText;
    [SerializeField] private GameObject _giveMedicine;

    private PatientSpawnController _patientSpawnController;
    private int _messageIndex = 0;
    private GameObject _currentMessage;
    private GameObject _currentText;
    private GameObject _currentAnswer;
    private GameObject _currentAnswerText;
    private bool _finishIn = false;
    private bool _StartOut = false;

    [HideInInspector]
    [SerializeField] private GameObject _pointB;
    [HideInInspector]
    [SerializeField] private GameObject _pointA;
    [HideInInspector]
    [SerializeField] private GameObject _mainCanvas;
    [HideInInspector]
    public bool patientSpeaking = true;
    [HideInInspector]
    [SerializeField] private GameObject _messagePos1;
    [HideInInspector]
    [SerializeField] private GameObject _messagePos2;

    private void Awake()
    {
        _mainCanvas = GameObject.Find("MainCanvas");
        _mainCanvas.GetComponent<MessageNextController0>().enabled = true;
        _mainCanvas.GetComponent<MessageNextController1>().enabled = false;
        _mainCanvas.GetComponent<MessageNextController2>().enabled = false;
    }

    private void Start()
    {
        _pointB = GameObject.Find("Finish");
        _pointA = GameObject.Find("Start");
        _messagePos1 = GameObject.Find("Point 1");
        _messagePos2 = GameObject.Find("Point 2");
        _patientSpawnController = FindAnyObjectByType<PatientSpawnController>();
    }

    private void Update()
    {
        if (gameObject.transform.position == _pointB.transform.position)
        {
            SpawnMessage();
        }
        PositionController();
        MovePersIn();
        MovePersOut();
    }

    private void MovePersIn()
    {
        if (_finishIn == false)
        {
            gameObject.transform.position = Vector2.MoveTowards(transform.position, _pointB.transform.position, Time.fixedDeltaTime);
        }
    }

    private void MovePersOut()
    {
        if (_StartOut == true)
        {
            gameObject.transform.position = Vector2.MoveTowards(transform.position, _pointA.transform.position, Time.fixedDeltaTime);
        }
    }

    private void PositionController()
    {
        if (gameObject.transform.position == _pointB.transform.position)
        {
            _finishIn = true;
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
                _messageIndex++;
            }
            patientSpeaking = false;
        }
        if (_messageIndex > _messagesOfPatient.Length)
        {
            Destroy(_currentMessage);
            Destroy(_currentText);
            patientSpeaking = false;
            _giveMedicine.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bottle_0"))
        {
            _currentAnswer = Instantiate(_messageImg, _messagePos1.transform.position, Quaternion.identity, gameObject.transform);
            _currentAnswerText = Instantiate(_messageText, _messagePos1.transform.position, Quaternion.identity, gameObject.transform);
            TextMeshProUGUI _message = _currentAnswerText.GetComponent<TextMeshProUGUI>();
            _message.text = _messageAnswers[0];
        }
        else
        {
            _currentAnswer = Instantiate(_messageImg, _messagePos1.transform.position, Quaternion.identity, gameObject.transform);
            _currentAnswerText = Instantiate(_messageText, _messagePos1.transform.position, Quaternion.identity, gameObject.transform);
            TextMeshProUGUI _message = _currentAnswerText.GetComponent<TextMeshProUGUI>();
            _message.text = _messageAnswers[1];
        }
        Destroy(collision.gameObject);
        StartCoroutine(GoHomeCorutine());
    }

    public void GiveMedicine()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    IEnumerator GoHomeCorutine()
    {

        yield return new WaitForSeconds(2f);
        _StartOut = true;
        StartCoroutine(DeleteAnswerCorutine());
        _patientSpawnController.SpawnPatient(1);
    }

    IEnumerator DeleteAnswerCorutine()
    {
        yield return new WaitForSeconds(5.5f);
        Destroy(gameObject);
    }
}
