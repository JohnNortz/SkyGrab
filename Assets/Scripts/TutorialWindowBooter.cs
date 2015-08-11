using UnityEngine;
using System.Collections;

public class TutorialWindowBooter : MonoBehaviour {

    public GameObject firstTutorialWindow;

	// Use this for initialization
    public void ShowTutorial()
    {
        var window = Instantiate(firstTutorialWindow, transform.position, Quaternion.identity) as GameObject;
        window.transform.parent = this.gameObject.transform;
    }
}
