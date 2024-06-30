using TMPro;
using UnityEngine;
using System.Collections;

public class Bag : MonoBehaviour
{
    [SerializeField] private string _acceptedTag;
    [SerializeField] private GameObject _resourcePrefab;
    [SerializeField] private int _resourceCount = 0;
    [SerializeField] private TextMeshProUGUI _resourceCountText;
    [SerializeField] private Transform _resourceSpawnPoint;
    [SerializeField] private Transform _resourceTargetPoint; // Целевая точка для движения
    [SerializeField] private float moveDuration = 0.3f; // Длительность движения
    private GameObject _canvas;

    CameraSliderController _sliderController;

    private void Awake()
    {
        _sliderController = FindAnyObjectByType<CameraSliderController>();
    }

    private void Start()
    {
        UpdateResourceCountText();
        _canvas = GameObject.Find("ResContent");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Physics") && collision.CompareTag(_acceptedTag))
        {
            Destroy(collision.gameObject);
            _resourceCount++;
            Debug.Log("Resource added. Current count: " + _resourceCount);
            UpdateResourceCountText();
        }
    }

    private void OnMouseDown()
    {
        if (_resourceCount > 0)
        {
            GameObject resource = Instantiate(_resourcePrefab, _resourceSpawnPoint.position, Quaternion.identity, _canvas.transform);
            StartCoroutine(MoveResource(resource));
            _resourceCount--;
            Debug.Log("Resource removed. Current count: " + _resourceCount);
            UpdateResourceCountText();
        }
    }

    private IEnumerator MoveResource(GameObject resource)
    {
        Vector3 startPosition = resource.transform.position;
        Vector3 endPosition = _resourceTargetPoint.position;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            resource.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        resource.transform.position = endPosition;
    }

    private void UpdateResourceCountText()
    {
        _resourceCountText.text = _resourceCount.ToString();
    }
}