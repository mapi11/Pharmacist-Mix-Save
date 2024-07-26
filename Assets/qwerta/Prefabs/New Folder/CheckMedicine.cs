using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheckMedicine : MonoBehaviour
{
    private GameObject _mainCanvas;
    [SerializeField] private string _rightMedicineTag;
    [SerializeField] private GameObject _messagePanel;
    [SerializeField] private string _positiveAnswer;
    [SerializeField] private string _negativeAnswer;
    private GameObject _answerMessage;
    private TextMeshProUGUI _answer_Text;
    [SerializeField] private GameObject _giveMedsButton;
    private PatientMovingController _patientMovingController;
    private Transform _finishPoint;
    private Transform _messagePoint;
    private bool _startAnimation = false;
    private float _spaleParametr = 0;
    private DialogSystem _dialogSystem;


    private void Awake()
    {
        _mainCanvas = GameObject.Find("MainCanvas");
        _finishPoint = GameObject.Find("FinishPoint").transform;
        _messagePoint = GameObject.Find("MessagePoint").transform;
    }

    private void Start()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        _patientMovingController = FindAnyObjectByType<PatientMovingController>();
        _dialogSystem = FindAnyObjectByType<DialogSystem>();
    }

    private void Update()
    {
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

    public void GiveMedicine()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(_rightMedicineTag))
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(collision.gameObject);
            _giveMedsButton.SetActive(false);
            PatientsAnswer(_positiveAnswer);
            StartCoroutine(GoHomeCorutine());
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(collision.gameObject);
            _giveMedsButton.SetActive(false);
            PatientsAnswer(_negativeAnswer);
            StartCoroutine(GoHomeCorutine());
        }
    }

    IEnumerator GoHomeCorutine()
    {
        yield return null;
        yield return new WaitForSeconds(2f);
        Destroy(_answerMessage);
        _patientMovingController.PatientGoHome();
    }

    private void PatientsAnswer(string var)
    {
        if (_answerMessage == null)
        {
            _answerMessage = Instantiate(_messagePanel, _finishPoint.position, Quaternion.identity, _mainCanvas.transform);
            _startAnimation = true;
            DialogAnimationScale();
            _answer_Text = _answerMessage.transform.Find("Message_Text").GetComponent<TextMeshProUGUI>();
            _answer_Text.text = var;
        }
    }

    private void DialogAnimationScale()
    {
        if (_answerMessage != null)
        {
            _answerMessage.transform.localScale = new Vector2(_spaleParametr, _spaleParametr);
            _spaleParametr += Time.deltaTime * _dialogSystem.ScaleSpeed;
        }
    }

    private void DialogAnimationPosition()
    {
        if (_answerMessage != null)
        {
            _answerMessage.transform.position = Vector2.MoveTowards(_answerMessage.transform.position, _messagePoint.position, Time.deltaTime * _dialogSystem.PositionSpeed);
        }
    }
}
