using UnityEngine;
using System.Collections;

public class TextDisplayScript : MonoBehaviour {

    public float FadeTimer;
    public string DisplayText;
    public float sinkSpeed;

    private float xpos;
    private float ypos;

    public GUIStyle myGUIStyle;
 

	// Use this for initialization
	void Start () {
        xpos = Screen.width - 210f;
        ypos = Screen.height - 50f; 
	}
	
	// Update is called once per frame
	void Update () {
        FadeTimer -= Time.deltaTime;
        if (FadeTimer <= 2)
        {
            ypos -= sinkSpeed * Time.deltaTime;
        }
        if (ypos >= Screen.height)
        {
            Destroy(this.gameObject);
        }

	}
    void OnGUI()
    {
        GUI.Box(new Rect(xpos, ypos, 200, 35), DisplayText);
    }
}
