using UnityEngine;
using System.Collections;

public class TrailColorSwapper : MonoBehaviour
{

    public GameObject trail1;
    public GameObject trail2;
    public GameObject trail3;
    public GameObject trail4;
    public GameObject Parent;
    public bool Blimp;
    public bool Tug;
    public bool Skiff;

    

    // Use this for initialization
    void Start()
    {

        if (Blimp)
        {
            var scri = Parent.gameObject.GetComponent<SphereScript>();
            var teamNumber = scri.TeamNumber;
            SwapColor(teamNumber);
        }
        else if (Tug)
        {
            var scri = Parent.gameObject.GetComponent<CylinderScript>();
            var teamNumber = scri.TeamNumber;
            SwapColor(teamNumber);
        }
        else if (Skiff)
        {
            var scri = Parent.gameObject.GetComponent<CubeScript>();
            var teamNumber = scri.TeamNumber;
            SwapColor(teamNumber);
        }

    }

    void SwapColor(int team)
    {
        if (team == 0)
        {
                trail1.gameObject.GetComponent<TrailRenderer>().renderer.material.SetColor("_Color", Color.green);
                trail2.gameObject.GetComponent<TrailRenderer>().renderer.material.SetColor("_Color", Color.green);
                if (trail3 != null) trail3.gameObject.GetComponent<TrailRenderer>().renderer.material.SetColor("_Color", Color.green);
                if (trail4 != null) trail4.gameObject.GetComponent<TrailRenderer>().renderer.material.SetColor("_Color", Color.green);

        }
        if (team == 1)
        {
                trail1.gameObject.GetComponent<TrailRenderer>().renderer.material.SetColor("_Color", Color.red);
                trail2.gameObject.GetComponent<TrailRenderer>().renderer.material.SetColor("_Color", Color.red);
                if (trail3 != null) trail3.gameObject.GetComponent<TrailRenderer>().renderer.material.SetColor("_Color", Color.red);
                if (trail4 != null) trail4.gameObject.GetComponent<TrailRenderer>().renderer.material.SetColor("_Color", Color.red);
        }
        if (team == 2)
        {
            trail1.gameObject.GetComponent<TrailRenderer>().renderer.material.SetColor("_Color", Color.blue);
                trail2.gameObject.GetComponent<TrailRenderer>().renderer.material.SetColor("_Color", Color.blue);
                if (trail3 != null) trail3.gameObject.GetComponent<TrailRenderer>().renderer.material.SetColor("_Color", Color.blue);
                if (trail4 != null) trail4.gameObject.GetComponent<TrailRenderer>().renderer.material.SetColor("_Color", Color.blue);
        }
        if (team == 3)
        {
                trail1.gameObject.GetComponent<TrailRenderer>().renderer.material.SetColor("_Color", Color.yellow);
                trail2.gameObject.GetComponent<TrailRenderer>().renderer.material.SetColor("_Color", Color.yellow);
                if (trail3 != null) trail3.gameObject.GetComponent<TrailRenderer>().renderer.material.SetColor("_Color", Color.yellow);
                if (trail4 != null) trail4.gameObject.GetComponent<TrailRenderer>().renderer.material.SetColor("_Color", Color.yellow);
        }

    }
}
	