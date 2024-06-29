using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Cauldron : MonoBehaviour
{
    public string res0Tag = "res_0";
    public string res1Tag = "res_1";
    public GameObject result0; // Объект, который появится при двух res_0
    public GameObject result1; // Объект, который появится при res_0 и res_1
    public Transform spawnPoint; // Точка, где будут появляться новые объекты
    public Button processButton; // Кнопка для обработки рецептов

    private GameObject _canvas;

    private List<GameObject> ingredients = new List<GameObject>();

    private void Start()
    {
        _canvas = GameObject.Find("MainCanvas");

        if (processButton != null)
        {
            processButton.onClick.AddListener(ProcessIngredients);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(res0Tag) || collision.CompareTag(res1Tag))
        {
            ingredients.Add(collision.gameObject);
            Debug.Log($"Ingredient added: {collision.tag}");

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (ingredients.Contains(collision.gameObject))
        {
            ingredients.Remove(collision.gameObject);
            Debug.Log($"Ingredient removed: {collision.tag}");
        }
    }

    private void ProcessIngredients()
    {
        int res0Count = 0;
        int res1Count = 0;

        List<GameObject> ingredientsToRemove = new List<GameObject>();

        foreach (var ingredient in ingredients)
        {
            if (ingredient.CompareTag(res0Tag)) res0Count++;
            if (ingredient.CompareTag(res1Tag)) res1Count++;
        }

        if (res0Count == 2 && res1Count == 0)
        {
            ingredientsToRemove.AddRange(ingredients.FindAll(ingredient => ingredient.CompareTag(res0Tag)));
            CreateResult(result0);

            Debug.Log("res_0 + res_0");
        }
        else if (res0Count == 1 && res1Count == 1)
        {
            ingredientsToRemove.AddRange(ingredients.FindAll(ingredient => ingredient.CompareTag(res0Tag) || ingredient.CompareTag(res1Tag)));
            CreateResult(result1);

            Debug.Log("res_0 + res_1");
        }

        foreach (var ingredient in ingredientsToRemove)
        {
            ingredients.Remove(ingredient);
            Destroy(ingredient);
        }
    }

    private void CreateResult(GameObject result)
    {
        if (result != null)
        {
            Instantiate(result, spawnPoint.position, Quaternion.identity, _canvas.transform);
            Debug.Log($"Created result: {result.name}");
        }
    }
}
