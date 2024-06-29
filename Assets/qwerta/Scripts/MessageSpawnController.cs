using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MessageSpawnController : MonoBehaviour
{
    [SerializeField] private GameObject _messageObject;
    [SerializeField] private string[] _messages;
    [SerializeField] private Transform[] _routePoints;
    private bool _nextMessage;
    private GameObject _currentMessage;
    private GameObject _prevMessage;


    void Start()
    {
        _nextMessage = true;
    }

    private void Update()
    {
        if (PatientMovingController.PatientIsSpeak && _nextMessage)
        {
            _currentMessage = Instantiate(_messageObject, _routePoints[0].position, Quaternion.identity);
            _nextMessage = false;
            Transform _curMessTrans = _currentMessage.GetComponent<Transform>();
            _curMessTrans.DOMove(_routePoints[1].position, 2f);
        }
    }
}
