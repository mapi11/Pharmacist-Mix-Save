using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class HeartReanimation : MonoBehaviour
{
    [Header("временное поле статистики")]
    [SerializeField] private int _successfulClicks;
    [SerializeField] private int _clicksComplete = 0;
    [SerializeField] private float _success;

    [Header ("Settings")]
    [SerializeField] private int _clicksPerLap;
    [SerializeField] private int _lapsCount;
    [SerializeField] private float _timePerLap;

    [Header("important objects")]
    [SerializeField] private GameObject _cardiogramm;
    [SerializeField] private GameObject _clickAreaOn;
    [SerializeField] private GameObject _clickAreaOff;


    private void Start()
    {
        StartCoroutine(ClickAreaOff());
    }

    public void OnClickButtonAreaOn()
    {
        ++_successfulClicks;
    }

    public void OnClickButtonAreaOff()
    {
        --_successfulClicks;
    }

    IEnumerator ClickAreaOn()
    {
        yield return new WaitForSeconds(0.6f);
        
        _clickAreaOn.SetActive (true);
        _clickAreaOff.SetActive (false);
        StartCoroutine(ClickAreaOff());
    }
    IEnumerator ClickAreaOff()
    {
        yield return new WaitForSeconds(0.6f);
        ++_clicksComplete;
        _success = (float)_successfulClicks / (float)_clicksComplete;
        _clickAreaOn.SetActive(false);
        _clickAreaOff.SetActive(true);
        StartCoroutine(ClickAreaOn());
    }
}