using UnityEngine;
using System.Collections;

public class CubeScript : MonoBehaviour {

    public GameObject SpawnOnDeath;
    public GameObject Child;
    public GameObject MapController; //needs this to get certain information about the map
    public GameObject SuicideExplosion;
    public float SuicideExplosionRadius;
    public float BoneMaulAttackDamperTime;
    public float HeavensAttackRangeBonus;
    public float SolsAttackRangeBonus;
    public float MoveSpeed;
    public float AttackDamage;                         //damage it does to other enemies
    public int TeamNumber;                            //"Red" or "Green"
    public GameObject[] MapControllers;
    public GameObject[] enemies;                      //when it searches for nearby targets it uses this array as a list of all viable targets and then compares distance to them
    public float Hp;                                   //hit points
    public float damping;                              //greater the number the faster the turn speed
    public float attackRange;
    public float sightRange;                     
    public GameObject Bomb;
    public int Enumerator;                             //What team buff it gets.  1 = cinderForge suicide bomb ;; 5 = SolsWrath Attack Slower with splash ;; 6 = Heavens Reach longer attack range ;; all else normal


    private string Targetting;                         //is "Unit" if attacking a thing or "Position" if its just a patrol location
    private GameObject TargetUnit;                     //the GameObject target (its null if it doesnt see an enemy)
    private Vector3 Target;                            //the position it is moving towards (is the transform.position of TargetUnit if not null)
    public int SizeOfMap;                             //Grabbed from the map controller (currently on the ground plane) so it has the range of its allowed movement

    private string otherTeamCube;                      //probably there is a better way to do this, but it needs to know what its attacking so that it can properly give damage when attacking
    private string otherTeamSphere;
    private string otherTeamCylinder;
    public bool Shooting; 
    
    
    // Use this for initialization
	void Start () 
    {
        this.name = "Skiff";
        Shooting = false;

        var ChildScript = Child.GetComponent<SkiffForAttack>();
        ChildScript.spd = 1f;

        GameObject[] MapControllers = GameObject.FindGameObjectsWithTag("GameController");

        foreach (var i in MapControllers)
        {
            MapController = i;
        }


        var MAP = MapController.GetComponent<MapControlScript>();    //other map controller things go here
        SizeOfMap = MAP.MapSize;
        Debug.Log("Skiff Booting Up:: MapSize is: " + SizeOfMap + "  Should be: " + MAP.MapSize);
        Target = GetRandomSpot(SizeOfMap);
        if (TeamNumber == 0)                                        //these two statments just set values based on team name
        {
            this.tag = "Green";
            otherTeamSphere = "Blimp";
            otherTeamCube = "Skiff";
            otherTeamCylinder = "Tug";
            transform.Find("Trail").gameObject.GetComponent<TrailRenderer>().material.SetColor("_TintColor", Color.green);
        }
        if (TeamNumber == 1)
        {
            this.tag = "Red";
            otherTeamSphere = "Blimp";
            otherTeamCube = "Skiff";
            otherTeamCylinder = "Tug";
            transform.Find("Trail").gameObject.GetComponent<TrailRenderer>().material.SetColor("_TintColor", Color.red);
        }
        if (TeamNumber == 2)
        {
            this.tag = "Blue";
            otherTeamSphere = "Blimp";
            otherTeamCube = "Skiff";
            otherTeamCylinder = "Tug";
            transform.Find("Trail").gameObject.GetComponent<TrailRenderer>().material.SetColor("_TintColor", Color.blue);
        }
        if (TeamNumber == 3)
        {
            this.tag = "Yellow";
            otherTeamSphere = "Blimp";
            otherTeamCube = "Skiff";
            otherTeamCylinder = "Tug";
            transform.Find("Trail").gameObject.GetComponent<TrailRenderer>().material.SetColor("_TintColor", Color.yellow);
        }
        if (Enumerator == 5)
        {
            ChildScript.spd = .5f;
            attackRange += SolsAttackRangeBonus;
        }
        if (Enumerator == 6) 
        { 
            attackRange += HeavensAttackRangeBonus;
            ChildScript.spd = .4f;
        }


        Targetting = "Position";
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Shooting)
        {
            var ChildScript = Child.GetComponent<SkiffForAttack>();
            ChildScript.Shooting = true;
        }
        if (!Shooting)
        {
            var ChildScript = Child.GetComponent<SkiffForAttack>();
            ChildScript.Shooting = false;
        }

        if (Targetting != "Position")                                                           //If cube isnt moving towards a position it updates the target vector as target-Unit moves
        {                                                                            
            if(TargetUnit != null) Target = TargetUnit.transform.position;                      //makes the target vector follow the target game object
        }

