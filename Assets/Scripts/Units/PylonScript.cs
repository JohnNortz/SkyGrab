using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PylonScript : MonoBehaviour {

    public string Team;
    public int TeamNumber;
    public float Hp;
    public float BouyHp;
    public bool active;
    public int scale;
    public GameObject MapController;
    private GameObject[] MapControllers;
    private GameObject[] Bases;
    public GameObject SpawnOnDeathActive;
    public GameObject SpawnOnDeathBeacon;
    public GameObject Child;
    public GameObject TeamHomeBase;
    public int MapSize;
    public ParticleSystem Sparks;
    public bool BasePylon = false;

    public GameObject damagePS;
    public GameObject damagePS1;
    public GameObject damagePSe;
    public Vector3 particleLoc;
    private Vector3 particleLocR;

    private Animator anim;

    public int LocalX;      //these two are the locations of the pylon in MapControllers array of vertexs
    public int LocalZ;

    public bool Link1 = false;
    public bool Link2 = false;
    public bool Link3 = false;
    public bool Link4 = false;
    private GameObject DmgPart;
    public AudioClip activateBells;
    public AudioSource source;
    public float pitchVariance;

    // Use this for initialization
	void Start () {

        GameObject[] MapControllers = GameObject.FindGameObjectsWithTag("GameController");
        BasePylon = false;

        particleLocR = new Vector3((transform.position.x + particleLoc.x), (transform.position.y + particleLoc.y), (transform.position.z + particleLoc.z));

        foreach (var i in MapControllers)
        {
            MapController = i;
        }

        GameObject[] Bases = GameObject.FindGameObjectsWithTag("Base");

        foreach (var i in Bases)
        {
            TeamHomeBase = i;
        }

        anim = GetComponent<Animator>();
        anim.SetBool("Active", false);
        active = false;
        var Map = MapController.gameObject.GetComponent<MapControlScript>();
        MapSize = Map.MapSize;
        if (TeamNumber == 0) Map.TakeScore("pylonPlaced", 0);
        scale = Map.Scale;

        if (TeamNumber == 0) { Team = "Green"; }
        if (TeamNumber == 1) { Team = "Red"; }
        if (TeamNumber == 2) { Team = "Blue"; }
        if (TeamNumber == 3) { Team = "Yellow"; }

        LocalX = (int) transform.position.x;
        LocalZ = (int) transform.position.z;

        if (active) Activate();


	}

	
    //called when a Blimp builds a Bouy at this location
	public void Activate() {
        DmgPart = Instantiate(damagePSe, particleLocR, Quaternion.identity) as GameObject;
        anim.SetBool("Active", true);
        active = true;
        source.pitch = source.pitch + (Random.Range(pitchVariance, -pitchVariance));
        if(!BasePylon)audio.PlayOneShot(activateBells, .15f);
        Hp = BouyHp;

        var Map = MapController.gameObject.GetComponent<MapControlScript>();
        if (TeamNumber == 0) Map.TakeScore("buoyBuilt", 0);
        Map.vertex[LocalX, LocalZ] = Team;

        int temp = MapSize * scale;
       // Map.CheckPylons(LocalX, LocalZ, TeamNumber, new string[temp, temp], new List<int[]>());
        Map.CheckSquare(LocalX, LocalZ);

	}

    public void TakeDamage(float Damage, int AttackingTeam)                           // Called by enemy when Enemy attacks the Pylon
    { 
        Hp -= Damage;
        Sparks.Emit(20);
        var Map = MapController.gameObject.GetComponent<MapControlScript>();
        if (DmgPart != null) Destroy(DmgPart.gameObject);
        if (Hp <= 0)
        {
            if (AttackingTeam == 0)  Map.TakeScore("buoyDestroid", 0);
            if (active)
            {
                var DeadPylon = Instantiate(SpawnOnDeathActive, Child.transform.position, Quaternion.identity) as GameObject;

                int temp = MapSize * scale;
                //Map.CheckPylons(LocalX, LocalZ, TeamNumber, new string[temp, temp], new List<int[]>());
                Map.KillFill(LocalX, LocalZ);
            }
            if (!active)
            {
                var DeadPylon = Instantiate(SpawnOnDeathBeacon, Child.transform.position, Quaternion.identity) as GameObject;
            }

            if (BasePylon && TeamHomeBase != null)
            {
                Debug.Log("-----------  I WAS A BASE PYLON at " + LocalX + ", " + LocalZ + ".   And I was just Destroied --------------------------------------------");
                var baseScript = TeamHomeBase.GetComponent<AIController>();
                baseScript.TeamLoss();
            }

            if (DmgPart != null) Destroy(DmgPart.gameObject);

            Map.vertex[LocalX, LocalZ] = "null";
            Destroy(this.gameObject);
        }
        else if (Hp <= 3)
        {
            if (DmgPart != null) Destroy(DmgPart.gameObject);
            DmgPart = Instantiate(damagePS1, particleLocR, Quaternion.identity) as GameObject;
        }
        else if (Hp <= 6)
        {
            if (DmgPart != null) Destroy(DmgPart.gameObject);
            DmgPart = Instantiate(damagePS, particleLocR, Quaternion.identity) as GameObject;
        }
    }

    public void getPylons()
    {

    }

    public void checkSurroundingPylons()
    {

    }

}
