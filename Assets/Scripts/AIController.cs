using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIController : MonoBehaviour {

    private GameObject MapController;
    private GameObject[] MapControllers;
    private GameObject ResourceCube;
    private GameObject[] ResourceCubes;
    public int TeamNumber;
    public string TeamName;
    public Vector3 Homebase;
    public int HouseNumber;         // Cinder 0 ;  HoP 1 ; Joves 2 ;  Silent 3  ;  Sols 4  ;  Heavens 5  ;  Iron 6  ;  HoB  7

    public GameObject Blimp;
    public GameObject Skiff;
    public GameObject Tug;
    public GameObject Bouy;

    public GameObject DefaultBlimp;
    public GameObject DefaultSkiff;
    public GameObject DefaultTug;
    public GameObject DefaultBouy;
    public GameObject CinderSkiff;
    public GameObject CinderTug;
    public GameObject CinderBlimp;
    public GameObject PorcelainBouy;
    public GameObject PorcelainBlimp;
    public GameObject PorcelainTug;
    public GameObject PorcelainSkiff;
    public GameObject JoveBlimp;
    public GameObject JoveTug;
    public GameObject JoveSkiff;
    public GameObject SilentStBlimp;
    public GameObject SilentStTug;
    public GameObject SilentStSkiff;
    public GameObject HeavensSkiff;
    public GameObject HeavensBlimp;
    public GameObject HeavensTug;
    public GameObject IornMawBlimp;
    public GameObject IornMawTug;
    public GameObject IornMawSkiff;
    public GameObject BlightTug;
    public GameObject BlightSkiff;
    public GameObject BlightBlimp;
    public GameObject SolsBlimp;
    public GameObject SolsTug;
    public GameObject SolsSkiff;

    public GameObject BOUY;
    public GameObject BOUY1;
    public GameObject BOUY2;
    public GameObject BOUY3;

    public float Rate_Spawn;
    public float Rate_Timer;
    public float First_Timer;

    public GameObject displayTextObject;

    public GameObject[] myBouys;
    public GameObject[] otherAI;
    public List<Vector3> open = new List<Vector3>();
    
    // Use this for initialization
	void Start () 
    {
        if (TeamNumber != 0)
        {
            HouseNumber = Random.Range(0, 8);
            GameObject[] otherAI = GameObject.FindGameObjectsWithTag("Base");
            foreach (var i in otherAI)
            {
                var AI = i.GetComponent<AIController>();
                if (AI.HouseNumber == HouseNumber)
                {
                    HouseNumber = Random.Range(0, 8);
                }
            }
        }
                
        GameObject[] MapControllers = GameObject.FindGameObjectsWithTag("GameController");

        foreach (var i in MapControllers)
        {
            MapController = i;
        }

        GameObject[] ResourceCubes = GameObject.FindGameObjectsWithTag("ResourceCube");

        foreach (var i in ResourceCubes)
        {
            ResourceCube = i;
        }
        
        var locationScript = MapController.GetComponent<MapControlScript>(); 
        if (TeamNumber == 0)
        {
            TeamName = "Green";
        }
        if (TeamNumber == 1)
        {
            TeamName = "Red";
        }
        if (TeamNumber == 2)
        {
            TeamName = "Blue";
        }
        if (TeamNumber == 3)
        {
            TeamName = "Yellow";
        }

        Homebase = this.transform.position;

            Blimp = DefaultBlimp; 
            if (locationScript.playersShips[TeamNumber, 0] == -1) { Blimp = DefaultBlimp; }
            if (locationScript.playersShips[TeamNumber, 0] == 2) { Blimp = JoveBlimp; }
            if (locationScript.playersShips[TeamNumber, 0] == 3) { Blimp = SilentStBlimp; }

            Skiff = DefaultSkiff;
            if (locationScript.playersShips[TeamNumber, 1] == 0) { Skiff = CinderSkiff; }
            if (locationScript.playersShips[TeamNumber, 1] == 4) { Skiff = SolsSkiff; }
            if (locationScript.playersShips[TeamNumber, 1] == 5) { Skiff = HeavensSkiff; }

            Tug = DefaultTug;
            if (locationScript.playersShips[TeamNumber, 2] == 6) { Tug = IornMawTug; }
            if (locationScript.playersShips[TeamNumber, 2] == 7) { Blimp = BlightTug; }

            Bouy = DefaultBouy;
            //if (locationScript.playersShips[TeamNumber, 3] == 1) { Bouy = PorcelainBouy; }

            Rate_Timer = Rate_Spawn;
            Homebase = this.transform.position;

            if (HouseNumber == 0)
            {
                Blimp = CinderBlimp;
                Skiff = CinderSkiff;
                Tug = CinderTug;
            }
            if (HouseNumber == 1)
            {
                Blimp = PorcelainBlimp; ;
                Skiff = PorcelainSkiff;
                Tug = PorcelainTug;
            }
            if (HouseNumber == 2)
            {
                Blimp = JoveBlimp;
                Skiff = JoveSkiff;
                Tug = JoveTug;
            }
            if (HouseNumber == 3)
            {
                Blimp = SilentStBlimp;
                Skiff = SilentStSkiff;
                Tug = SilentStTug;
            }
            if (HouseNumber == 4)
            {
                Blimp = SolsBlimp;
                Skiff = SolsSkiff;
                Tug = SolsTug;
            }
            if (HouseNumber == 5)
            {
                Blimp = HeavensBlimp;
                Skiff = HeavensSkiff;
                Tug = HeavensTug;
            }
            if (HouseNumber == 6)
            {
                Blimp = IornMawBlimp;
                Skiff = IornMawSkiff;
                Tug = IornMawTug;
            }
            if (HouseNumber == 7)
            {
                Blimp = BlightBlimp;
                Skiff = BlightSkiff;
                Tug = BlightTug;
            }


	}
	

	void Update () 
    {

        if (TeamNumber != 0)
        {
            if (Rate_Timer > 0) Rate_Timer -= Time.deltaTime;
            if (Rate_Timer <= 0)
            {
                var locationScript = MapController.GetComponent<MapControlScript>();
                var resourceScript = ResourceCube.GetComponent<ResourceManager>();
                if (resourceScript.Teams[TeamNumber] > 35)
                {
                    SpawnThings(1, 4, 1, 3);
                    resourceScript.Teams[TeamNumber] -= 34;
                }
                if (resourceScript.Teams[TeamNumber] <= 35 && resourceScript.Teams[TeamNumber] > 25)
                {
                    SpawnThings(0, 2, 2, 0);
                    resourceScript.Teams[TeamNumber] -= 32;
                }
                if (resourceScript.Teams[TeamNumber] <= 25 && resourceScript.Teams[TeamNumber] > 15)
                {
                    SpawnThings(0, 1, 1, 5);
                    resourceScript.Teams[TeamNumber] -= 19;
                }
                if (resourceScript.Teams[TeamNumber] <= 15 && resourceScript.Teams[TeamNumber] > 8)
                {
                    SpawnThings(0, 1, 0, 5);
                    resourceScript.Teams[TeamNumber] -= 9;
                }
                if (resourceScript.Teams[TeamNumber] <= 8 && resourceScript.Teams[TeamNumber] > 5)
                {
                    SpawnThings(1, 0, 0, 4);
                    resourceScript.Teams[TeamNumber] -= 7;
                }
                if (resourceScript.Teams[TeamNumber] <= 8 && resourceScript.Teams[TeamNumber] > 5)
                {
                    SpawnThings(1,0,0,2);
                    resourceScript.Teams[TeamNumber] -= 5;
                }
                if (resourceScript.Teams[TeamNumber] <= 5 && resourceScript.Teams[TeamNumber] > 0)
                {
                    SpawnThings(1,0,0,2);
                    resourceScript.Teams[TeamNumber] -= 3;
                }
                Rate_Timer = Rate_Spawn;
            }
        }
        if (First_Timer > 0) First_Timer -= Time.deltaTime;
        if (First_Timer <= 3 && First_Timer >= -10)
        {

            if (First_Timer <= 3 && First_Timer >= 2.9)
            {
                First_Timer -= .11f;
                var locationScript = MapController.GetComponent<MapControlScript>();
                var HomeBaseStart = transform.position;
                HomeBaseStart.x += 1;
                HomeBaseStart.z += 1;

                BOUY = Instantiate(Bouy, HomeBaseStart, Quaternion.identity) as GameObject;
                locationScript.vertex[(int)HomeBaseStart.x, (int)HomeBaseStart.z] = TeamName;
                BOUY.GetComponent<PylonScript>().TeamNumber = TeamNumber;

                HomeBaseStart.z -= 2;
                BOUY1 = Instantiate(Bouy, HomeBaseStart, Quaternion.identity) as GameObject;
                locationScript.vertex[(int)HomeBaseStart.x, (int)HomeBaseStart.z] = TeamName;
                BOUY1.GetComponent<PylonScript>().TeamNumber = TeamNumber;


                HomeBaseStart.x -= 2;
                BOUY2 = Instantiate(Bouy, HomeBaseStart, Quaternion.identity) as GameObject;
                locationScript.vertex[(int)HomeBaseStart.x, (int)HomeBaseStart.z] = TeamName;
                BOUY2.GetComponent<PylonScript>().TeamNumber = TeamNumber;


                HomeBaseStart.z += 2;
                BOUY3 = Instantiate(BOUY, HomeBaseStart, Quaternion.identity) as GameObject;
                locationScript.vertex[(int)HomeBaseStart.x, (int)HomeBaseStart.z] = TeamName;
                BOUY3.GetComponent<PylonScript>().TeamNumber = TeamNumber;
            }
            if  (First_Timer <= 0 && First_Timer >= -10f)    
            {
                First_Timer -= 11f;
                var BOUYScript = BOUY.GetComponent<PylonScript>();
                var BOUY1Script = BOUY1.GetComponent<PylonScript>();
                var BOUY2Script = BOUY2.GetComponent<PylonScript>();
                var BOUY3Script = BOUY3.GetComponent<PylonScript>();

                 BOUYScript.active = true;
                BOUY1Script.active = true;
                BOUY2Script.active = true;
                BOUY3Script.active = true;

                 BOUYScript.BasePylon = true;
                BOUY1Script.BasePylon = true;
                BOUY2Script.BasePylon = true;
                BOUY3Script.BasePylon = true;

                BOUYScript.TeamHomeBase = this.gameObject;
                BOUY1Script.TeamHomeBase = this.gameObject;
                BOUY2Script.TeamHomeBase = this.gameObject;
                BOUY3Script.TeamHomeBase = this.gameObject;
                
                BOUYScript.Activate();
                BOUY1Script.Activate();
                BOUY2Script.Activate();
                BOUY3Script.Activate();
            }
        }
	}

    public void SpawnThings(int blimps, int skiffs, int tugs, int bouys)
    {
        for (int i = blimps; i > 0; i--)
        {
            var BLIMP = Instantiate(Blimp, Homebase, Quaternion.identity) as GameObject;
            var BlimpScript = BLIMP.GetComponent<SphereScript>();
            BlimpScript.TeamNumber = TeamNumber;
            BLIMP.name = "Blimp";
        }

        for (int j = skiffs; j > 0; j--)
        {
            var SKIFF = Instantiate(Skiff, Homebase, Quaternion.identity) as GameObject;
            var SkiffScript = SKIFF.GetComponent<CubeScript>();
            SkiffScript.TeamNumber = TeamNumber;
            SKIFF.name = "Skiff";
        }

        for (int k = tugs; k > 0; k--)
        {
            var TUG = Instantiate(Tug, Homebase, Quaternion.identity) as GameObject;
            var TugScript = TUG.GetComponent<CylinderScript>();
            TugScript.TeamNumber = TeamNumber;
            TUG.name = "Tug";
        }

        var pos = new Vector3(0,0,0);

        for (int l = bouys; l > 0; l-- )
        {
            myBouys = GameObject.FindGameObjectsWithTag("Pylon");
            int index = 0;
            open = new List<Vector3>();
            var locationScript = MapController.GetComponent<MapControlScript>();
            Debug.Log("THE MAP SIZE IS :  " + (locationScript.MapSize * 2));
            foreach (var bou in myBouys)
            {
                var bouyScript = bou.GetComponent<PylonScript>();
                if (bouyScript.active && bouyScript.TeamNumber == TeamNumber)
                {
                    pos = bou.transform.position;
                    pos.x += 2;
                    if (pos.x > locationScript.MapSize * 2) pos.x = (locationScript.MapSize * 2);
                    if (locationScript.vertex[(int)pos.x, (int)pos.z] == "null" || locationScript.vertex[(int)pos.x, (int)pos.z] == null)
                    {
                        if (pos.x <= (locationScript.MapSize * 2) && pos.z <= (locationScript.MapSize * 2) && pos.x >= 0 && pos.z >= 0)
                        {
                            open.Add(pos);
                        }
                    }
                    pos.x -= 4;
                    if (pos.x < 0) pos.x = 0;
                    if (locationScript.vertex[(int)pos.x, (int)pos.z] == "null" || locationScript.vertex[(int)pos.x, (int)pos.z] == null && pos.x >= 0)
                    {
                        if (pos.x <= (locationScript.MapSize * 2) && pos.z <= (locationScript.MapSize * 2) && pos.x >= 0 && pos.z >= 0)
                        {
                            open.Add(pos);
                        }
                    }
                    pos.z += 2;
                    pos.x += 2;
                    if (pos.z > locationScript.MapSize * 2) pos.z = (locationScript.MapSize * 2);
                    if (locationScript.vertex[(int)pos.x, (int)pos.z] == "null" || locationScript.vertex[(int)pos.x, (int)pos.z] == null)
                    {
                        if (pos.x <= (locationScript.MapSize * 2) && pos.z <= (locationScript.MapSize * 2) && pos.x >= 0 && pos.z >= 0)
                        {
                            open.Add(pos);
                        }
                    }
                    pos.z -= 4;
                    if (pos.z < 0) pos.z = 0;
                    if (locationScript.vertex[(int)pos.x, (int)pos.z] == "null" || locationScript.vertex[(int)pos.x, (int)pos.z] == null && pos.z >= 0)
                    {
                        // if (!open.Contains(pos))
                        if (pos.x <= (locationScript.MapSize * 2) && pos.z <= (locationScript.MapSize * 2) && pos.x >= 0 && pos.z >= 0)
                        {
                            open.Add(pos);
                        }
                    }
                }
                
            }
            if (open.Count > 0)
            {
                index = Random.Range(0, open.Count);
                if (locationScript.vertex[(int)open[index].x, (int)open[index].z] != "beacon")
                {
                    var BOUY = Instantiate(Bouy, open[index], Quaternion.identity) as GameObject;
                    var BOUYScript = BOUY.GetComponent<PylonScript>();
                    BOUYScript.TeamNumber = TeamNumber;
                    BOUYScript.TeamHomeBase = this.gameObject;
                    Debug.Log("WHY this pylon not TEAM 2!?");
                    locationScript.vertex[(int)open[index].x, (int)open[index].z] = "beacon";
                }
            }
        }
        
        
            
            /*      int index = 2;
                  myBouys = GameObject.FindGameObjectsWithTag("RedPylon");
                  bool stepOne = false;
                  bool stepTwo = false;
                  while (stepOne == false)
                  {
                      Debug.Log("try to Step ONE...");     //DEBUG LINE
                      index = Random.Range(0, myBouys.Length);
                      var bouyScript = myBouys[index].GetComponent<PylonScript>();
                      if (bouyScript.active)
                      {
                          stepOne = true;
                          Debug.Log("Found an Active at : " + myBouys[index].transform.position);     //DEBUG LINE
                      }
                  }
                  while (stepTwo == false)
                  {
                      Debug.Log("try to Step TWO... Index = " + index);     //DEBUG LINE
                      var pos = myBouys[index].transform.position;
                      var locationScript = MapController.GetComponent<MapControlScript>();
                      var dir = Random.Range(0, 3);
                      if (dir == 0)
                      {
                          pos.x += 2;
                      }
                      if (dir == 1)
                      {
                          pos.x -= 2;
                      }
                      if (dir == 2)
                      {
                          pos.z += 2;
                      }
                      if (dir == 3)
                      {
                          pos.z -= 2;
                      }
                      Debug.Log("try to Step TWO. Found pos now is : " + pos + " . from dir: " + dir);     //DEBUG LINE
                      if (locationScript.vertex[(int)pos.x, (int)pos.z] == "null" || locationScript.vertex[(int)pos.x, (int)pos.z] == null)
                      {
                          var BOUY = Instantiate(Bouy, pos, Quaternion.identity) as GameObject;
                          var BOUYScript = BOUY.GetComponent<PylonScript>();
                          BOUYScript.TeamNumber = TeamNumber;
                          BOUY.name = "Pylon";
                          stepTwo = true;
                      }
                      if (locationScript.vertex[(int)pos.x, (int)pos.z] != "null")
                      {
                          Debug.Log("New Position was occupied... " + pos + " .. was .. " + locationScript.vertex[(int)pos.x, (int)pos.z]);
                      }
                  }
                  stepTwo = false;
                  stepOne = false;
           */
        
        
        
        
            
    }

    public void TeamLoss()
    {
        GameObject[] myBouys = GameObject.FindGameObjectsWithTag("Pylon");
        PlayerPrefs.SetInt("HouseUnlock" + HouseNumber, 1);
        DisplayNotice("Team " + TeamName + " has been eliminated", 8f);
        if (TeamNumber == 0) MapController.GetComponent<MapControlScript>().Team0Status = false;
        if (TeamNumber == 1) MapController.GetComponent<MapControlScript>().Team1Status = false;
        if (TeamNumber == 2) MapController.GetComponent<MapControlScript>().Team2Status = false;
        if (TeamNumber == 3) MapController.GetComponent<MapControlScript>().Team3Status = false;

        foreach (var i in myBouys)
        {
            if (i.GetComponent<PylonScript>().TeamNumber == TeamNumber)
            {
                var scri = i.GetComponent<PylonScript>();
                if (scri.BasePylon)
                {
                    scri.BasePylon = false;
                }
                scri.TakeDamage(100, -1);
            }

        }
        var check = MapController.GetComponent<MapControlScript>();
        check.checkStatus();

        Destroy(this.gameObject);
    }

    public void DisplayNotice(string Text, float Time)
    {
        var Notice = Instantiate(displayTextObject, transform.position, Quaternion.identity) as GameObject;
        var notice = Notice.gameObject.GetComponent<TextDisplayScript>();
        notice.FadeTimer = Time;
        notice.DisplayText = Text;
    }
}
