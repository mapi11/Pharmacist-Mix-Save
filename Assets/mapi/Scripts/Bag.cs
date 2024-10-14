using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class Bag : MonoBehaviour
{
   [SerializeField] private string _acceptedTag;
    [SerializeField] private GameObject _resourcePrefab;
    [SerializeField] private int _resourceCount = 0;
    [SerializeField] private TextMeshProUGUI _resourceCountText;
    [SerializeField] private Transform _resourceSpawnPoint; // ����� ������ �������
    [SerializeField] private Transform _resourceTargetPointA; // ������� ����� A
    //[SerializeField] private GameObject buttonPrefab;
    //[SerializeField] private Transform buttonParent;
    [SerializeField] private float moveDuration = 0.2f;

    public bool _isOnTable;
    private bool _isMoving;

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
        if (!_isMoving && _resourceCount > 0)
        {
            _resourceCount--;
            Debug.Log("Resource removed. Current count: " + _resourceCount);
            UpdateResourceCountText();

            // ����� ������� �� ����� ������
            GameObject resource = Instantiate(_resourcePrefab, _resourceSpawnPoint.position, Quaternion.identity, _canvas.transform);

            // ������ �������� � ������� ����� A
            MoveResourceToTarget(resource, _resourceTargetPointA.position);
        }
    }

    // ����� ��� ����������� ������� � ��������� ����
    private void MoveResourceToTarget(GameObject resource, Vector3 targetPosition)
    {
        resource.transform.DOMove(targetPosition, moveDuration).SetEase(Ease.Linear).OnComplete(() =>
        {
            Debug.Log("Resource reached the target point.");
            // �������������� ������ ����� ���������� ����, ���� ����������
        });
        
        Rigidbody2D rb = resource.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero; // ������������� ���������� �������� ����� ���������� ����
        }
     }

     private void UpdateResourceCountText()
     {
         if (_resourceCountText != null)
         {
             _resourceCountText.text = $"Resources: {_resourceCount}";
         }
     }
}