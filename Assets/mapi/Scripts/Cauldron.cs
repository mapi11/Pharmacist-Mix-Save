using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;

public class Cauldron : MonoBehaviour
{
    // Теги ресурсов
    public string res0Tag = "res_0";
    public string res1Tag = "res_1";
    public string res2Tag = "res_2";
    public string res3Tag = "res_3";
    // Теги готовых ресурсов
    //public string bottle_0 = "bottle_0";
    //public string bottle_1 = "bottle_1";
    //public string bottle_2 = "bottle_2";

    public GameObject result999;
    public GameObject result0; // res_0 x 2
    public GameObject result1; // res_0 + res_1
    public GameObject result2; // res_0 + res_1

    public Transform spawnPoint;
    public Button processButton;

    public bool _resInside;
    public bool _anythingInside;

    public float launchForce = 5f; // Сила запуска объекта

    public GameObject FrontCauldron;
    [SerializeField] private Image frontCauldronImage;
    [SerializeField] private Sprite newSprite;

    private GameObject _canvas;
    private CanvasGroup frontCauldronCanvasGroup;
    private float targetAlpha = 0f;
    private float fadeDuration = 5f;

    private List<GameObject> ingredients = new List<GameObject>();

    private void Start()
    {
        _canvas = GameObject.Find("ResContent");

        frontCauldronCanvasGroup = FrontCauldron.GetComponent<CanvasGroup>();
        frontCauldronImage.type = Image.Type.Filled;

        processButton.onClick.AddListener(ProcessIngredients);

    }
    private void Update()
    {
        if (_anythingInside || _resInside)
        {
            targetAlpha = 0.5f; // Прозрачность, к которой должен стремиться FrontCauldron

            StartCoroutine(FadeFrontCauldron());
        }
        else
        {
            targetAlpha = 1f; // Прозрачность по умолчанию
            StartCoroutine(FadeFrontCauldron());
        }
    }

    IEnumerator FadeFrontCauldron()
    {
        float startAlpha = frontCauldronCanvasGroup.alpha;
        float currentTime = 0f;

        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            frontCauldronCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, currentTime / fadeDuration);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(res0Tag) || collision.CompareTag(res1Tag) || collision.CompareTag(res2Tag) || collision.CompareTag(res3Tag))
        {
            ingredients.Add(collision.gameObject);
            Debug.Log($"Ingredient added: {collision.tag}");
            _resInside = true; // Устанавливаем булевую переменную в true при добавлении объекта
        }
        else
        {
            _anythingInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (ingredients.Contains(collision.gameObject))
        {
            ingredients.Remove(collision.gameObject);
            Debug.Log($"Ingredient removed: {collision.tag}");
            _resInside = ingredients.Count > 0; // Проверяем, остались ли объекты в триггерной зоне
        }
        else
        {
            _anythingInside = false;
        }
    }

    private void ProcessIngredients()
    {
        int res0Count = 0;
        int res1Count = 0;
        int res2Count = 0;
        int res3Count = 0;


        List<GameObject> ingredientsToRemove = new List<GameObject>();

        foreach (var ingredient in ingredients)
        {
            if (ingredient.CompareTag(res0Tag)) res0Count++;
            if (ingredient.CompareTag(res1Tag)) res1Count++;
            if (ingredient.CompareTag(res2Tag)) res2Count++;
            if (ingredient.CompareTag(res3Tag)) res3Count++;
        }

        if (res0Count == 3 && res1Count == 2 && res2Count == 2 && res3Count == 0) // bottle_0
        {
            ingredientsToRemove.AddRange(ingredients.FindAll(ingredient => ingredient.CompareTag(res0Tag) || ingredient.CompareTag(res1Tag) || ingredient.CompareTag(res2Tag) || ingredient.CompareTag(res3Tag)));
            CreateResult(result0);

            Debug.Log("bottle_0");
        }
        else if (res0Count == 4 && res1Count == 2 && res2Count == 0 && res3Count == 3) //bottle_0
        {
            ingredientsToRemove.AddRange(ingredients.FindAll(ingredient => ingredient.CompareTag(res0Tag) || ingredient.CompareTag(res1Tag) || ingredient.CompareTag(res2Tag) || ingredient.CompareTag(res3Tag)));
            CreateResult(result1);

            Debug.Log("bottle_1");
        }
        else if (_resInside == true)
        {
            ingredientsToRemove.AddRange(ingredients.FindAll(ingredient => ingredient.CompareTag(res0Tag) || ingredient.CompareTag(res1Tag) || ingredient.CompareTag(res2Tag) || ingredient.CompareTag(res3Tag)));
            CreateResult(result999);

            Debug.Log("res_999");
        }

        foreach (var ingredient in ingredientsToRemove)
        {
            ingredients.Remove(ingredient);
            Destroy(ingredient);
        }

        _resInside = ingredients.Count > 0; // Проверяем, остались ли объекты после обработки
    }

    private void CreateResult(GameObject result)
    {
        if (result != null)
        {
            // Replace the image sprite
            frontCauldronImage.sprite = newSprite;

            // Smoothly transition fillAmount from 0 to 1
            frontCauldronImage.fillAmount = 0; // Start with fillAmount at 0
            frontCauldronImage.DOFillAmount(1, 1f).SetEase(Ease.Linear).OnComplete(() =>
            {
                // Create the result after the fillAmount animation completes
                GameObject newResult = Instantiate(result, spawnPoint.position, Quaternion.identity, _canvas.transform);

                Rigidbody2D rb = newResult.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = new Vector2(Random.Range(1f, 2f), 1f) * launchForce; // Set a random launch direction
                }

                Debug.Log($"Created result: {result.name}");

                // Smoothly transition fillAmount back from 1 to 0
                frontCauldronImage.DOFillAmount(0, 1f).SetEase(Ease.Linear);
            });
        }
    }
}