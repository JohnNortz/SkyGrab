using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialWindowSwitcher : MonoBehaviour {

    public GameObject NextWindow;
    public bool last;

    public Texture firstImage;
    public Texture secondImage;
    public RawImage place;
    
    

	// Update is called once per frame
	public void Next() {
        if (!last)
        {
            var window = Instantiate(NextWindow, transform.position, Quaternion.identity) as GameObject;
            window.transform.parent = this.transform.parent;
        }

        Destroy(this.gameObject);
	}

    public void ToggleImages()
    {
        if (firstImage != null && secondImage != null && place != null && place.texture == firstImage)
        {
            place.texture = secondImage;
        }
        else if (firstImage != null && secondImage != null && place != null && place.texture == secondImage)
        {
            place.texture = firstImage;
        }
    }
}
