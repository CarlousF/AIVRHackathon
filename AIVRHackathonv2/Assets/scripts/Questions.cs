using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Questions : MonoBehaviour {

    // 1
    private SteamVR_TrackedObject trackedObj;
    // 2
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }


    public GameObject questionText;
    public int healthScore = 0;
    public int questionNumber;

    void setQuestion(int question)
    {
        //questionText = GameObject.Find("Question Text");
        switch (question)
        {
            case 1:
                questionText.GetComponent<TextMesh>().text = "Q1";
                break;
            case 2:
                questionText.GetComponent<TextMesh>().text = "Q2";
                break;
            case 3:
                questionText.GetComponent<TextMesh>().text = "Q3";
                break;
            case 4:
                questionText.GetComponent<TextMesh>().text = "Q4";
                break;
            case 5:
                questionText.GetComponent<TextMesh>().text = "Q5";
                break;
            case 6:
                questionText.GetComponent<TextMesh>().text = "Q6";
                break;
            case 7:
                if(healthScore > 10)
                {
                    Application.LoadLevel("Beach scene");
                }
                else
                {
                    Application.LoadLevel("Cube-scene");
                }
                
                break;
        }
        
        //Debug.Log("Q1");
    }

    string CheckForHit()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        //if Hit detected, get hit objects name
        if (Physics.Raycast(transform.position, transform.forward * 10, out hit))
        {
            //Debug.Log(hit.collider.name);
            return hit.collider.name;
        }
        return "";
    }

    void CheckIfAnsweredQuestion()
    {
        //if any of the 4 answer boxs hit, load next question, incriment question number and record designated health score
        if (CheckForHit() == "A1")
        {
            setQuestion(questionNumber);
            questionNumber++;
            healthScore = healthScore + 1;
        }
        else if(CheckForHit() == "A2")
        {
            setQuestion(questionNumber);
            questionNumber++;
            healthScore = healthScore + 2;
        }
        else if(CheckForHit() == "A3")
        {
            setQuestion(questionNumber);
            questionNumber++;
            healthScore = healthScore + 3;
        }
        else if (CheckForHit() == "A4")
        {
            setQuestion(questionNumber);
            questionNumber++;
            healthScore = healthScore + 4;
        }
        Debug.Log("health score: " + healthScore);
    }

    // Use this for initialization
    void Start () {
        questionNumber = 1;
        setQuestion(questionNumber);
        questionNumber++;
    }
	
	// Update is called once per frame
	void Update () {
        // 2
        if (Controller.GetHairTriggerUp())
        {
            CheckIfAnsweredQuestion();
        }
    }
}
