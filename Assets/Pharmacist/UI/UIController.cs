using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] UIMainMenuScreen mainMenuScreen;
    [SerializeField] UICustomerScreen customerScreen;
    [SerializeField] UILaboratoryScreen laboratoryScreen;
    public void KickStart()
    {
        customerScreen.gameObject.SetActive(true);
    }
}
