using UnityEngine;
using System.Collections;

public class SphereScript : MonoBehaviour {

    public GameObject MapController;
    public GameObject SpawnOnDeath;
    public GameObject Child;
    public float MoveSpeed;
    public int TeamNumber;
    public string TeamName;
    public float Hp;
    public float damping;
    public float sightRange;
    public bool Joves;
    public bool SilentStride;
    public float JoveSpeedLoss;
    public float SilentSpeedBuff;

    private GameObject[] pylons;
    public GameObject[] MapControllers;
    private string Targetting;                         //is "Unit" if attacking a thing or "Position" if its just a patrol location
    private GameObject TargetUnit;
    private Vector3 Target;
    private int SizeOfMap;

    private Vector3 SpawnPoint;

    // Use this for initialization
    void Start()
    {

        this.name = "Blimp";

        GameObject[] MapControllers = GameObject.FindGameObjectsWithTag("GameController");

        foreach (var i in MapControllers)
        {
            MapController = i;
        }



        var MAP = MapController.GetComponent<MapControlScript>();
        SizeOfMap = MAP.MapSize;
        Target = GetRandomSpot(SizeOfMap);
        Targetting = "Position";
        if (TeamNumber == 0)
        {
            SpawnPoint = MAP.Team0SpawnPoint;
            this.tag = "Green";
            TeamName = "Green";
        }
        if (TeamNumber == 1)
        {
            SpawnPoint = MAP.Team1SpawnPoint;
            this.tag = "Red";
            TeamName = "Red";
        }
        if (TeamNumber == 2)
        {
            SpawnPoint = MAP.Team1SpawnPoint;
            this.tag = "Blue";
            TeamName = "Blue";
        }
        if (TeamNumber == 3)
        {
            SpawnPoint = MAP.Team1SpawnPoint;
            this.tag = "Yellow";
            TeamName = "Yellow";
        }


        if (Joves) { MoveSpeed -= JoveSpeedLoss; }
        if (SilentStride) { MoveSpeed += SilentSpeedBuff; }
    }

    // Update is called once per frame
    void Update()
    {
        if (Targetting != "Position")                                                           //If sphere isnt moving towards a position it updates the target vector as target-Unit moves
        {
            if (TargetUnit == null) Target = GetRandomSpot(SizeOfMap);
            if (TargetUnit != null)
            {
                Target = TargetUnit.transform.position;
                var SCRIPT = TargetUnit.GetComponent<PylonScript>();
                if (SCRIPT.active == true)
                {
                    Target = GetRandomSpot(SizeOfMap);
                    CheckForPylons();
                }
            }
            else { CheckForPylons(); Target = GetRandomSpot(SizeOfMap); }
        }
        var lookPos = Target - transform.position;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);    //movement things

        transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        Debug.DrawRay(transform.position, lookPos, Color.green);

        if (Vector3.Distance(Target, transform.position) <= 1)                                 //When it finds its TargetVector if checks if its targetting a UNIT or a Position
        {                                                                                      //If its a position it finds a new position
            if (Targetting == "Unit" && TargetUnit != null)
            {
                var ACT = TargetUnit.GetComponent<PylonScript>();                               //If its a pylon it activates it and then checks for more pylons
                ACT.Activate();
                Target = GetRandomSpot(SizeOfMap);
                CheckForPylons();

            }
            else if (Targetting == "Position")                                                      
            {                                                                                 
                Target = GetRandomSpot(SizeOfMap);
                CheckForPylons();
            }
        }
        if (Vector3.Distance(Target, transform.position) <= 1 && TargetUnit == null && Targetting == "Unit")
        {
            //Debug.Log("Has been destroyed!");
            Target = GetRandomSpot(SizeOfMap);
            CheckForPylons();
        }
    }

    void OnTriggerEnter(Collider collision)                                                 //when something comes within range, Sphere will check if its a pylon and 
    {                                                                                          //   target it if the pylon is its team and not active
        //Debug.Log("Saw Something");
        if (collision.gameObject.tag == "Pylon" && collision.gameObject.GetComponent<PylonScript>().TeamNumber == TeamNumber)// && collision.gameObject.GetComponent<PylonScript>().active == false)
        {
            var SCRIPT = collision.GetComponent<PylonScript>();
            if (Targetting == "Position" && SCRIPT.active == false)
            {
                TargetUnit = collision.gameObject;
                Targetting = "Unit";
                //Debug.Log("Going to Pylon");
            }
        }
    }
  

    void CheckForPylons()                                                                      //used when Sphere spawns or activates a pylon or other things I guess
    {
        pylons = GameObject.FindGameObjectsWithTag("Pylon");

        //Debug.Log("Sphere Checking For Pylons");
        int i = 0;
        int j = 0;
        var closestPylon = 8.01F;
        foreach (var unit in pylons)
        {
            i++;
            var dist = Vector3.Distance(unit.transform.position, transform.position);           //checks if the units in the array are within its site range
            if (dist <= sightRange && unit.gameObject.GetComponent<PylonScript>().active == false && unit.gameObject.GetComponent<PylonScript>().TeamNumber == TeamNumber)
            {
                j++;
                if (dist <= closestPylon)
                {
                    TargetUnit = unit;
                    Targetting = "Unit";
                    closestPylon = dist;
                }
            }
            
        }
        //Debug.Log(i + " valid pylons, " + j + " within range");                                 //i and j are just debug helpers
        if (closestPylon >= (sightRange + .1F))
        {
            TargetUnit = null;
            Target = GetRandomSpot(SizeOfMap);
            //Debug.Log("I saw no Pylons, moving on");

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

    public void TakeDamage(float Damage)                           // Called by enemy when Enemy attacks the sphere
    {
        Hp -= Damage;
        if (Hp <= 0)
        {
            
            var clone = Instantiate(SpawnOnDeath, Child.transform.position, Quaternion.identity) as GameObject;
            clone.transform.rotation = transform.rotation; 
            Destroy(this.gameObject);
        }
    }
}
