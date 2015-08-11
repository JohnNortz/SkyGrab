using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour {

    public GameObject target;
    public GameObject ExplosionObject;
    public GameObject parentUnit;
    public GameObject MapControllerPass;
    public float Height;
    public float Speed;
    public float AttackDamage;
    public float damping;
    public bool splash;
    public int TeamNumber;

    private float StartHeight;
    private float MaxHeight;
    private float Distance;
    private float StartDistance;
    private float timer;
    public bool rocket;
    private Vector3 lastpos;
    
    // Use this for initialization
	void Start () {
        if(target != null) StartDistance = Vector3.Distance(target.gameObject.transform.position, transform.position);
        timer = 6f;
	}
	
	// Update is called once per frame
	void Update () {

        if (!target)
        {
            transform.position -= transform.up * Speed * Time.deltaTime * (6 - timer);
            transform.position += transform.forward * Speed * Time.deltaTime;
            timer -= Time.deltaTime;
            if (timer <= 0) Destroy(this.gameObject);
        }
        else 
        {
       
            var LocalPlanar = transform.position;
            if (!rocket) LocalPlanar.y = 1;
            var TargetPlanar = target.gameObject.transform.position;
            if (!rocket) TargetPlanar.y = 1;

            Distance = Vector3.Distance(TargetPlanar, LocalPlanar);

            var lookPos = TargetPlanar - LocalPlanar;
            var rotation = Quaternion.LookRotation(lookPos);
            Debug.DrawRay(transform.position, lookPos, Color.white);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
            transform.position += transform.forward * Speed * Time.deltaTime;
            transform.position += (Distance - (StartDistance * .5f)) * Time.deltaTime * transform.up;

            if (Distance <= .1) Kill();

            if (rocket)
            {
                lookPos = transform.position - lastpos;
                rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1f);
                lastpos = transform.position;
            }

            
        }

	}

    void Kill()
    {
        var Explosion = Instantiate(ExplosionObject, transform.position, Quaternion.identity) as GameObject;
        if (splash)
        {
            var ExploScript = Explosion.GetComponent<AreaExplosion>();
            ExploScript.TeamNumber = TeamNumber;
            ExploScript.MapControllerPass = MapControllerPass;
        }
        if (target.gameObject.name == "GreenSphere" || target.gameObject.name == "RedSphere" || target.gameObject.name == "Blimp")
        {
            var SphereScript = target.gameObject.GetComponent<SphereScript>();
            SphereScript.TakeDamage(AttackDamage);
            Debug.Log("(BomB): I Hit the Sphere!");
            var ParentScript = parentUnit.gameObject.GetComponent<CubeScript>();
            ParentScript.CheckForEnemies();
            if (TeamNumber == 0) MapControllerPass.GetComponent<MapControlScript>().TakeScore("boatDestroid", 0);
            Destroy(this.gameObject);
        }
        if (target.gameObject.name == "GreenCylinder" || target.gameObject.name == "RedCylinder" || target.gameObject.name == "Tug")
        {
            var CylinderScript = target.gameObject.GetComponent<CylinderScript>();
            CylinderScript.TakeDamage(AttackDamage);
            Debug.Log("(BomB): I Hit the Cylinder!");
            var ParentScript = parentUnit.gameObject.GetComponent<CubeScript>();
            ParentScript.CheckForEnemies();
            if (TeamNumber == 0) MapControllerPass.GetComponent<MapControlScript>().TakeScore("boatDestroid", 0);
            Destroy(this.gameObject);
        }
        if (target.gameObject.name == "GreenCube" || target.gameObject.name == "RedCube" || target.gameObject.name == "Skiff") 
        {
            var CubeScript = target.gameObject.GetComponent<CubeScript>();
            CubeScript.TakeDamage(AttackDamage);
            Debug.Log("(BomB): I Hit the Cube!");
            var ParentScript = parentUnit.gameObject.GetComponent<CubeScript>();
            ParentScript.CheckForEnemies();
            if (TeamNumber == 0) MapControllerPass.GetComponent<MapControlScript>().TakeScore("boatDestroid", 0);
            Destroy(this.gameObject);
        }
       
    }
}
