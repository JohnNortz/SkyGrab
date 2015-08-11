using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapControlScript : MonoBehaviour
{

    public int MapSize;
    public int Scale;
    public GameObject fillTile;
    public GameObject ResourceManager;
    public Vector3 Team0SpawnPoint;
    public Vector3 Team1SpawnPoint;
    public Vector3 Team2SpawnPoint;
    public Vector3 Team3SpawnPoint;
    public Vector3 Team4SpawnPoint;
    public Vector3 Team5SpawnPoint;
    public string[,] vertex = new string[100, 100];
    private string[,] squares = new string[100, 100];
    private GameObject[] Filled;
    public GameObject Manager;
    public GameObject Bouy;
    public GameObject BouyNotAllowed;
    public GameObject BouyNotAllowedNoProx;
    public GameObject BouyToSpawn;
    public GameObject BouyPorcelain;
    public GameObject Ghost;
    public GameObject PlayerRig;
    public GameObject EnemyRig;
    public RaycastHit hit;
    private Vector3 S;
    private float planeScale;
    public Collider[] Adjacent;

    public bool TwoPlayerGame;

    //[TeamNumber,TimerInterval,1-3 where 1=kills, 2=income, 3=bank]
    public int TimeScoreInterval;
    private float scoreTimer;
    public int scoreNumber;
    public GameObject ScoreBoard;
    public bool Team0Status = true;
    public bool Team1Status = true;
    public bool Team2Status = true;
    public bool Team3Status = true;

    public bool MainMenu;

    public int[,] playersShips = new int[6,4];  //2d array, first number is play number (0 = player,, ai++) Second Number is which ship (0 = default, see table bellow)               //
                        // Number ||  0           ||   1           ||   2           ||   3           ||   4           ||   5           ||   6           ||   7           ||   -1      // 
                        // House  || CinderForge  || Ho. Porcelain ||Jove's Deciples||Silent Strides ||Bone Maul Clan ||Heaven's Reach ||The IronMaw    ||House of Blight||   DEFAULT


    //   _____________________
    //   ::  FOR DEBUG ONLY ::         make buttons for this instead  VVV
    //   ---------------------

    private int Team0House1;
    private int Team0House2;
    private int Team1House1;
    private int Team1House2;

    //............................................................... ^^^


    // Use this for initialization
    void Start ()
    {
        if (PlayerPrefs.GetInt("TwoPlayerGame") == 1) TwoPlayerGame = true;
        else TwoPlayerGame = false;
        int teamsNumber = 4;
        if (TwoPlayerGame)
        {
            teamsNumber = 2;
            Team2Status = false;
            Team3Status = false;
        }

        TakeScore("first", 0);

        Team0Status = true;
        Team1Status = true;
        Team2Status = true;
        Team3Status = true;
        
        MapSize = PlayerPrefs.GetInt("mapSize");

        Team0House1 = PlayerPrefs.GetInt("House" + 1);
        Team0House2 = PlayerPrefs.GetInt("House" + 2);
        
        playersShips[0, 0] = -1;
        playersShips[0, 1] = -1;
        playersShips[0, 2] = -1;
        playersShips[0, 3] = -1;
        playersShips[1, 0] = -1;
        playersShips[1, 1] = -1;
        playersShips[1, 2] = -1;
        playersShips[1, 3] = -1; 
        
        if (Team0House1 == 2 || Team0House1 == 3)                                                 {playersShips[0, 0] = Team0House1;}
        if (Team0House1 == 0 || Team0House1 == 4 || Team0House1 == 5)                             {playersShips[0, 1] = Team0House1;}
        if (Team0House1 == 6 || Team0House1 == 7)                                                 {playersShips[0, 2] = Team0House1;}
        if (Team0House1 == 1)                                                                     {playersShips[0, 3] = Team0House1;}
        if (Team0House2 == 2 || Team0House2 == 3)                                                 {playersShips[0, 0] = Team0House2;}
        if (Team0House2 == 0 || Team0House2 == 4 || Team0House2 == 5)                             {playersShips[0, 1] = Team0House2;}
        if (Team0House2 == 6 || Team0House2 == 7)                                                 {playersShips[0, 2] = Team0House2;}
        if (Team0House2 == 1)                                                                     {playersShips[0, 3] = Team0House2;}
                      

        for (int i = 0; i <= (MapSize * Scale); i += Scale)                               // this creates a 2d array with dimensions of the map + 1 for the edge vertex
        {                                                                                 // i = x position ;; j = z position
            for (int j = 0; j <= (MapSize * Scale); j += Scale)                           // if the scale is 2, for example, then the array will look like [2,0],[2,2],[2,4] etc.
            {
                vertex[(i), (j)] = "null";                                                // its and array of strings (though that could change to int easily) where it can be null, beacon, red, or green
                //Debug.Log(i + "i " + j + " j");                                           // beacon means its occupied by a de-activated bouy, color means an active bouy of that color.
            }
        }



        for (int i = (Scale / 2); i <= (MapSize * Scale) - (Scale / 2); i += Scale)       // this creates a 2d array with dimensions of the map
        {                                                                                 // i = x position ;; j = z position
            for (int j = (Scale / 2); j <= (MapSize * Scale) - (Scale / 2); j += Scale)   // if the scale is 2, for example, then the array will look like [3,1],[3,3],[3,5] etc.
            {
                squares[(i), (j)] = "null";                                               // its and array of strings (though that could change to int easily) where it can be null, red, or green
                //Debug.Log("square" + i + " i " + j + " j:" + squares[(i), (j)]);                                                // color means an active bouy of that color, null is unclaimed.
            }
        }

        this.transform.position = new Vector3(MapSize, 0, MapSize);                                                   // all this junk is what spawns when game starts
                                              
        planeScale = (float)MapSize / 5;                                                                              //tiles the planes grid texture properly
        transform.localScale = new Vector3 (planeScale, planeScale, planeScale);
        gameObject.renderer.material.mainTextureScale = new Vector2(MapSize, MapSize);


        var pieceRotation = Quaternion.AngleAxis(270, Vector3.right); 

        Vector3 playerStart = new Vector3(3f, .8f, 3f);
        if (!MainMenu)
        {
            GameObject PlayerStart = Instantiate(PlayerRig, playerStart, pieceRotation) as GameObject;
            PlayerStart.name = "HomeBase";
        }
        

        Team0SpawnPoint = playerStart;
        Vector3 enemyStart = new Vector3(0, 0, 0);
        if (!TwoPlayerGame)
        {
            enemyStart = new Vector3(((MapSize * Scale) - 3), .6f, (3f));                  // all this junk
            GameObject ENEMYStart2 = Instantiate(EnemyRig, enemyStart, pieceRotation) as GameObject;                       
            ENEMYStart2.name = "OpponentBase2";
            ENEMYStart2.GetComponent<AIController>().TeamNumber = 2;
            Team2SpawnPoint = enemyStart;

            enemyStart = new Vector3((3f), .6f, ((MapSize * Scale) - 3f));                  // all this junk
            GameObject ENEMYStart3 = Instantiate(EnemyRig, enemyStart, pieceRotation) as GameObject;
            ENEMYStart3.name = "OpponentBase3";
            ENEMYStart3.GetComponent<AIController>().TeamNumber = 3;
            Team3SpawnPoint = enemyStart;
        }


        if (!MainMenu)
        {
            enemyStart = new Vector3(((MapSize * Scale) - 3), .6f, ((MapSize * Scale) - 3f));
            GameObject ENEMYStart = Instantiate(EnemyRig, enemyStart, pieceRotation) as GameObject;
            ENEMYStart.name = "OpponentBase";

            Team1SpawnPoint = enemyStart;
        }

 
    }

    // Update is called once per frame
    void Update ()
    {
        scoreTimer += Time.deltaTime;
        if (scoreTimer >= TimeScoreInterval)
        {
            scoreTimer = 0;
        }
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);                         // mouse to world pointing
        var MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Vector3 P = hit.point;
        MousePos.x = (((Mathf.Round(P.x / Scale)) * Scale));
        MousePos.z = (((Mathf.Round(P.z / Scale)) * Scale));
        MousePos.y = .5f;
        var posMaxi = MapSize * Scale;
        if (MousePos.x <= 0) MousePos.x = 0;
        if (MousePos.z <= 0) MousePos.z = 0;
        if (MousePos.x >= posMaxi) MousePos.x = posMaxi;
        if (MousePos.z >= posMaxi) MousePos.z = posMaxi;

        if (Input.GetMouseButtonDown(0))
        {
            
            var resourceScript = ResourceManager.GetComponent<ResourceManager>(); 
            if (Physics.Raycast(ray, out hit, 1000f))                                     
            {
              
                P = hit.point;
                MousePos.x = (((Mathf.Round(P.x / Scale)) * Scale));
                MousePos.z = (((Mathf.Round(P.z / Scale)) * Scale));
                if (MousePos.x <= 0) MousePos.x = 0;
                if (MousePos.z <= 0) MousePos.z = 0;
                MousePos.y = .5f;
                var hity = hit.point;
                hity.y += 3f;
                int VX = (int) MousePos.x;
                int VZ = (int) MousePos.z;



                bool ActiveNear = false;

                Collider[] Adjacent = Physics.OverlapSphere(MousePos, (.1f));   //gets near pylons, makes sure there is an adjacent active pylon
                foreach (var b in Adjacent)
                {
                    if (b.gameObject.tag == "Pylon" && b.gameObject.GetComponent<PylonScript>().TeamNumber == 0)
                    {
                        var scrip = b.gameObject.GetComponent<PylonScript>();
                        if (scrip.active == true) 
                        { 
                            ActiveNear = true;
                        }
                    }
                    
                }


                var ChkHoPScipt = Manager.GetComponent<ButtonScript>();
                int pylonCost = 0;
                if (ChkHoPScipt != null && ChkHoPScipt.PorcelainDrop)
                {
                    if(resourceScript.Teams[0] < 2)
                    {
                        pylonCost = 0;
                        BouyToSpawn = null;                        // If not enough for PorcePylon NEEDS Player Ping/visualization
                    
                    }
                    if (resourceScript.Teams[0] >= 2)
                    {
                        pylonCost = 2;
                        BouyToSpawn = BouyPorcelain;
                    }
                    
                }
                else
                {
                    pylonCost = 1;
                    BouyToSpawn = Bouy;
                }

                if(vertex[(VX), (VZ)] != null)
                {

                    if (vertex[(VX), (VZ)] != "null")                                                           //Here is a list of things that will stop beacons from being placed
                    {
                        Instantiate(BouyNotAllowed, MousePos, Quaternion.identity);                      //this is what happens when something is already in the clicked position
                    }
                    else if (VX < -1 || VX > ((MapSize * Scale) + 1) || VZ > ((MapSize * Scale) + 1) || VZ < -1)                                                           //Here is a list of things that will stop beacons from being placed
                    {
                        Instantiate(BouyNotAllowed, MousePos, Quaternion.identity);                      //this is what happens when something is already in the clicked position
                    }                                                                       
                    else if (!ActiveNear)
                    {
                        Instantiate(BouyNotAllowedNoProx, MousePos, Quaternion.identity);                 //if there isnt an adjacent active pylon
                        resourceScript.NotAdj(1.5f);
                    }
                    else if (BouyToSpawn == null)                                                              
                    {
                        Instantiate(BouyNotAllowed, MousePos, Quaternion.identity);                      //error catching
                    }
                    else if (resourceScript.Teams[0] < pylonCost)                                          //not enough resources
                    {
                        resourceScript.NotEnoughResources(1.5f);
                    }
                    else if (hit.collider.tag == "HouseButton")
                    {

                    }
                    else                                                                                   //adds a beacon if the space is empty then changes that location in the array to "Beacon"
                    {
                        Instantiate(BouyToSpawn, MousePos, Quaternion.identity);
                        var BouyScript = Bouy.gameObject.GetComponent<PylonScript>();
                        BouyScript.scale = Scale;
                        vertex[(VX), (VZ)] = "Beacon";
                        resourceScript.Teams[0] -= pylonCost;
                    }

                }


                Debug.DrawLine(ray.origin, hit.point);
                Debug.DrawLine(hit.point, hity);

            }


        }

        if (Physics.Raycast(ray, out hit, 1000f))
        {
            Ghost.transform.position = new Vector3(MousePos.x, .5f, MousePos.z);            // draws the ghost at the location closest to that of mouse 
            Debug.DrawLine(ray.origin, hit.point);
        }
    }
    /*
    public List<List<int[]>> CheckPylons(int X, int Z, int TeamNumber, string[,] visited, List<int[]> ancestors)   //idk what this is, or what to do at this point ::  MapSize
    {
        List<List<int[]>> Ret = new List<List<int[]>>();
        
        string id = "null";
        if (TeamNumber == 0) id = "Green";
        if (TeamNumber == 1) id = "Red";

        int[] temp = new int[2];
        temp[0] = X;
        temp[1] = Z;

        if (!ancestors.Contains(temp))
        {
            ancestors.Add(temp);
            visited[X,Z] = id;
        }
        else
        {
            Ret.Add(ancestors);
            return Ret;
        }





        if (X <= MapSize && Z <= MapSize)
        {
            List<List<int[]>> inStream = CheckPylons(X + 1, Z + 1, TeamNumber, visited, ancestors);
            foreach (List<int[]> i in inStream)
            {
                Ret.Add(i);
            }
            return Ret;

        }
        if (X <= MapSize && Z >= 0)
        {
            List<List<int[]>> inStream = CheckPylons(X + 1, Z - 1, TeamNumber, visited, ancestors);
            foreach (List<int[]> i in inStream)
            {
                Ret.Add(i);
            }
            return Ret;
        }
        if (X >= 0 && Z <= MapSize)
        {
            List<List<int[]>> inStream = CheckPylons(X - 1, Z + 1, TeamNumber, visited, ancestors);
            foreach (List<int[]> i in inStream)
            {
                Ret.Add(i);
            }
            return Ret;
        }
        if (X >= 0 && Z >= 0)
        {
            List<List<int[]>> inStream = CheckPylons(X - 1, Z - 1, TeamNumber, visited, ancestors);
            foreach (List<int[]> i in inStream)
            {
                Ret.Add(i);
            }
            return Ret;
        }

        return  new List<List<int[]>>();





    }

     */ 
     
    public void CheckSquare(int X, int Z)   //temp function. this just counts completely filled in areas Feed this Pylon Location
    {
        var x = X;
        var z = Z;

        for (int i = 0; i < 4; i++)
        {
            if (i == 0) { x = X + 1; z = Z + 1; }
            if (i == 1) { x = X + 1; z = Z - 1; }
            if (i == 2) { x = X - 1; z = Z + 1; }
            if (i == 3) { x = X - 1; z = Z - 1; }


            int p1x = x + 1;
            int p2x = x + 1;
            int p1z = z + 1;
            int p3z = z + 1;
            int p3x = x - 1;
            int p4x = x - 1;
            int p2z = z - 1;
            int p4z = z - 1;

            if (x >= 0 && z >= 0)
            {
                if (vertex[p1x, p1z] != "null" && vertex[p1x, p1z] != "beacon" && vertex[p1x, p1z] != null)
                {
                    var teamCheck = vertex[p1x, p1z];
                    if (p2x >= 0 && p2z >= 0)
                    {
                        if (vertex[p2x, p2z] == teamCheck && vertex[p3x, p3z] == teamCheck && vertex[p4x, p4z] == teamCheck)
                        {
                            squares[x, z] = teamCheck;
                            Filled = GameObject.FindGameObjectsWithTag("Fill");
                            var Duplicate = false;
                            foreach (var tile in Filled)
                            {
                                if (tile.transform.position.x == x && tile.transform.position.z == z)
                                {
                                    Duplicate = true;
                                }
                            }
                            if (!Duplicate)
                            {
                                var gogo = ResourceManager.GetComponent<ResourceManager>();
                                var Spawn = new Vector3(x, 0, z);
                                var Square = Instantiate(fillTile, Spawn, Quaternion.identity) as GameObject;
                                var Filler = Square.GetComponent<FillScript>();
                                if (teamCheck == "Green")
                                {
                                    Filler.TeamNumber = 0;
                                    //Debug.Log("CAPPED!");
                                }
                                if (teamCheck == "Red")
                                {
                                    Filler.TeamNumber = 1;
                                }
                                if (teamCheck == "Blue")
                                {
                                    Filler.TeamNumber = 2;
                                }
                                if (teamCheck == "Yellow")
                                {
                                    Filler.TeamNumber = 3;
                                }
                                gogo.Income(Filler.TeamNumber);
                            }

                        }
                    }
                }
                else
                {
                    squares[x, z] = "null";
                }
            }
        }

    }

    public void KillFill(int X, int Z) // pass this the dead pylon location;
    {
        if ((X + 1) <= MapSize && (Z + 1) <= MapSize) squares[X + 1, Z + 1] = "null";
        if ((X + 1) <= MapSize && (Z - 1) >= 0) squares[X + 1, Z - 1] = "null";
        if ((X - 1) >= 0 && (Z + 1) <= MapSize) squares[X - 1, Z + 1] = "null";
        if ((X - 1) >= 0 && (Z - 1) >= 0) squares[X - 1, Z - 1] = "null";

        Filled = GameObject.FindGameObjectsWithTag("Fill");
        foreach (var tile in Filled)
        {
            if (tile.transform.position.x == X + 1 && tile.transform.position.z == Z + 1)
            {
                Destroy(tile.gameObject);
            }
            if (tile.transform.position.x == X + 1 && tile.transform.position.z == Z - 1)
            {
                Destroy(tile.gameObject);
            }
            if (tile.transform.position.x == X - 1 && tile.transform.position.z == Z + 1)
            {
                Destroy(tile.gameObject);
            }
            if (tile.transform.position.x == X - 1 && tile.transform.position.z == Z - 1)
            {
                Destroy(tile.gameObject);
            }
        }
    }

    public void checkStatus()
    {

        var ResourceScript = ResourceManager.GetComponent<ResourceManager>();
        int teamsNumber = 4;
        if (TwoPlayerGame) teamsNumber = 2;
        if(!Team0Status)
        {

            TakeScore("LastGas", ResourceScript.Totals[0]);
            TakeScore("LastGrid", ResourceScript.MaxIncome);

            Application.LoadLevel(2);
        }
        if(Team1Status == false && Team2Status == false && Team3Status == false || Team1Status == false && TwoPlayerGame)
        {
            TakeScore("LastGas", ResourceScript.Totals[0]);
            TakeScore("LastGrid", ResourceScript.MaxIncome);

            Debug.Log("PLAYER WINS!!");
            Application.LoadLevel(3);
            
        }
    }

    public void TakeScore(string type, int ammount)
    {
        var ResourceScript = ResourceManager.GetComponent<ResourceManager>();
        var getScript = ScoreBoard.GetComponent<ScoreBoard>();

        switch (type)
        {
            case "first":
                getScript.boatsDestroid = 0;
                getScript.boatsBuilt = 0;
                getScript.buoysDestroid = 0;
                getScript.buoysCreated = 0;
                getScript.beaconPlaced = 0;
                getScript.mostGridsControlled = 1;
                break;

            case "lastGrid":
                getScript.mostGridsControlled = ammount;
                break;

            case "lastGas":
                getScript.totalGasCollected = ammount;
                break;

            case "boatDestroid":
                getScript.boatsDestroid++;
                break;

            case "boatBuilt":
                getScript.boatsBuilt++;
                break;

            case "buoyBuilt":
                getScript.buoysCreated++;
                break;

            case "buoyDestroid":
                getScript.buoysDestroid++;
                break;

            case "pylonPlaced":
                getScript.beaconPlaced++;
                break;

        }

    }


    
}
