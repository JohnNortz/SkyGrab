using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

    public string ButtonName;
    public Vector3 Homebase;
    public int ButtonType;  // 0 == Blimp || 1 == Skiff || 2 == Tug || 3 == Bouy || 
    public int ButtonNumber; // null is normal or defualts,  1 = house choice 1 ,  2 = house choice 2
    public GameObject prefabDefaultBlimp;
    public GameObject prefabDefaultSkiff;
    public GameObject prefabDefaultTug;     
    public GameObject prefabCinder;
    public GameObject prefabPorcelain;
    public GameObject prefabJoves;
    public GameObject prefabSilent;
    public GameObject prefabSols;
    public GameObject prefabHeavens;
    public GameObject prefabIron;
    public GameObject prefabBlight;
    public GameObject prefabToCreate;
    public bool PorcelainDrop = false;
    public string spawnWithName;
    public int ResourceCostPerUnit;
    public GameObject MapController;
    public GameObject ResourceCube;
    public int TeamNumber;                //0 = Player ;; 1 =+ are AI;;
    public string TeamName;
    public int HouseNumber;

    public GameObject BlimpCinder;
    public GameObject BlimpPorcelain;
    public GameObject BlimpSols;
    public GameObject BlimpHeavens;
    public GameObject BlimpIron;
    public GameObject BlimpBlight; 
    public GameObject TugCinder;
    public GameObject TugPorcelain;
    public GameObject TugJoves;
    public GameObject TugSilent;
    public GameObject TugSols;
    public GameObject TugHeavens;
    public GameObject SkiffPorcelain;
    public GameObject SkiffJoves;
    public GameObject SkiffSilent;
    public GameObject SkiffIron;
    public GameObject SkiffBlight;
    
    void Start()
    {
        var load = PlayerPrefs.GetInt("House1");
        switch (load)
        {
            case 0:
                prefabDefaultBlimp = BlimpCinder;
                prefabDefaultTug = TugCinder; 
                break;
            case 1:
                prefabDefaultBlimp = BlimpPorcelain;
                prefabDefaultSkiff = SkiffPorcelain;
                prefabDefaultTug = TugPorcelain;  
                break;
            case 2:
                prefabDefaultSkiff = SkiffJoves;
                prefabDefaultTug = TugJoves; 
                break;
            case 3:
                prefabDefaultSkiff = SkiffSilent;
                prefabDefaultTug = TugSilent;
                break;
            case 4:
                prefabDefaultBlimp = BlimpSols;
                prefabDefaultTug = TugSols;
                break;
            case 5:
                prefabDefaultBlimp = BlimpHeavens;
                prefabDefaultTug = TugHeavens; 
                break;
            case 6:
                prefabDefaultBlimp = BlimpIron;
                prefabDefaultSkiff = SkiffIron; 
                break;
            case 7:
                prefabDefaultBlimp = BlimpBlight;
                prefabDefaultSkiff = SkiffBlight;
                break;
        }

    }
    
    public void TogglePorcelain()
    {
        PorcelainDrop = !PorcelainDrop;
    }

    public void PressAButton(int type)    // 0:blimp  1:skiff  2:Tug  3:House1  4:House2
    {
        Debug.Log("Button was Pressed, button tpye: " + type );
        var locationScript = MapController.GetComponent<MapControlScript>();
        var resourceScript = ResourceCube.GetComponent<ResourceManager>();

        if (type == 0) HouseNumber = PlayerPrefs.GetInt("House1");
        if (type == 1) HouseNumber = PlayerPrefs.GetInt("House1");
        if (type == 2) HouseNumber = PlayerPrefs.GetInt("House1");
        if (type == 3) HouseNumber = PlayerPrefs.GetInt("House1");
        if (type == 4) HouseNumber = PlayerPrefs.GetInt("House2");

        Homebase = locationScript.Team0SpawnPoint;
        TeamName = "Green";
        
/*      if (locationScript.playersShips[TeamNumber, ButtonType] == -1){ prefabToCreate = prefabDefault; }       //this is all outdated, removed on 10/31, remove if long dead
        if (locationScript.playersShips[TeamNumber, ButtonType] == 0) { prefabToCreate = prefabCinder;  }
        if (locationScript.playersShips[TeamNumber, ButtonType] == 1) { prefabToCreate = prefabPorcelain; }
        if (locationScript.playersShips[TeamNumber, ButtonType] == 2) { prefabToCreate = prefabJoves;   }
        if (locationScript.playersShips[TeamNumber, ButtonType] == 3) { prefabToCreate = prefabSilent;  }
        if (locationScript.playersShips[TeamNumber, ButtonType] == 4) { prefabToCreate = prefabSols;    }
        if (locationScript.playersShips[TeamNumber, ButtonType] == 5) { prefabToCreate = prefabHeavens; }
        if (locationScript.playersShips[TeamNumber, ButtonType] == 6) { prefabToCreate = prefabIron;    }
        if (locationScript.playersShips[TeamNumber, ButtonType] == 7) { prefabToCreate = prefabBlight;  }    */


        if (HouseNumber == 0 && type == 1)
        {
            ResourceCostPerUnit = 7;
            prefabToCreate = prefabCinder;
            ButtonType = 1; 
        }
        else if (HouseNumber == 1 && type == 3)
        {
            ResourceCostPerUnit = 2;
            PorcelainDrop = !PorcelainDrop;
            ButtonType = -2;
        }

        else if (HouseNumber == 2 && type == 0)
        {
            ResourceCostPerUnit = 1;
            prefabToCreate = prefabJoves;
            ButtonType = 0;
        }
        else if (HouseNumber == 3 && type == 0)
        {
            ResourceCostPerUnit = 4;
            prefabToCreate = prefabSilent;
            ButtonType = 0; 
        }
        else if (HouseNumber == 4 && type == 1)
        {
            ResourceCostPerUnit = 9;
            prefabToCreate = prefabSols;
            ButtonType = 1; 
        }
        else if (HouseNumber == 5 && type == 1)
        {
            ResourceCostPerUnit = 9;
            prefabToCreate = prefabHeavens;
            ButtonType = 1;
        }
        else if (HouseNumber == 6 && type == 2)
        {
            ResourceCostPerUnit = 13;
            prefabToCreate = prefabIron;
            ButtonType = 2;
        }
        else if (HouseNumber == 7 && type == 2)
        {
            ResourceCostPerUnit = 13;
            prefabToCreate = prefabBlight;
            ButtonType = 2;
        }
        else
        {
            if (type == 0)
            {
                ButtonType = 0;
                ResourceCostPerUnit = 3;
                prefabToCreate = prefabDefaultBlimp;
            }
            if (type == 1)
            {
                ButtonType = 1;
                ResourceCostPerUnit = 6;
                prefabToCreate = prefabDefaultSkiff;
            }
            if (type == 2)
            {
                ButtonType = 2;
                ResourceCostPerUnit = 10;
                prefabToCreate = prefabDefaultTug;
            }
        }


        if (HouseNumber == null && ButtonNumber != 0)
        {
            Debug.Log("This Shouldnt Happen, fix it");
            prefabToCreate = null;
        }


        if (resourceScript.Teams[TeamNumber] < ResourceCostPerUnit)
        {
            resourceScript.NotEnoughResources(1.5f);
        }

        if (resourceScript.Teams[TeamNumber] >= ResourceCostPerUnit && prefabToCreate != null && ButtonType != -2)
        {
            locationScript.TakeScore("boatBuilt", 0);
            GameObject clone = Instantiate(prefabToCreate, Homebase, Quaternion.identity) as GameObject;
            clone.name = spawnWithName;
            resourceScript.Teams[TeamNumber] -= ResourceCostPerUnit;


            if (ButtonType == 0)
            {
                clone.GetComponent<SphereScript>().TeamNumber = TeamNumber;
            }
            if (ButtonType == 1)
            {
                clone.GetComponent<CubeScript>().TeamNumber = TeamNumber;
            }
            if (ButtonType == 2)
            {
                clone.GetComponent<CylinderScript>().TeamNumber = TeamNumber;
            }

        }

    }

    void OnGUI()
    {

    }
}
