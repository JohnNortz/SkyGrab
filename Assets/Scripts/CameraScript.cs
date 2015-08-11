using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {


    private Vector3 dragOrigin;
    public float cameraMaxPos;
    private float cameraZOffset = -7;
    private Vector3 currentPosition = Vector3.zero;
    private Vector3 translation;
    private Vector3 TargetPos;
    private Vector3 StoredPosition;
    private Quaternion StoredAngle;

    public bool Chase = false;
    public float panSpeed;
    public float zoomSpeed;
    public float ScrollArea;
    public Vector3 ChaseOffset;
    public float orbitDistance = 10.0F;
    public int orbitCameraSpeed = 10;
    public bool Transitional;
    public GameObject MapController;

    float xSpeed = 175.0F;
    float ySpeed = 75.0F;
    int yMinLimit = 30; //Lowest vertical angle in respect with the target.
    int yMaxLimit = 80;
    int minDistance = 2; //Min distance of the camera from the target
    int maxDistance = 12;
    private float x = 0.0F;
    private float y = 0.0F;

    public string TeamTag;
    public GameObject[] Units;
    public GameObject[] MapControllers;
    public GameObject Target;
    public float TransitionDamp;

    
    // Use this for initialization
	void Start () {

        GameObject[] MapControllers = GameObject.FindGameObjectsWithTag("GameController");

        foreach (var i in MapControllers)
        {
            MapController = i;
        }
        cameraMaxPos = PlayerPrefs.GetInt("mapSize");
        
        
        StoredAngle = transform.rotation;
        Transitional = false;
        Chase = false;

        var angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Transitional)
        {
            transform.position = Vector3.Slerp(transform.position, StoredPosition, 0.1f);
            transform.rotation = Quaternion.Slerp(transform.rotation, StoredAngle, Time.deltaTime * 15);

            var dist = Vector3.Distance(transform.position, StoredPosition); 
            if (dist <= .01f)
            {
                Transitional = false;
                Chase = false;
                transform.position = StoredPosition;
                transform.rotation = StoredAngle;
            }
        }
        
        if (!Chase && !Transitional)
        {
            translation = Vector3.zero;

            var zoomDelta = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime;
            if (zoomDelta != 0)
            {
                translation -= Vector3.up * zoomSpeed * zoomDelta;
            }


            if (Input.mousePosition.x < ScrollArea && transform.position.x >= 0) translation += Vector3.right * -panSpeed * Time.deltaTime;
            if (Input.mousePosition.x >= Screen.width - ScrollArea && transform.position.x <= cameraMaxPos * 2) translation += Vector3.right * panSpeed * Time.deltaTime;
            if (Input.mousePosition.y < ScrollArea && transform.position.z >= -0 + cameraZOffset) translation += Vector3.forward * -panSpeed * Time.deltaTime;
            if (Input.mousePosition.y > Screen.height - ScrollArea && transform.position.z <= (cameraMaxPos * 2) + cameraZOffset) translation += Vector3.forward * panSpeed * Time.deltaTime;

            transform.position += translation;
        }
        if (Chase && !Transitional)
        {
            if (Target == null)
            {
                if (CheckForNearest() == false)
                {
                    ToggleChase();
                }
            }

           orbitDistance += Input.GetAxis("Mouse ScrollWheel") * orbitDistance;
           orbitDistance = Mathf.Clamp(orbitDistance, minDistance, maxDistance);
     
           //Detect mouse drag;
           if(Input.GetMouseButton(1))   
           {
                x += Input.GetAxis("Mouse X") * xSpeed * 0.02F;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02F;      
           }
       
           y = ClampAngle(y, yMinLimit, yMaxLimit);
                 
           Quaternion rotation = Quaternion.Euler(y, x, 0);
           var position2 = rotation * new Vector3(0f, 0f, -orbitDistance) + Target.transform.position;

           transform.position = Vector3.Lerp(transform.position, position2, orbitCameraSpeed * Time.deltaTime);
           transform.rotation = rotation;      
    
           //transform.position = Target.transform.position + ChaseOffset;

           //var lookPos = Target.transform.position - transform.position;
           //var rotation = Quaternion.LookRotation(lookPos);
           //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 15);
        }

    }

    public bool CheckForNearest()
    {
        Units = GameObject.FindGameObjectsWithTag(TeamTag);
        var closestUnit = 100F;
        
        foreach (var unit in Units)
        {
            var dist = Vector3.Distance(unit.transform.position, transform.position);           //checks if the units in the array are within its site range of 8;
            if (dist <= closestUnit)
            {
                Target = unit;
                closestUnit = dist;
            }
           
        }
        if (closestUnit == 100F)
        {
            Target = null;
            return false;
        }
        else return true;
    }

    public void ToggleChase()
    {
        if (!Chase)
        {
            StoredPosition = transform.position;
            var check = CheckForNearest();
            Chase = true;

            if (!check)
            {
                Chase = false;
            }
        }
        else if (Chase)
        {

            Target = null;
            Chase = false;
            Transitional = true;
        }

    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }
}
