using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;

public class Cauldron : MonoBehaviour
{
    //[Header("Res tags")]
    private string res0Tag = "res_0";
    private string res1Tag = "res_1";
    private string res2Tag = "res_2";
    private string res3Tag = "res_3";
    private string res4Tag = "res_4";
    private string res5Tag = "res_5";
    private string res6Tag = "res_6";

    private string bottle999Tag = "bottle_999";
    private string bottle0Tag = "bottle_0";
    private string bottle1Tag = "bottle_1";
    private string bottle2Tag = "bottle_2";

    [Space]
    [Header("Prefabs")]
    [SerializeField] private GameObject result999;
    [SerializeField] private GameObject result0;
    [SerializeField] private GameObject result1;
    [SerializeField] private GameObject result2;

    [Space]
    [Header("Prefab objects")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Button processButton;

    [SerializeField] private GameObject FrontCauldron;
    [SerializeField] private Image frontCauldronImage;
    [SerializeField] private Sprite newSprite;

    [Space]
    [SerializeField] private float launchForce = 5f; // Сила запуска объекта
    [SerializeField] private bool launchRight = true;

    private GameObject _canvas;
    private CanvasGroup frontCauldronCanvasGroup;

    private float targetAlpha = 1f;

    private bool _bottleInside;
    private bool _resInside;
    private bool _anythingInside;


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
        if (_anythingInside || _resInside || _bottleInside)
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

        while (currentTime < 5)
        {
            currentTime += Time.deltaTime;
            frontCauldronCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, currentTime / 5);
            yield return null;
        }
    }

    //IEnumerator FadeFrontCauldron()
    //{
    //    frontCauldronCanvasGroup.alpha = Mathf.Lerp(frontCauldronCanvasGroup.alpha, targetAlpha, 0);
    //    yield return null;
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(bottle0Tag) || collision.CompareTag(bottle1Tag) || collision.CompareTag(bottle2Tag))
        {
            Debug.Log($"Bottle added: {collision.tag}");
            _bottleInside = true;
        }

        if (collision.CompareTag(res0Tag) || collision.CompareTag(res1Tag) || collision.CompareTag(res2Tag) || collision.CompareTag(res3Tag))
        {
            ingredients.Add(collision.gameObject);
            Debug.Log($"Ingredient added: {collision.tag}");
            _resInside = true;
        }
        else
        {
            _anythingInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(bottle0Tag) || collision.CompareTag(bottle1Tag) || collision.CompareTag(bottle2Tag))
        {
            Debug.Log($"Bottle added: {collision.tag}");
            _bottleInside = false;
        }

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
        int res4Count = 0;
        int res5Count = 0;
        int res6Count = 0;


        List<GameObject> ingredientsToRemove = new List<GameObject>();

        foreach (var ingredient in ingredients)
        {
            if (ingredient.CompareTag(res0Tag)) res0Count++;
            if (ingredient.CompareTag(res1Tag)) res1Count++;
            if (ingredient.CompareTag(res2Tag)) res2Count++;
            if (ingredient.CompareTag(res3Tag)) res3Count++;
            if (ingredient.CompareTag(res4Tag)) res4Count++;
            if (ingredient.CompareTag(res5Tag)) res5Count++;
            if (ingredient.CompareTag(res6Tag)) res6Count++;
        }

        if (res0Count == 2 && res1Count == 3 && res2Count == 2 && res3Count == 0 && res4Count == 0 && res5Count == 0 && res6Count == 0)
        {
            ingredientsToRemove.AddRange(ingredients.FindAll(ingredient => ingredient.CompareTag(res0Tag) || ingredient.CompareTag(res1Tag) || ingredient.CompareTag(res2Tag) || ingredient.CompareTag(res3Tag)));
            CreateResult(result0);

            Debug.Log("bottle_0");
        }
        else if (res0Count == 2 && res1Count == 4 && res2Count == 0 && res3Count == 3 && res4Count == 0 && res5Count == 0 && res6Count == 0)
        {
            ingredientsToRemove.AddRange(ingredients.FindAll(ingredient => ingredient.CompareTag(res0Tag) || ingredient.CompareTag(res1Tag) || ingredient.CompareTag(res2Tag) || ingredient.CompareTag(res3Tag)));
            CreateResult(result1);

            Debug.Log("bottle_1");
        }
        else if (res0Count == 0 && res1Count == 0 && res2Count == 0 && res3Count == 0 && res4Count == 5 && res5Count == 3 && res6Count == 4)
        {
            ingredientsToRemove.AddRange(ingredients.FindAll(ingredient => ingredient.CompareTag(res0Tag) || ingredient.CompareTag(res1Tag) || ingredient.CompareTag(res2Tag) || ingredient.CompareTag(res3Tag)));
            CreateResult(result1);

            Debug.Log("bottle_2");
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
                    if (launchRight == true)
                    {
                        rb.velocity = new Vector2(Random.Range(1f, 2f), 1f) * launchForce;
                    }
                    else
                    {
                        rb.velocity = new Vector2(Random.Range(1f, 2f), -1f) * -launchForce;
                    }
                }

                Debug.Log($"Created result: {result.name}");

                // Smoothly transition fillAmount back from 1 to 0
                frontCauldronImage.DOFillAmount(0, 1f).SetEase(Ease.Linear);
            });
        }
    }
}