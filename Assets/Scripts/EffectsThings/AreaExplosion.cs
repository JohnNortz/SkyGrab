using UnityEngine;
using System.Collections;

public class AreaExplosion : MonoBehaviour {

    public int TeamNumber;
    public float explosionRadius;
    public float Damage;
    public GameObject MapControllerPass;
    public GameObject Explosion;
    private float timeSinceBoom;


    void Start()
    {
        transform.localScale = new Vector3(0.1F, 0.1F, 0.1F);
        var Boom = Instantiate(Explosion, transform.position, Quaternion.identity) as GameObject;
        timeSinceBoom = 0;
    }

	// Update is called once per frame
	void Update () {

        timeSinceBoom += Time.deltaTime * .08f;
        float expansion = .1f - timeSinceBoom;
        if (transform.localScale.x <= explosionRadius)
        {
            transform.localScale += new Vector3(expansion, expansion, expansion);
        }
        if (transform.localScale.x >= explosionRadius)
        {
            Destroy(this.gameObject);
        }

	}

    void OnTriggerEnter(Collider collision)                                                 //when something comes within range Cube will check if its an enemy and make it the Cube's 
    {                                                                                          //   target if the cube doesnt already have a unit target
        if (collision.gameObject.name == "Skiff" || collision.gameObject.name == "GreenCube" || collision.gameObject.name == "RedCube") 
        { 
            var Script = collision.gameObject.GetComponent<CubeScript>();
            if (Script.TeamNumber != TeamNumber)
            {
                Script.TakeDamage(Damage);
                if (TeamNumber == 0) MapControllerPass.GetComponent<MapControlScript>().TakeScore("boatDestroid", 0);
            }
                
        }
        if (collision.gameObject.name == "Tug" || collision.gameObject.name == "GreenCylinder" || collision.gameObject.name == "RedCylinder") 
        {
            var Script = collision.gameObject.GetComponent<CylinderScript>(); 
            if (Script.TeamNumber != TeamNumber)
            {
                Script.TakeDamage(Damage);
                if (TeamNumber == 0) MapControllerPass.GetComponent<MapControlScript>().TakeScore("boatDestroid", 0);

            }
        }
        if (collision.gameObject.name == "Blimp" || collision.gameObject.name == "GreenSphere" || collision.gameObject.name == "RedSphere") 
        {
            var Script = collision.gameObject.GetComponent<SphereScript>(); 
            if (Script.TeamNumber != TeamNumber)
            {
                Script.TakeDamage(Damage);
                if (TeamNumber == 0) MapControllerPass.GetComponent<MapControlScript>().TakeScore("boatDestroid", 0);
            }
        }

            
            //Debug.Log("Boom");
    }

}
