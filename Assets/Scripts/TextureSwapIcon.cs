using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextureSwapIcon : MonoBehaviour {

    public int house;
    public RawImage me;

    public int buttonNumber;

    public Texture icon_1;
    public Texture iconBlimp;
    public Texture iconSkiff;
    public Texture iconTug;
    public Texture iconBlimpSp;
    public Texture iconSkiffSp;
    public Texture iconTugSp;
    public Texture iconBouySp;

    public GameObject destroyWith;
    public int ifHouse;

    public bool constantChecking;

    void Start()
    {
        checkIcon();
    }

    void Update()
    {
        if(constantChecking) checkIcon();
    }
	// Update is called once per frame
	public void checkIcon () {
        if (PlayerPrefs.GetInt("House" + house) == -1)
        {
            me.texture = icon_1;
        }
        if (PlayerPrefs.GetInt("House" + house) == 1)
        {
            switch (buttonNumber)
            {
                case 1:
                    me.texture = iconBlimp;
                    break;
                case 2:
                    me.texture = iconSkiff;
                    break;
                case 3:
                    me.texture = iconTug;
                    break;
                case 4:
                    me.texture = iconBouySp;
                    break;
                case 5:
                    me.texture = iconBouySp;
                    break;
            }
            
        }
        if (PlayerPrefs.GetInt("House" + house) == 0 || PlayerPrefs.GetInt("House" + house) == 4 || PlayerPrefs.GetInt("House" + house) == 5)
        {
            switch (buttonNumber)
            {
                case 1:
                    me.texture = iconBlimp;
                    break;
                case 2:
                    me.texture = iconSkiffSp;
                    break;
                case 3:
                    me.texture = iconTug;
                    break;
                case 4:
                    me.texture = icon_1;
                    break;
                case 5:
                    me.texture = iconSkiffSp;
                    break;
            }
        }
        if (PlayerPrefs.GetInt("House" + house) == 2 || PlayerPrefs.GetInt("House" + house) == 3)
        {
            switch (buttonNumber)
            {
                case 1:
                    me.texture = iconBlimpSp;
                    break;
                case 2:
                    me.texture = iconSkiff;
                    break;
                case 3:
                    me.texture = iconTug;
                    break;
                case 4:
                    me.texture = icon_1;
                    break;
                case 5:
                    me.texture = iconBlimpSp;
                    break;
            }
        }
        if (PlayerPrefs.GetInt("House" + house) == 6 || PlayerPrefs.GetInt("House" + house) == 7)
        {
            switch (buttonNumber)
            {
                case 1:
                    me.texture = iconBlimp;
                    break;
                case 2:
                    me.texture = iconSkiff;
                    break;
                case 3:
                    me.texture = iconTug;
                    break;
                case 4:
                    me.texture = icon_1;
                    break;
                case 5:
                    me.texture = iconTugSp;
                    break;
            }
        }

        if (buttonNumber == 4 && destroyWith != null && ifHouse != null && ifHouse != PlayerPrefs.GetInt("House" + house))
        {
            Destroy(destroyWith.gameObject);
        }

	}
}
