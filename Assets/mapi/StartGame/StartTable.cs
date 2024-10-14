using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTable : MonoBehaviour
{
    [SerializeField] private GameObject CameraCanvas;

    [Header("Other window")]
    [SerializeField] private GameObject ResBags;
    [SerializeField] private Transform Camera;
    [SerializeField] private Transform CameraTarget;

    CameraHorizontallMove cameraHorizontallMove;



    private void Awake()
    {
        cameraHorizontallMove = FindAnyObjectByType<CameraHorizontallMove>();

        cameraHorizontallMove._canMoove = true;
        CameraCanvas.SetActive(true);
    }

    private void Update()
    {
        if (Camera.position.x >= CameraTarget.position.x)
        {
            ResBags.SetActive(true);
        }
        else
        {
            ResBags.SetActive(false);
        }
    }
}
