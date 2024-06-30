//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using DG.Tweening;
//using UnityEngine.EventSystems;
//using TMPro;

//public class MessageSpawnController : MonoBehaviour
//{
//    [SerializeField] private GameObject _mainCanvas;
//    [SerializeField] private GameObject _currentMessageObject;
//    [SerializeField] private GameObject _prevMessageObject;
//    [SerializeField] private Transform[] _routePoints;

//    [SerializeField] private string[] _messagesPatient1;
//    [SerializeField] private string[] _messagesPatient2;
//    [SerializeField] private string[] _messagesPatient3;
//    private int _messageIndex = 0;
//    private float typingSpeed = 0.15f;

//    public static bool _nextMessage;
//    public static bool DeleteMessages;

//    private GameObject _currentMessageText;
//    private GameObject _prevMessageText;

//    private GameObject _currentMessage;
//    private GameObject _prevMessage;

//    void Start()
//    {
//        _nextMessage = true;
//        DeleteMessages = false;
//    }

//    private void Update()
//    {
//        if (_messageIndex == _messagesPatient1.Length)
//        {
//            Debug.Log("123123123123123131231231231312");
//        }
//        MessageSpawn();
//        DeliteMessages();
        
//    }

//    private void MessageSpawn()
//    {
//        Debug.Log(_messageIndex);
//        if (PatientMovingController.PatientIsSpeak && _nextMessage && _messageIndex == 0)
//        {
//            _prevMessage = null;
//            _currentMessage = Instantiate(_currentMessageObject, _routePoints[0].position, Quaternion.identity, _mainCanvas.transform);
//            _nextMessage = false;
//            _currentMessageText = GameObject.Find("currentMessageText");
//            TextMeshProUGUI messageText = _currentMessageText.GetComponent<TextMeshProUGUI>();
//            StartCoroutine(TypeText(messageText, _messagesPatient1[_messageIndex]));
//            _messageIndex++;
//            _currentMessage.transform.DOMove(_routePoints[1].position, 2f);
//        }
//        if (PatientMovingController.PatientIsSpeak && _nextMessage && _messageIndex > 0)
//        {
//            TextMeshProUGUI messageText = _currentMessageText.GetComponent<TextMeshProUGUI>();
//            StartCoroutine(TypeText(messageText, _messagesPatient1[_messageIndex]));
//            _nextMessage = false;
//            _messageIndex++;
//        }
//    }

//    private IEnumerator TypeText(TextMeshProUGUI textComponent, string message)
//    {
//        textComponent.text = "";
//        foreach (char letter in message.ToCharArray())
//        {
//            textComponent.text += letter;
//            yield return new WaitForSeconds(typingSpeed);
//        }
//    }

//    private void DeliteMessages()
//    {
//        if (DeleteMessages)
//        {
//            Destroy(_prevMessageObject);
//            Destroy(_currentMessageObject);
//        }
//    }
//}