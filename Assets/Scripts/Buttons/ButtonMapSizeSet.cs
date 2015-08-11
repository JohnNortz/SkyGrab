using UnityEngine;
using System.Collections;

public class ButtonMapSizeSet : MonoBehaviour
{

    public void ChangeMapSize(int Size)
    {
        PlayerPrefs.SetInt("mapSize", Size);
    }
}
