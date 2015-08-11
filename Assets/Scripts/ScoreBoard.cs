using UnityEngine;
using System.Collections;

public class ScoreBoard : MonoBehaviour {

    public int boatsDestroid;
    public int boatsBuilt;
    public int buoysDestroid;
    public int buoysCreated;
    public int beaconPlaced;
    public int mostGridsControlled;
    public int totalGasCollected;

    
	// Use this for initialization
	void Awake() {
        DontDestroyOnLoad(this.gameObject);
	}
}
