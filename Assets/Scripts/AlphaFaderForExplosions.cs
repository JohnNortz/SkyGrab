using UnityEngine;
using System.Collections;

public class AlphaFaderForExplosions : MonoBehaviour {

    public GameObject parent;
    private float explosionRadius;
    
    // Use this for initialization
	void Start () {
	    explosionRadius = parent.gameObject.GetComponent<AreaExplosion>().explosionRadius;
	}
	
	// Update is called once per frame
	void Update () {

        var transformX = parent.transform.localScale.x;
        float colorDif = 1 - (transformX / explosionRadius);

        var originalColour = renderer.material.color;
        renderer.material.color = new Color(originalColour.r, originalColour.g, originalColour.b, colorDif);
	}
}
