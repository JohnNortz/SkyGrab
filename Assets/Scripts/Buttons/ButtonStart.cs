using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonStart : MonoBehaviour
{
    public bool TwoPlayerGame;
    public Text theText;

    void Start()
    {
        PlayerPrefs.SetInt("mapSize", 8);
    }


    public void StartGameGOGO(int level)
    {
        if (TwoPlayerGame) PlayerPrefs.SetInt("TwoPlayerGame", 1);
        else PlayerPrefs.SetInt("TwoPlayerGame", 0);
        if (Application.CanStreamedLevelBeLoaded(level))
        {
            Application.LoadLevel(level);
        }
    }

    public void swapBool2player()
    {
        TwoPlayerGame = !TwoPlayerGame;
        if(TwoPlayerGame)
        {
            theText.text = "1 vs. 1";
        }
        if (!TwoPlayerGame)
        {
            theText.text = "4 player FFA ";
        }
    }
}
