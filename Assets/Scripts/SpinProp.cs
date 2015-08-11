using UnityEngine;
using System.Collections;

public class SpinProp : MonoBehaviour {

    public float xRate;
    public float yRate;
    public float zRate;

    private Vector3 rate;
    
    // Use this for initialization
    void Start()
    {
        rate = new Vector3(xRate, yRate, zRate);
        rate = rate * Time.deltaTime;
    }
	// Update is called once per frame
	void Update () {
        transform.Rotate(rate);
	}
}