        var lookPos = Target - transform.position;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);

        if (!Shooting) transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        Debug.DrawRay(transform.position, lookPos, Color.red);

        if (Vector3.Distance(Target, transform.position) <= 3 && Targetting == "Position")                                 //When it finds its TargetVector if checks if its targetting a UNIT or a Position
        {                                                                                      //If its a position it finds a new position
            Target = GetRandomSpot(SizeOfMap);                                             //if target unit has died it checks for new targets and then keeps patrolling
            CheckForEnemies();                                                                  //If its a Unit it does damage to it and then checks if its still alive
            Shooting = false;
        }
        if (Vector3.Distance(Target, transform.position) <= attackRange && Targetting == "Unit" && TargetUnit != null && Enumerator != 1)
        {
            if (TargetUnit.name == otherTeamCube)
            {
                Shooting = true;
            }
            if (TargetUnit.name == otherTeamSphere)
            {
                Shooting = true;
            }
            if (TargetUnit.name == otherTeamCylinder)
            {
                Shooting = true;
            }
                

        }
        if (Vector3.Distance(Target, transform.position) <= .6f && Targetting == "Unit" && TargetUnit != null && Enumerator == 1)
        {
            TakeDamage(10);
        }
        
        if (TargetUnit == null && Targetting == "Unit")        //this statement was supposed to be the check if it is attacking soemthing that no longer exists
        {
            //Debug.Log("Has been destroyed!");
            Shooting = false;
            CheckForEnemies();
        }
	}

    void OnTriggerEnter(Collider collision)                                                 //when something comes within range Cube will check if its an enemy and make it the Cube's 
    {                                                                                          //   target if the cube doesnt already have a unit target
        //Debug.Log("Sphere Saw Something");
        if (collision.gameObject.tag != this.tag)
        {
            if (Targetting == "Position")
            {
                if (collision.name == "Tug" || collision.name == "Skiff" || collision.name == "Blimp" )
                {
                    TargetUnit = collision.gameObject;
                    Targetting = "Unit";
                    //Debug.Log("Attacking");
                }    

            }
        }
    }

    public void CheckForEnemies()                                                                      //used when Cube spawns or Kills and enemy or reaches a location
    {
        
        enemies = new GameObject[250];
        var closestEnemy = 8.01F;
        Shooting = false;
        for (int i = 0; i < 4; i++ )
        {
            if (i == 0 && TeamNumber != 0) enemies = (GameObject.FindGameObjectsWithTag("Green"));
            if (i == 1 && TeamNumber != 1) enemies = (GameObject.FindGameObjectsWithTag("Red"));
            if (i == 2 && TeamNumber != 2) enemies = (GameObject.FindGameObjectsWithTag("Blue"));
            if (i == 3 && TeamNumber != 3) enemies = (GameObject.FindGameObjectsWithTag("Yellow"));


            //Debug.Log("Checking For Enemies");
            
            foreach (var unit in enemies)
            {
                var dist = 10f;
                if (unit != null)
                {
                    dist = Vector3.Distance(unit.transform.position, transform.position); //checks if the units in the array are within its site range of 8;
                }
                if (dist <= sightRange)
                {
                    if (dist <= closestEnemy)
                    {
                        TargetUnit = unit;
                        closestEnemy = dist;
                    }
                }
            }
        }
        if (closestEnemy == 8.01F)
        {
            TargetUnit = null;
            Target = GetRandomSpot(SizeOfMap);

        }
    }

    public Vector3 GetRandomSpot(int MapSize)                        //Find new position to patrol to

    {
        //Debug.Log("Location Reached, Moving to new Location");
        var size = MapSize*2;
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

            if (Enumerator == 1)
            {
                var SuicideBomb = Instantiate(SuicideExplosion, transform.position, Quaternion.identity) as GameObject;
                var bombScript = SuicideBomb.GetComponent<AreaExplosion>();
                bombScript.TeamNumber = TeamNumber;
                bombScript.explosionRadius = SuicideExplosionRadius;
            }

            Destroy(this.gameObject);
        }
    }

    public void Shoot()
    {
        var pos = transform.position;
        pos.x += .05f;
        var Bombshot = Instantiate(Bomb, pos, Quaternion.identity) as GameObject;
        var BombScript = Bombshot.GetComponent<BombScript>();
        BombScript.target = TargetUnit;
        BombScript.parentUnit = this.gameObject;
        BombScript.TeamNumber = TeamNumber;
        BombScript.MapControllerPass = MapController;
        Shooting = false;
    }

}
