using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirstMessage : MonoBehaviour
{
    [SerializeField] private GameObject _message;

    [SerializeField] private TextMeshProUGUI _text;


    public string bottle_0 = "bottle_0";
    public string bottle_1 = "bottle_1";

    private void Awake()
    {
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(bottle_1))
        {
            _text.text = "Спасибо, помогло";
        }
        else if ((collision.CompareTag(bottle_0)))
        {
            _text.text = "Мне стало только хуже";
        }
    }
}
