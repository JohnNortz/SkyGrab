using UnityEngine;
using System.Collections;

public class DeadThingScript : MonoBehaviour {

    public float deathTimer;
    public float fallSpeed;
    public float MoveSpeed;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        deathTimer -= Time.deltaTime;
        if(MoveSpeed >= 0) MoveSpeed -= Time.deltaTime * .5f;
        fallSpeed -= .5f * Time.deltaTime;

        if (deathTimer <= 0)
        {
            Destroy(this.gameObject);
        }

        transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        transform.position += transform.up * fallSpeed * Time.deltaTime;
	}
}
