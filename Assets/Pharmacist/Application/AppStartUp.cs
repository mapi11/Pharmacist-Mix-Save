using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppStartUp : MonoBehaviour
{    
    [SerializeField] UIController uiController;
    private void Awake()
    {
        Instantiate(uiController, uiController.transform.position, Quaternion.Euler(Vector3.zero));

    }

}
