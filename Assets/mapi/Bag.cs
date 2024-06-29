using TMPro;
using UnityEngine;

public class Bag : MonoBehaviour
{
    public string _acceptedTag;
    public GameObject _resourcePrefab;
    public int _resourceCount = 0;
    public TextMeshProUGUI _resourceCountText; // Текстовый объект для отображения количества ресурсов
    public Transform _resourceSpawnPoint; // Точка, где будет появляться ресурс
    public GameObject _canvas;

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
            Instantiate(_resourcePrefab, _resourceSpawnPoint.position, Quaternion.identity, _canvas.transform);
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