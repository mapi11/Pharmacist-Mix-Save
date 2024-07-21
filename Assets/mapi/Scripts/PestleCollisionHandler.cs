using UnityEngine;

public class PestleCollisionHandler : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;

    private bool isMouseDown = false;
    private bool isInMortar = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == targetObject)
            {
                isMouseDown = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isMouseDown && isInMortar == true && (other.CompareTag("res_0") || other.CompareTag("res_1") || other.CompareTag("res_2") || other.CompareTag("res_3") || other.CompareTag("res_4")))
        {
            Res tagObjectHandler = other.GetComponent<Res>();
            if (tagObjectHandler != null)
            {
                tagObjectHandler.HandleCollision();
            }
        }

        if (other.CompareTag("Mortar"))
        {
            isInMortar = true;
        }
    }



    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Mortar"))
        {
            isInMortar = false;
        }
    }
}
