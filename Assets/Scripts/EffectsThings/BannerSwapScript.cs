using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BannerSwapScript : MonoBehaviour {

    public Texture _0;
    public Texture _1;
    public Texture _2;
    public Texture _3;
    public Texture _4;
    public Texture _5;
    public Texture _6;
    public Texture _7;
    public bool firstHouse;
    public RawImage theThing;

	// Use this for initialization
    void Start()
    {

        if (PlayerPrefs.GetInt("House1") == 0)
        {
            theThing.texture = _0;
        }
        if (PlayerPrefs.GetInt("House1") == 1)
        {
            theThing.texture = _1;
            //porcelain things here , instantiate the toggle
        }
        if (PlayerPrefs.GetInt("House1") == 2)
        {
            theThing.texture = _2;
        }
        if (PlayerPrefs.GetInt("House1") == 3)
        {
            theThing.texture = _3;
        }
        if (PlayerPrefs.GetInt("House1") == 4)
        {
            theThing.texture = _4;
        }
        if (PlayerPrefs.GetInt("House1") == 5)
        {
            theThing.texture = _5;
        }
        if (PlayerPrefs.GetInt("House1") == 6)
        {
            theThing.texture = _6;
        }
        if (PlayerPrefs.GetInt("House1") == 7)
        {
            theThing.texture = _7;
        }
    }
}
