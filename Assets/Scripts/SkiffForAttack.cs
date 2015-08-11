using UnityEngine;
using System.Collections;

public class SkiffForAttack : MonoBehaviour {

    public GameObject Parent;
    public bool Shooting;
    public bool other;   //if cinder or heavens
    public Animator animator;
    public float spd;

	// Use this for initialization
	void Start () {
        Shooting = false;
        animator = gameObject.GetComponent<Animator>();
        animator.speed = spd;
        if (other) animator.SetBool("Other", true);
	}

    void Update()
    {
        if (Shooting)
        {
            animator.SetBool("Shooting", true);
        }
        else animator.SetBool("Shooting", false);
    }
	
	// Update is called once per frame
	public void Shoot () {
        var ParentBool = Parent.gameObject.GetComponent<CubeScript>();
        ParentBool.Shoot();
        ParentBool.Shooting = false;
	}
}
