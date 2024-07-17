using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckMedicine : MonoBehaviour
{
    private GameObject _mainCanvas;
    [SerializeField] private string _rightMedicineTag;
    [SerializeField] private GameObject _messagePanel;
    [SerializeField] private string[] _answerList;
    private GameObject _answerMessage;
    private TextMeshProUGUI _answer_Text;
    [SerializeField] private GameObject _giveMedsButton;
    private PatientMovingController _patientMovingController;


    private void Awake()
    {
        _mainCanvas = GameObject.Find("MainCanvas");
    }

    private void Start()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        _patientMovingController = FindAnyObjectByType<PatientMovingController>();
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
            PatientsAnswer(0);
            StartCoroutine(GoHomeCorutine());
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(collision.gameObject);
            _giveMedsButton.SetActive(false);
            PatientsAnswer(1);
            StartCoroutine(GoHomeCorutine());
        }
    }

    IEnumerator GoHomeCorutine()
    {
        yield return null;
        yield return new WaitForSeconds(1.5f);
        Destroy(_answerMessage);
        _patientMovingController.PatientGoHome();
    }

    private void PatientsAnswer(int var)
    {
        if (_answerMessage == null)
        {
            _answerMessage = Instantiate(_messagePanel, Vector2.zero, Quaternion.identity, _mainCanvas.transform);
            _answer_Text = _answerMessage.transform.Find("Message_Text").GetComponent<TextMeshProUGUI>();
            _answer_Text.text = _answerList[var];
        }
    }
}
