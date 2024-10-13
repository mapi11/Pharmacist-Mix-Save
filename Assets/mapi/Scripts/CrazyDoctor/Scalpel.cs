using UnityEngine;
using UnityEngine.UI;

public class Scalpel : MonoBehaviour
{
    public Image targetImage;  // Ссылка на компонент Image, который будет изменяться
    public Sprite newSprite;   // Новое изображение, которое нужно установить

    [Space]
    public string ScalpelZoneTag_0;
    public string ScalpelZoneTag_1;

    [Space]
    public int ScalpelZoneCount = 0;
    public int ScalpelInt = 0;

    //public bool enteredZone0 = false;
    //public bool enteredZone1 = false;

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    // Проверяем, что зашли в зону 1
    //if (collision.gameObject.CompareTag("ScalpelZone"))
    //{
    //    Debug.Log("0");
    //    enteredZone0 = true;

    //    collision.gameObject.GetComponent<Image>().sprite = newSprite;
    //}
    //// Проверяем, что зашли в зону 2
    //else if (collision.gameObject.CompareTag("ScalpelZone_1"))
    //{
    //    Debug.Log("1");
    //    enteredZone1 = true;

    //    collision.gameObject.GetComponent<Image>().sprite = newSprite;

    //    //Destroy(collision.gameObject);  // Удаляем зону 2
    //}

    //// Если скальпель прошел обе зоны, меняем изображение
    //if (enteredZone0 && enteredZone1)
    //{
    //    ChangeImage();
    //}
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ScalpelZone"))
        {
            Debug.Log(ScalpelInt);

            ScalpelInt++;

            Destroy(collision.gameObject);
            //collision.gameObject.GetComponent<Image>().sprite = newSprite;
        }

        //if (collision.CompareTag(ScalpelZoneTag_1))
        //{
        //    Debug.Log("1");
        //    //enteredZone1 = true;

        //    collision.gameObject.GetComponent<Image>().sprite = newSprite;
        //}

        if (ScalpelInt == ScalpelZoneCount)
        {
            ChangeImage();
        }
    }

    private void ChangeImage()
    {
        if (targetImage != null && newSprite != null)
        {
            targetImage.sprite = newSprite;  // Меняем изображение на новое
        }
    }
}