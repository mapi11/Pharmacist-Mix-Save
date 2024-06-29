using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePrefScript : MonoBehaviour
{
    public enum PrefTypes { Music, Languages, Volume };

    public static void Save(PrefTypes prefTypes, int Value)
    {
        PlayerPrefs.SetInt(prefTypes.ToString(), Value);
    }

    public static int Load(PrefTypes prefTypes)
    {
        return PlayerPrefs.GetInt(prefTypes.ToString());
    }
}
