using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RotatingUnitDisplayScript : MonoBehaviour {

    public float SpinSpeed;
    public GameObject DefaultMesh;
    public GameObject Cinder;
    public GameObject Porcelain;
    public GameObject Joves;
    public GameObject Silent;
    public GameObject Sols;
    public GameObject Heavens;
    public GameObject Iron;
    public GameObject Blight;
    public GameObject Displaying;
    public Texture Selected;
    public Texture NotSelected;
    public Texture CinderBanner;
    public Texture PorcelainBanner;
    public Texture JovesBanner;
    public Texture SilentBanner;
    public Texture SolsBanner;
    public Texture HeavensBanner;
    public Texture IronBanner;
    public Texture BlightBanner;
    public Texture UnAligned;
    public int choice;
    public GameObject DisplayPlane;
    public GameObject SpinTarget;
    private Vector3 spinPos;

    public int UpToDate;

    
    // Use this for initialization
	void Start () {
        spinPos = SpinTarget.transform.position;
        Displaying = Instantiate(DefaultMesh, spinPos, Quaternion.identity) as GameObject; 
	}
	
	// Update is called once per frame
	void Update () {

        if (PlayerPrefs.GetInt("House" + choice) == -1 && PlayerPrefs.GetInt("House" + choice) != UpToDate)
        {
            Destroy(Displaying.gameObject);
            Displaying = Instantiate(DefaultMesh, spinPos, Quaternion.identity) as GameObject;
            UpToDate = PlayerPrefs.GetInt("House" + choice);
        }

        if (PlayerPrefs.GetInt("House" + choice) == 0 && PlayerPrefs.GetInt("House" + choice) != UpToDate)
        {
            Destroy(Displaying.gameObject);
            var rot = new Quaternion(0, 90, 90, 0);
            Displaying = Instantiate(Cinder, spinPos, rot) as GameObject;
            UpToDate = PlayerPrefs.GetInt("House" + choice);
        }

        if (PlayerPrefs.GetInt("House" + choice) == 1 && PlayerPrefs.GetInt("House" + choice) != UpToDate)
        {
            Destroy(Displaying.gameObject);
            var rot = new Quaternion(0, 90, 90, 0);
            Displaying = Instantiate(Porcelain, spinPos, rot) as GameObject;
            UpToDate = PlayerPrefs.GetInt("House" + choice);
        }

        if (PlayerPrefs.GetInt("House" + choice) == 2 && PlayerPrefs.GetInt("House" + choice) != UpToDate)
        {
            Destroy(Displaying.gameObject);
            Displaying = Instantiate(Joves, spinPos, Quaternion.identity) as GameObject;
            UpToDate = PlayerPrefs.GetInt("House" + choice);
        }

        if (PlayerPrefs.GetInt("House" + choice) == 3 && PlayerPrefs.GetInt("House" + choice) != UpToDate)
        {
            Destroy(Displaying.gameObject);
            Displaying = Instantiate(Silent, spinPos, Quaternion.identity) as GameObject;
            UpToDate = PlayerPrefs.GetInt("House" + choice);
        }

        if (PlayerPrefs.GetInt("House" + choice) == 4 && PlayerPrefs.GetInt("House" + choice) != UpToDate)
        {
            Destroy(Displaying.gameObject);
            var rot = new Quaternion(0, 90, 90, 0);
            Displaying = Instantiate(Sols, spinPos, rot) as GameObject;
            UpToDate = PlayerPrefs.GetInt("House" + choice);
        }

        if (PlayerPrefs.GetInt("House" + choice) == 5 && PlayerPrefs.GetInt("House" + choice) != UpToDate)
        {
            Destroy(Displaying.gameObject);
            var rot = new Quaternion(0, 90, 90, 0);
            Displaying = Instantiate(Heavens, spinPos, rot) as GameObject;
            UpToDate = PlayerPrefs.GetInt("House" + choice);
        }

        if (PlayerPrefs.GetInt("House" + choice) == 6 && PlayerPrefs.GetInt("House" + choice) != UpToDate)
        {
            Destroy(Displaying.gameObject);
            Displaying = Instantiate(Iron, spinPos, Quaternion.identity) as GameObject;
            UpToDate = PlayerPrefs.GetInt("House" + choice);
        }

        if (PlayerPrefs.GetInt("House" + choice) == 7 && PlayerPrefs.GetInt("House" + choice) != UpToDate)
        {
            Destroy(Displaying.gameObject);
            Displaying = Instantiate(Blight, spinPos, Quaternion.identity) as GameObject;
            UpToDate = PlayerPrefs.GetInt("House" + choice);
        }

        if (Displaying != null && UpToDate != 0 && UpToDate != 1 && UpToDate != 4 && UpToDate != 5) Displaying.transform.Rotate(0, SpinSpeed * Time.deltaTime, 0);
        if (Displaying != null && UpToDate == 0 || UpToDate == 1 || UpToDate == 4 || UpToDate == 5) Displaying.transform.Rotate(0, 0, SpinSpeed * Time.deltaTime);


        if (UpToDate != -1) { DisplayPlane.renderer.material.SetTexture("_MainTex", Selected); }
        if (UpToDate == -1) { DisplayPlane.renderer.material.SetTexture("_MainTex", NotSelected); }

	}
}
