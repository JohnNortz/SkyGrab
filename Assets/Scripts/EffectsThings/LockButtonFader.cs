using UnityEngine;
using System.Collections;

public class LockButtonFader : MonoBehaviour {

    public int HouseNumber;

	// Use this for initialization
    void Start()
    {

        if(PlayerPrefs.GetInt("HouseUnlock" + HouseNumber) == 1)
        {
            Destroy(this.gameObject);
            Debug.Log("I should be unlocked now");
        }

    }
}
