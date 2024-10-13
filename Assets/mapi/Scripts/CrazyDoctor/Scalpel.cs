using UnityEngine;
using UnityEngine.UI;

public class Scalpel : MonoBehaviour
{
    public Image targetImage;  // ������ �� ��������� Image, ������� ����� ����������
    public Sprite newSprite;   // ����� �����������, ������� ����� ����������

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
    // ���������, ��� ����� � ���� 1
    //if (collision.gameObject.CompareTag("ScalpelZone"))
    //{
    //    Debug.Log("0");
    //    enteredZone0 = true;

    //    collision.gameObject.GetComponent<Image>().sprite = newSprite;
    //}
    //// ���������, ��� ����� � ���� 2
    //else if (collision.gameObject.CompareTag("ScalpelZone_1"))
    //{
    //    Debug.Log("1");
    //    enteredZone1 = true;

    //    collision.gameObject.GetComponent<Image>().sprite = newSprite;

    //    //Destroy(collision.gameObject);  // ������� ���� 2
    //}

    //// ���� ��������� ������ ��� ����, ������ �����������
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
            targetImage.sprite = newSprite;  // ������ ����������� �� �����
        }
    }
}