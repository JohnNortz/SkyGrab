using UnityEngine;
using System.Collections;

public class ButtonChaseToggle : MonoBehaviour {

    public GameObject CameraObject;


    public void ButtonClicked()
    {
        Debug.Log("Button was Pressed");
        
        var Cam = CameraObject.GetComponent<CameraScript>();
        Cam.ToggleChase();
    }
}
