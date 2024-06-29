using TMPro;
using UnityEngine;

public class Bag : MonoBehaviour
{
    [SerializeField] private string _acceptedTag;
    [SerializeField] private GameObject _resourcePrefab;
    [SerializeField] private int _resourceCount = 0;
    [SerializeField] private TextMeshProUGUI _resourceCountText;
    [SerializeField] private Transform _resourceSpawnPoint;
    private GameObject _canvas;
    public float launchForce = 10f; // Сила, с которой будет запускаться объект

    private void Start()
    {
        UpdateResourceCountText();
        _canvas = GameObject.Find("MainCanvas");
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

    void OnMouseDown()
    {
        if (_resourceCount > 0)
        {
            GameObject resource = Instantiate(_resourcePrefab, _resourceSpawnPoint.position, Quaternion.identity, _canvas.transform);
            Rigidbody2D rb = resource.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(Vector2.left * launchForce, ForceMode2D.Impulse);
            }
            _resourceCount--;
            Debug.Log("Resource removed. Current count: " + _resourceCount);
            UpdateResourceCountText();
        }
    }

    void UpdateResourceCountText()
    {
        _resourceCountText.text = _resourceCount.ToString();
    }
}