using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bag : MonoBehaviour
{
    [SerializeField] private string _acceptedTag;
    [SerializeField] private GameObject _resourcePrefab;
    [SerializeField] private int _resourceCount = 0;
    [SerializeField] private TextMeshProUGUI _resourceCountText;
    [SerializeField] private Transform _resourceSpawnPoint;
    [SerializeField] private Transform _resourceTargetPointA;
    [SerializeField] private Transform _resourceTargetPointB;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform buttonParent;
    [SerializeField] private float moveDuration = 0.2f;

    [SerializeField] private float SpawnPower = -4f;

    private bool _isOnTable;
    private bool _isMoving;

    private GameObject _canvas;
    private GameObject _buttonInstance;

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
        if (!_isMoving)
        {
            if (_isOnTable)
            {
                if (_resourceCount > 0)
                {
                    _resourceCount--;
                    Debug.Log("Resource removed. Current count: " + _resourceCount);
                    UpdateResourceCountText();

                    GameObject resource = Instantiate(_resourcePrefab, _resourceSpawnPoint.position, Quaternion.identity, _canvas.transform);
                    Rigidbody2D rb = resource.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.velocity = new Vector2(SpawnPower, 7f);
                    }
                }
            }
            else
            {
                StartCoroutine(MoveResourceToA(gameObject));
            }
        }
    }

    private IEnumerator MoveResourceToA(GameObject resource)
    {
        _isMoving = true;

        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        Vector3 startPosition = resource.transform.position;
        Vector3 endPosition = _resourceTargetPointA.position;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            resource.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        resource.transform.position = endPosition;
        _isMoving = false;
        _isOnTable = true;

        if (_buttonInstance == null)
        {
            _buttonInstance = Instantiate(buttonPrefab, buttonParent);
            Button button = _buttonInstance.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => OnButtonClick(resource));
            }
        }

        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnButtonClick(GameObject resource)
    {
        if (!_isMoving)
        {
            StartCoroutine(MoveResourceToB(resource));
            _isOnTable = false;
            Destroy(_buttonInstance);
        }
    }

    public IEnumerator MoveResourceToB(GameObject resource)
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        _isMoving = true;
        Vector3 startPosition = resource.transform.position;
        Vector3 endPosition = _resourceTargetPointB.position;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            resource.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        resource.transform.position = endPosition;
        _isMoving = false;

        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void UpdateResourceCountText()
    {
        _resourceCountText.text = _resourceCount.ToString();
    }
}