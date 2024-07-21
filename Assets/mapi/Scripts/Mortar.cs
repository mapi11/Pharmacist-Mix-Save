using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;

public class Mortar : MonoBehaviour
{
    //[Header("Res tags")]
    private string res0Tag = "res_0";
    private string res1Tag = "res_1";
    private string res2Tag = "res_2";
    private string res3Tag = "res_3";

    private string bottle999Tag = "bottle_999";
    private string bottle0Tag = "bottle_0";
    private string bottle1Tag = "bottle_1";
    private string bottle2Tag = "bottle_2";

    //[Space]
    //[Header("Prefabs Crushed")]
    ////[SerializeField] private GameObject result999;
    ////[SerializeField] private GameObject Crushed0;
    ////[SerializeField] private GameObject Crushed1;
    ////[SerializeField] private GameObject Crushed2;
    ////[SerializeField] private GameObject Crushed3;

    [Space]
    [Header("Mortar objects")]
    [SerializeField] private Transform spawnPoint;
    //[SerializeField] private Button processButton;

    [SerializeField] private GameObject FrontMortar;
    [SerializeField] private Sprite newSprite;

    private GameObject _canvas;
    private CanvasGroup frontCauldronCanvasGroup;

    private float targetAlpha = 1f;
    private float fadeDuration = 5f;

    private bool _anythingInside;

    private float launchForce = 5f; // Сила запуска объекта

    private List<GameObject> ingredients = new List<GameObject>();

    private void Start()
    {
        _canvas = GameObject.Find("ResContent");

        frontCauldronCanvasGroup = FrontMortar.GetComponent<CanvasGroup>();
    }
    private void Update()
    {
        if (_anythingInside)
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
        //if (collision.CompareTag(res0Tag) || collision.CompareTag(res1Tag) || collision.CompareTag(res2Tag) || collision.CompareTag(res3Tag) || collision.CompareTag(bottle999Tag) || collision.CompareTag(bottle0Tag) || collision.CompareTag(bottle1Tag) || collision.CompareTag(bottle2Tag))
        //{
            ingredients.Add(collision.gameObject);
            Debug.Log($"Ingredient added: {collision.tag}");
        _anythingInside = true; // Устанавливаем булевую переменную в true при добавлении объекта
        //}
        //else
        //{
        //    _anythingInside = true;
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (ingredients.Contains(collision.gameObject))
        //{
            ingredients.Remove(collision.gameObject);
            Debug.Log($"Ingredient removed: {collision.tag}");
            _anythingInside = ingredients.Count > 0;
        //}
        //else
        //{
        //    _anythingInside = false;
        //}
    }

    //private void ProcessIngredients()
    //{
    //    int res0Count = 0;
    //    int res1Count = 0;
    //    int res2Count = 0;
    //    int res3Count = 0;


    //    List<GameObject> ingredientsToRemove = new List<GameObject>();

    //    foreach (var ingredient in ingredients)
    //    {
    //        if (ingredient.CompareTag(res0Tag)) res0Count++;
    //        if (ingredient.CompareTag(res1Tag)) res1Count++;
    //        if (ingredient.CompareTag(res2Tag)) res2Count++;
    //        if (ingredient.CompareTag(res3Tag)) res3Count++;
    //    }


    //    _resInside = ingredients.Count > 0; // Проверяем, остались ли объекты после обработки
    //}

    //private void CreateResult(GameObject result)
    //{
    //    if (result != null)
    //    {
    //        // Replace the image sprite
    //        frontCauldronImage.sprite = newSprite;

    //        // Smoothly transition fillAmount from 0 to 1
    //        frontCauldronImage.fillAmount = 0; // Start with fillAmount at 0
    //        frontCauldronImage.DOFillAmount(1, 1f).SetEase(Ease.Linear).OnComplete(() =>
    //        {
    //            // Create the result after the fillAmount animation completes
    //            GameObject newResult = Instantiate(result, spawnPoint.position, Quaternion.identity, _canvas.transform);

    //            Rigidbody2D rb = newResult.GetComponent<Rigidbody2D>();
    //            if (rb != null)
    //            {
    //                rb.velocity = new Vector2(Random.Range(1f, 2f), 1f) * launchForce; // Set a random launch direction
    //            }

    //            Debug.Log($"Created result: {result.name}");

    //            // Smoothly transition fillAmount back from 1 to 0
    //            frontCauldronImage.DOFillAmount(0, 1f).SetEase(Ease.Linear);
    //        });
    //    }
    //}
}
