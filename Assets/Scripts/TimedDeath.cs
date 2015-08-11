using UnityEngine;
using System.Collections;

public class TimedDeath : MonoBehaviour {

    public float timer;
    public int TeamNumber;
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer <= 0) Destroy(this.gameObject);
	}
}
