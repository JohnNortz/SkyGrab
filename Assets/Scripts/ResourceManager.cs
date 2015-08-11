using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour {

    public int[] Incomes = new int[8];
    public int[] Teams = new int[8];
    public int[] Totals = new int[8];
    public int Team0Income = 0;
    public int Team1Income = 0;

    private float resourceGatherTimer;
    public float resourceGatherTime;
    public float SquareCheck;
    private float SquareCheckTime = 1;
    private GameObject[] fill;

    public GUIStyle myGUIStyle;

    public RectTransform IncomeTransform;
    public RectTransform IncomeTransform1;
    public RectTransform IncomeTransform2;
    public RectTransform IncomeTransform3;
    public float cachedY;
    public float cachedY1;
    public float cachedY2;
    public float cachedY3;
    public Text text0;
    public Text text1;
    public Text text2;
    public Text text3;
    public Text textBank;
    public Image NotEnoughImg;
    public Text NotEnoughTex;
    public float NotEnoughImgTimer;
    public Image NotAdjImg;
    public Text NotAdjTex;
    public float NotAdjImgTimer;

    public Text house1Price;    
    public Toggle house1Porce;
    public Text BlimpPrice;
    public Text SkiffPrice;
    public Text TugPrice;

    private float MinXValue;
    private float MaxXValue;
    public int HighestScore;
    public int MaxIncome;

    public bool team0Status = true;
    public bool team1Status = true;
    public bool team2Status = true;
    public bool team3Status = true;

    private float updateTimer;

    public bool MainMenu;

    void Start()
    {
        for (int i = 0; i <= 7; i++)
        {
            Incomes[i] = 0;
            Teams[i] = 5;
            //Debug.Log("Go Ahead on StartUP  : " + i);
        }
        if (!MainMenu)
        {
            if (PlayerPrefs.GetInt("House1") == 0) house1Price.text = "";
            if (PlayerPrefs.GetInt("House1") == 1) house1Price.text = "+1";
            if (PlayerPrefs.GetInt("House1") == 2) house1Price.text = "";
            if (PlayerPrefs.GetInt("House1") == 3) house1Price.text = "";
            if (PlayerPrefs.GetInt("House1") == 4) house1Price.text = "";
            if (PlayerPrefs.GetInt("House1") == 5) house1Price.text = "";
            if (PlayerPrefs.GetInt("House1") == 6) house1Price.text = "";
            if (PlayerPrefs.GetInt("House1") == 7) house1Price.text = "";

            if (PlayerPrefs.GetInt("House1") == 0) BlimpPrice.text = "3";
            if (PlayerPrefs.GetInt("House1") == 1) BlimpPrice.text = "3";
            if (PlayerPrefs.GetInt("House1") == 2) BlimpPrice.text = "1";
            if (PlayerPrefs.GetInt("House1") == 3) BlimpPrice.text = "4";
            if (PlayerPrefs.GetInt("House1") == 4) BlimpPrice.text = "3";
            if (PlayerPrefs.GetInt("House1") == 5) BlimpPrice.text = "3";
            if (PlayerPrefs.GetInt("House1") == 6) BlimpPrice.text = "3";
            if (PlayerPrefs.GetInt("House1") == 7) BlimpPrice.text = "3";

            if (PlayerPrefs.GetInt("House1") == 0) SkiffPrice.text = "7";
            if (PlayerPrefs.GetInt("House1") == 1) SkiffPrice.text = "6";
            if (PlayerPrefs.GetInt("House1") == 2) SkiffPrice.text = "6";
            if (PlayerPrefs.GetInt("House1") == 3) SkiffPrice.text = "6";
            if (PlayerPrefs.GetInt("House1") == 4) SkiffPrice.text = "9";
            if (PlayerPrefs.GetInt("House1") == 5) SkiffPrice.text = "10";
            if (PlayerPrefs.GetInt("House1") == 6) SkiffPrice.text = "6";
            if (PlayerPrefs.GetInt("House1") == 7) SkiffPrice.text = "6";

            if (PlayerPrefs.GetInt("House1") == 0) TugPrice.text = "10";
            if (PlayerPrefs.GetInt("House1") == 1) TugPrice.text = "10";
            if (PlayerPrefs.GetInt("House1") == 2) TugPrice.text = "10";
            if (PlayerPrefs.GetInt("House1") == 3) TugPrice.text = "10";
            if (PlayerPrefs.GetInt("House1") == 4) TugPrice.text = "10";
            if (PlayerPrefs.GetInt("House1") == 5) TugPrice.text = "10";
            if (PlayerPrefs.GetInt("House1") == 6) TugPrice.text = "13";
            if (PlayerPrefs.GetInt("House1") == 7) TugPrice.text = "13";

            if (PlayerPrefs.GetInt("House1") != 1) Destroy(house1Porce.gameObject);

            resourceGatherTimer = resourceGatherTime;
            Team0Income = 0;
            NotEnoughImg.enabled = false;
            NotEnoughTex.enabled = false;
            NotEnoughImgTimer = 0;

            NotAdjImgTimer = 0;
            NotAdjImg.enabled = false;
            NotAdjTex.enabled = false;
            MaxIncome = 1;

            cachedY = IncomeTransform.position.y;
            cachedY1 = IncomeTransform1.position.y;
            cachedY2 = IncomeTransform2.position.y;
            cachedY3 = IncomeTransform3.position.y;
            MaxXValue = IncomeTransform.position.x;
            MinXValue = IncomeTransform.position.x - IncomeTransform.rect.width;

            updateTimer = 1;
        }
    }

    void Update()
    {

        if (textBank != null) textBank.text = "Bank: " + Teams[0].ToString();
        resourceGatherTimer -= Time.deltaTime;
        SquareCheck -= Time.deltaTime;

        if (SquareCheck <= 0)
        {
            Incomes[0] = 0;
            Incomes[1] = 0;
            GameObject[] fill = GameObject.FindGameObjectsWithTag("Fill");

            foreach (var i in fill)
            {
                if (i.GetComponent<FillScript>().TeamNumber == 0)
                {
                    Incomes[0] += 1;
                    if (Incomes[0] > MaxIncome) MaxIncome = Incomes[0];
                }
                if (i.GetComponent<FillScript>().TeamNumber == 1) Incomes[1] += 1;
            }

            SquareCheck = SquareCheckTime;
        }

        if (resourceGatherTimer <= 0)
        {
            Gather();
        }

        if (updateTimer > 0 && !MainMenu) updateTimer -= Time.deltaTime;
        if (updateTimer <= 0)
        {
            updateTimer = 1;
            if (!MainMenu) UpdateDisplayValues();
        }
        if (NotEnoughImg != null)
        {
            if (NotEnoughImgTimer > 0)
            {
                NotEnoughImgTimer -= Time.deltaTime;
                NotEnoughImg.enabled = true;
                NotEnoughTex.enabled = true;
            }
            else
            {
                NotEnoughImg.enabled = false;
                NotEnoughTex.enabled = false;
            }
        }
        if (NotAdjImg != null)
        {
            if (NotAdjImgTimer > 0)
            {
                NotAdjImgTimer -= Time.deltaTime;
                NotAdjImg.enabled = true;
                NotAdjTex.enabled = true;
            }
            else
            {
                NotAdjImg.enabled = false;
                NotAdjTex.enabled = false;
            }
        }

    }
    public void Gather()
    {
        for (var i = 0; i <= 7; i++)
        {
            //Debug.Log(Incomes[i]);
            bool alive = true;
            if (i == 0 && !team0Status)
            {
                Teams[0] = 0;
                Incomes[0] = 0;
                Totals[0] = 0;
            }
            else if(i == 1 && !team1Status)
            {
                Teams[1] = 0;
                Incomes[1] = 0;
                Totals[1] = 0;
            }
            else if (i == 2 && !team2Status)
            {
                Teams[2] = 0;
                Incomes[2] = 0;
                Totals[2] = 0;
            }
            else if (i == 3 && !team3Status)
            {
                Teams[3] = 0;
                Incomes[3] = 0;
                Totals[3] = 0;
            }
            else
            {
                Teams[i] = (Teams[i]) + (Incomes[i]);
                Totals[i] += Incomes[i];
            }
        }
        resourceGatherTimer = resourceGatherTime;
    }

    public void Income(int Team)
    {
        Incomes[Team] += 1;
        if (Team == 0) Team0Income += 1;
        Totals[Team] += Incomes[Team];
        

    }

    void OnGUI()
    {
        //GUI.Box(new Rect(10, Screen.height - 200, 0.01f * Screen.width * resourceGatherTimer, 0.05f * Screen.height), "INCOME", myGUIStyle);
    }

    public void NotEnoughResources(float timetoadd)
    {
        NotEnoughImgTimer = timetoadd;
    }

    public void NotAdj(float timetoadd)
    {
        NotAdjImgTimer = timetoadd;
    }

    private void UpdateDisplayValues()
    {
        var topScore = 1;
        if (Incomes[0] >= topScore) topScore = Incomes[0];
        if (Incomes[1] > topScore) topScore = Incomes[1];
        if (Incomes[2] > topScore) topScore = Incomes[2];
        if (Incomes[3] > topScore) topScore = Incomes[3];
        HighestScore = topScore;

        var percentOfMax = (Incomes[0]) * (MaxXValue - MinXValue) / HighestScore + MinXValue;
        if (IncomeTransform != null && !MainMenu) IncomeTransform.position = new Vector3(percentOfMax, cachedY);

        percentOfMax = (Incomes[1]) * (MaxXValue - MinXValue) / HighestScore + MinXValue;
        if (IncomeTransform1 != null && !MainMenu) IncomeTransform1.position = new Vector3(percentOfMax, cachedY1);

        percentOfMax = (Incomes[2]) * (MaxXValue - MinXValue) / HighestScore + MinXValue;
        if (IncomeTransform2 != null && !MainMenu) IncomeTransform2.position = new Vector3(percentOfMax, cachedY2);

        percentOfMax = (Incomes[3]) * (MaxXValue - MinXValue) / HighestScore + MinXValue;
        if (IncomeTransform3 != null && !MainMenu) IncomeTransform3.position = new Vector3(percentOfMax, cachedY3);

        text0.text = "Income: " + Incomes[0].ToString();
        text1.text = Incomes[1].ToString();
        text2.text = Incomes[2].ToString();
        text3.text = Incomes[3].ToString();
    }


}
