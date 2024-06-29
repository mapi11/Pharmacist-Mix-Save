using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicScript : MonoBehaviour
{
    [SerializeField] private GameObject[] Objects;
    public int _int = 10;

    public void MakeMusic(int Num)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        Instantiate(Objects[Num], gameObject.transform);
    }

}
