using UnityEngine;
using System.Collections;

public class CylinderScript : MonoBehaviour {

    public Animator animator;
    public GameObject MapController;
    public GameObject SpawnOnDeath;
    public float MoveSpeed;
    public int TeamNumber;
    public float Hp;                                   //hit points
    public float damping;                              //higher number = higher turn speed
    public float AttackDamage;                         //damage it does to pylons 
    public float sightRange;
    public GameObject BlightExplosion;
    public float IronMawSpeedLoss;
    public float IronMawAttackBuff;
    public float BlightSpeedLoss;
    public int Enumerator;                             //What team buff it gets.  7 = IronMaw move speed increase ;; 8 = Suicide Blight


    private GameObject[] pylons;                       //array that holds all the target units it can move towards
    public GameObject[] MapControllers;                
    private string Targetting;                         //is "Unit" if attacking a thing or "Position" if its just a patrol location
    private GameObject TargetUnit;                     //the GameObject it is targetting (null is a position)
    private Vector3 Target;                            //the Vector position it moves towards
    public int SizeOfMap;
    private string targetName;
    public bool Shooting;
    public AudioClip hit;

    private Vector3 SpawnPoint;

    // Use this for initialization
    void Start()
    {
        this.name = "Tug";
        Shooting = false;

        GameObject[] MapControllers = GameObject.FindGameObjectsWithTag("GameController");

        foreach (var i in MapControllers)
        {
            MapController = i;
        }

        var MAP = MapController.GetComponent<MapControlScript>();
        SizeOfMap = MAP.MapSize;
        Target = GetRandomSpot(SizeOfMap);                              //setting team values
        Targetting = "Position";
        if (TeamNumber == 0)
        {
            SpawnPoint = MAP.Team0SpawnPoint;
            this.tag = "Green";
        }
        if (TeamNumber == 1)
        {
            SpawnPoint = MAP.Team1SpawnPoint;
            this.tag = "Red";
        }
        if (TeamNumber == 2)
        {
            SpawnPoint = MAP.Team1SpawnPoint;
            this.tag = "Blue";
        }
        if (TeamNumber == 3)
        {
            SpawnPoint = MAP.Team1SpawnPoint;
            this.tag = "Yellow";
        }
        if (Enumerator == 7)
        {
            MoveSpeed -= IronMawSpeedLoss;
            this.animator.speed = (1 + IronMawAttackBuff);
        }
        if (Enumerator == 8)
        {
            MoveSpeed -= BlightSpeedLoss;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Shooting)
        {
            animator.SetBool("Attacking", true);
        }
        else
        {
            animator.SetBool("Attacking", false);
        }
        
        if (Targetting != "Position")                                                           //If cylinder isnt moving towards a position it updates the target vector as target-Unit moves
        {
            if (TargetUnit == null)
            {
                Shooting = false;
                Target = GetRandomSpot(SizeOfMap);
                CheckForPylons();
                Targetting = "Position";
            }
            if (TargetUnit != null) 
            {
                Target = TargetUnit.transform.position;
            }
        }
        var lookPos = Target - transform.position;                                                          //this is all the movement stuff
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);

        if (!Shooting) transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        Debug.DrawRay(transform.position, lookPos, Color.yellow);                                            //draws a ray to where its moving

        if (Vector3.Distance(Target, transform.position) <= .6)                                 //When it finds its TargetVector if checks if its targetting a UNIT or a Position
        {                                                                                      //If its a position it finds a new position
            if (Targetting == "Unit" && TargetUnit != null && Vector3.Distance(TargetUnit.transform.position, transform.position) <= .6)
            {
                Shooting = true;

            }
            else if (Targetting == "Position")                                                      //If its targetting a Unit, the cylinder does damage to it and then checks if its still alive
            {                                                                                       //if target unit has died it checks for new targets and then keeps patrolling
                Target = GetRandomSpot(SizeOfMap);
                CheckForPylons();
                Shooting = false;
            }
        }
        if (Vector3.Distance(Target, transform.position) <= 1 && TargetUnit == null && Targetting == "Unit")
        {
            Shooting = false;
            Target = GetRandomSpot(SizeOfMap);
            CheckForPylons();
        }
    }

    void OnTriggerEnter(Collider collision)                                                    //when something comes within range Cube will check if its an enemy and make it the Cube's 
    {                                                                                          //   target if the cube doesnt already have a unit target

        /*foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);                         //this only work for collisions, not triggers
        }*/
        if (collision.gameObject.tag == "Pylon")// && collision.gameObject.GetComponent<PylonScript>().active == false)
        {
            if (Targetting == "Position" && collision.gameObject.GetComponent<PylonScript>().TeamNumber != TeamNumber)
            {
                TargetUnit = collision.gameObject;
                Targetting = "Unit";
            }
        }
    }

    void CheckForPylons()                                                                      //used when Cube spawns or Kills and enemy
    {
        pylons = GameObject.FindGameObjectsWithTag("Pylon");

        int i = 0;
        int j = 0;
        var closestPylon = 8.01F;
        foreach (var unit in pylons)
        {
            i++;
            var dist = Vector3.Distance(unit.transform.position, transform.position);           //checks if the units in the array are within its site range of 8;
            if (dist <= sightRange && unit.gameObject.GetComponent<PylonScript>().TeamNumber != TeamNumber)
            {
                j++;
                if (dist <= closestPylon)
                {
                    TargetUnit = unit;
                    Targetting = "Unit";
                    closestPylon = dist;
                }
            }

        }                               //i and j are just debug helpers <---
        if (closestPylon >= (sightRange + .1F))
        {
            TargetUnit = null;
            Target = GetRandomSpot(SizeOfMap);

        }
    }

    public Vector3 GetRandomSpot(int MapSize)                        //Find new position to patrol to
    {
        var size = MapSize*2;
        var MAP = MapController.GetComponent<MapControlScript>();   //.
        SizeOfMap = MAP.MapSize;
        size = SizeOfMap*2;                                         //'
        Vector3 PosTarget = new Vector3(Random.Range(0, size), .5f, Random.Range(0, size));
        Targetting = "Position";
        return PosTarget;
    }

    public void TakeDamage(float Damage)                           // Called by enemy when Enemy attacks the Cube
    {
        Hp -= Damage;
        if (Hp <= 0)
        {

            var clone = Instantiate(SpawnOnDeath, transform.position, Quaternion.identity) as GameObject;
            clone.transform.rotation = transform.rotation;

            if (Enumerator == 8)
            {
                var bomb = Instantiate(BlightExplosion, transform.position, Quaternion.identity) as GameObject;
                var bombScript = bomb.GetComponent<AreaExplosion>();
                bombScript.TeamNumber = TeamNumber;
                bombScript.explosionRadius = 2;
            }

            Destroy(this.gameObject);
        }
    }

    public void DoDamage()
    {
        if (TargetUnit != null)
        {
            var ATC = TargetUnit.GetComponent<PylonScript>();
            ATC.TakeDamage(AttackDamage, TeamNumber);
            audio.PlayOneShot(hit, 0.7F);
        }
    }
}