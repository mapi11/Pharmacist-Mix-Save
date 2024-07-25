using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Res : MonoBehaviour
{
    public GameObject replacementPrefab;

    private int collisionCount = 0;
    private GameObject resContent;

    private void Awake()
    {
        resContent = GameObject.Find("ResContent");
    }

    public void HandleCollision()
    {
        collisionCount++;

        Vector3 scale = transform.localScale;
        scale.y -= 0.05f;
        scale.x -= 0.0025f;
        transform.localScale = scale;

        if (collisionCount >= 4)
        {
            Destroy(gameObject);

            Vector3 position = transform.position;
            Quaternion rotation = transform.rotation;
            Instantiate(replacementPrefab, position, rotation, resContent.transform);
        }
    }
}