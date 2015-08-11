using UnityEngine;
using System.Collections;

public class EjectaScript : MonoBehaviour {

    public float forceRange;

	// Use this for initialization
	void Start () {

        var a = Random.Range(-forceRange, forceRange);
        var b = Random.Range(-forceRange, forceRange);
        var c = Random.Range(0, forceRange);
        
        rigidbody.AddForce(a, b, c);
	}

    void Update()
    {
        rigidbody.AddForce(0, -5, 0);
    }
}
