using UnityEngine;
using System.Collections;

public class FillScript : MonoBehaviour {

    public GameObject ResourceManager;

    public int TeamNumber;

    public Texture GreenTeam;
    public Texture RedTeam;
    public Texture BlueTeam;
    public Texture YellowTeam;

	void Start () {
        if (TeamNumber == 0) { renderer.material.SetTexture("_MainTex", GreenTeam); }
        if (TeamNumber == 1) { renderer.material.SetTexture("_MainTex", RedTeam); }
        if (TeamNumber == 2) { renderer.material.SetTexture("_MainTex", BlueTeam); }
        if (TeamNumber == 3) { renderer.material.SetTexture("_MainTex", YellowTeam); }
	}

    void OnDestroy()
    {
        var Script = ResourceManager.GetComponent<ResourceManager>();
        Script.Incomes[TeamNumber] -= 1;
    }
	
}
