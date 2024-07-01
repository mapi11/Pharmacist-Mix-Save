using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraSliderController : MonoBehaviour
{
    public Slider verticalSlider;
    public Camera mainCamera;

    private float cameraStartY;
    private float cameraEndY;

    public Button _btnUp;
    public Button _btnDown;

    void Start()
    {
        _btnUp.onClick.AddListener(CameraUp);
        _btnDown.onClick.AddListener(CameraDown);

        if (verticalSlider != null && mainCamera != null)
        {
            cameraStartY = mainCamera.transform.position.y;
            cameraEndY = cameraStartY - 10f; // Adjust this value based on your needs
            verticalSlider.onValueChanged.AddListener(OnSliderValueChanged);
        }
    }

    private void Update()
    {
        if (verticalSlider.value == 0)
        {
            _btnUp.gameObject.SetActive(false);
        }
        else
        {
            _btnUp.gameObject.SetActive(true);
        }

        if (verticalSlider.value == 1)
        {
            _btnDown.gameObject.SetActive(false);
        }
        else
        {
            _btnDown.gameObject.SetActive(true);
        }
    }

    void OnSliderValueChanged(float value)
    {
        if (mainCamera != null)
        {
            float newY = Mathf.Lerp(cameraStartY, cameraEndY, value);
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, newY, mainCamera.transform.position.z);
        }
    }

    public void CameraUp()
    {
        if (verticalSlider.value != 0)
        {
            StartCoroutine(SmoothSliderToTop());
        }
    }

    private IEnumerator SmoothSliderToTop()
    {
        float duration = 1f; // Duration for smooth scroll
        float elapsedTime = 0f;
        float startValue = verticalSlider.value;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            verticalSlider.value = Mathf.Lerp(startValue, 0f, t);
            yield return null;
        }

        verticalSlider.value = 0f;
    }

    public void CameraDown()
    {
        if (verticalSlider.value != 1)
        {
            StartCoroutine(SmoothSliderToDown());
        }
    }

    private IEnumerator SmoothSliderToDown()
    {
        float duration = 1f; // Duration for smooth scroll
        float elapsedTime = 0f;
        float startValue = verticalSlider.value;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            verticalSlider.value = Mathf.Lerp(startValue, 1f, t);
            yield return null;
        }

        verticalSlider.value = 1f;
    }
}