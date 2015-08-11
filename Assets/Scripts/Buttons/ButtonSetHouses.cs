using UnityEngine;
using System.Collections;

public class ButtonSetHouses : MonoBehaviour {

    public int TeamNumber;
    public int choice;
    public bool One;
    private int OneOrTwo;
    private int other;
    public GameObject[] buttons;
    public bool FirstBootHouseReset;
    public bool UnlockAllHouses;
    public GameObject firstTutorialWindow;

    public AudioClip clipHover;
    public AudioClip clipSelect;

    void Start()
    {
        Screen.SetResolution(9000, 9000, true);

        if (One) { OneOrTwo = 1; other = 2; }
        if (!One) { OneOrTwo = 2; other = 1;  }
        PlayerPrefs.SetInt("House1", 1);
        PlayerPrefs.SetInt("House2", -1);

        if (FirstBootHouseReset)
        {
            PlayerPrefs.SetInt("HouseUnlock" + 0, 0);
            PlayerPrefs.SetInt("HouseUnlock" + 1, 1);
            PlayerPrefs.SetInt("HouseUnlock" + 2, 0);
            PlayerPrefs.SetInt("HouseUnlock" + 3, 0);
            PlayerPrefs.SetInt("HouseUnlock" + 4, 0);
            PlayerPrefs.SetInt("HouseUnlock" + 5, 0);
            PlayerPrefs.SetInt("HouseUnlock" + 6, 0);
            PlayerPrefs.SetInt("HouseUnlock" + 7, 0);
        }
        if (UnlockAllHouses)
        {
            PlayerPrefs.SetInt("HouseUnlock" + 0, 1);
            PlayerPrefs.SetInt("HouseUnlock" + 1, 1);
            PlayerPrefs.SetInt("HouseUnlock" + 2, 1);
            PlayerPrefs.SetInt("HouseUnlock" + 3, 1);
            PlayerPrefs.SetInt("HouseUnlock" + 4, 1);
            PlayerPrefs.SetInt("HouseUnlock" + 5, 1);
            PlayerPrefs.SetInt("HouseUnlock" + 6, 1);
            PlayerPrefs.SetInt("HouseUnlock" + 7, 1);
        }
    }

    public void HouseButtonPress(int choice)
    {
        if (PlayerPrefs.GetInt("HouseUnlock" + choice) == 1)
        {
            if (PlayerPrefs.GetInt("House1") == choice)
            {
                    PlayerPrefs.SetInt("House1", -1);
            }
            else
            {
                PlayerPrefs.SetInt("House1", choice);
            }
        }
    }

	void OnMouseUpAsButton() 
    {
        buttons = GameObject.FindGameObjectsWithTag("HouseButton");
        //(this.gameObject.GetComponent("Halo") as Behaviour).enabled = true;

        if (PlayerPrefs.GetInt("House1") == choice || PlayerPrefs.GetInt("House2") == choice)
        {
            if (PlayerPrefs.GetInt("House1") == choice)
            {
                PlayerPrefs.SetInt("House1", -1);
                if (PlayerPrefs.GetInt("House2") != -1)
                {
                    PlayerPrefs.SetInt("House1", PlayerPrefs.GetInt("House2"));
                    PlayerPrefs.SetInt("House2", -1);
                }

            }
            else if (PlayerPrefs.GetInt("House2") == choice)
            {
                PlayerPrefs.SetInt("House2", -1);
            }
            
        }
        else if (PlayerPrefs.GetInt("House1") != -1 && PlayerPrefs.GetInt("House2") != -1)
        {
            PlayerPrefs.SetInt("House1", PlayerPrefs.GetInt("House2"));
            PlayerPrefs.SetInt("House2", choice);
        }
        else if (PlayerPrefs.GetInt("House1") == -1)
        {
            PlayerPrefs.SetInt("House1", choice);
        }
        else if (PlayerPrefs.GetInt("House2") == -1)
        {
            PlayerPrefs.SetInt("House2", choice);
        }


        Debug.Log("clicked... House1 now: " + PlayerPrefs.GetInt("House1") + "   House2 now: " + PlayerPrefs.GetInt("House2"));
        
	}

    public void playSoundHover()
    {
        audio.clip = clipHover;
        audio.loop = false;
        audio.Play();
    }

    public void playSoundSelect()
    {
        audio.clip = clipHover;
        audio.loop = false;
        audio.Play();
    }




}
