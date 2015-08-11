using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextureSwap : MonoBehaviour {

 
    public RawImage me;
    public Texture _0;
    public Texture _1;
    public Texture _2;
    public Texture _3;
    public Texture _4;
    public Texture _5;
    public Texture _6;
    public Texture _7;
	
    // Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        var choice = PlayerPrefs.GetInt("House1");
        if (PlayerPrefs.GetInt("House1") == 0)
        {
            me.texture = _0;
        }
        if (PlayerPrefs.GetInt("House1") == 1)
        {
            me.texture = _1;
            //porcelain things here , instantiate the toggle
        }
        if (PlayerPrefs.GetInt("House1") == 2)
        {
            me.texture = _2;
        }
        if (PlayerPrefs.GetInt("House1") == 3)
        {
            me.texture = _3;
        }
        if (PlayerPrefs.GetInt("House1") == 4)
        {
            me.texture = _4;
        }
        if (PlayerPrefs.GetInt("House1") == 5)
        {
            me.texture = _5;
        }
        if (PlayerPrefs.GetInt("House1") == 6)
        {
            me.texture = _6;
        }
        if (PlayerPrefs.GetInt("House1") == 7)
        {
            me.texture = _7;
        }
	}
}
