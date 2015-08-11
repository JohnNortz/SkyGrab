using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreBuilder : MonoBehaviour {

    public GameObject ScoreBoard;
    public Text BotsDest;
    public Text Botsmade;
    public Text BuyDest;
    public Text BuyMade;
    public Text Pylons;
    public Text GridsCont;
    public Text TotalRes;

	// Use this for initialization
	void Start () {
 
        GameObject[] Boards = GameObject.FindGameObjectsWithTag("GameController");

        foreach (var i in Boards)
        {
            ScoreBoard = i;
        }

        var ScoreGet = ScoreBoard.GetComponent<ScoreBoard>();

        BotsDest.text = ScoreGet.boatsDestroid.ToString();
        Botsmade.text = ScoreGet.boatsBuilt.ToString();
        BuyDest.text = ScoreGet.buoysDestroid.ToString();
        BuyMade.text = ScoreGet.buoysCreated.ToString();
        Pylons.text = ScoreGet.beaconPlaced.ToString();
        GridsCont.text = ScoreGet.mostGridsControlled.ToString();
        TotalRes.text = ScoreGet.totalGasCollected.ToString();
	}

}
