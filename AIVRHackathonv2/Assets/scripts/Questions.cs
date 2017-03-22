using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using System.Threading;
using SimpleJSON;

public class Questions : MonoBehaviour {

    public static string ConversationId = "";

    public static bool answerReceived = false;

    public static string BotId = "VRARBot";

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

    private void OnBotResponse(object sender, Assets.BotDirectLine.BotResponseEventArgs e)
    {

        switch (e.EventType)
        {
            case Assets.BotDirectLine.EventTypes.ConversationStarted:
                // Store the ID
                Debug.Log("Conversation Started.");
                answerReceived = true;
                ConversationId = e.ConversationId;
                break;
            case Assets.BotDirectLine.EventTypes.MessageSent:
                Debug.Log("Message sent..");
                if (!string.IsNullOrEmpty(ConversationId))
                {
                    // Get the bot's response(s)
                    StartCoroutine(BotDirectLineManager.Instance.GetMessagesCoroutine(ConversationId));
                }

                break;
            case Assets.BotDirectLine.EventTypes.MessageReceived:
                // Handle the received message(s)
                Debug.Log("Message received");
                if(e.Messages.Count > 0){
                    Debug.Log("OnBotResponse: " + e.ToString());

                    JSONNode ResObject;
                    JSONNode FromObject;
                    string fromId;

                     for (int L = e.Messages.Count-1; L > 0; L--)
                    {
                        ResObject = JSON.Parse((e.Messages[L]).ToString());

                        FromObject = JSON.Parse(ResObject["from"].ToString());
                        fromId = FromObject["id"];
                        Debug.Log("from is: "+fromId);

                        if(fromId == BotId){
                            Debug.Log("Latest message from  Bot: " + ResObject["text"]);
                            setQuestionNameAndAnswers(ResObject["text"]);
                            break;
                        }
                    }

                }
                
                break;
            case Assets.BotDirectLine.EventTypes.Error:
                // Handle the error
                break;
        }
    }

    public GameObject questionText;
    public int healthScore = 0;
    public int questionNumber;

    void setQuestionNameAndAnswers(string newName)
    {

        string[] splitArray = newName.Split('1');
        string theQuestion = splitArray[0];

        questionText = GameObject.Find("Question Text");
        questionText.GetComponent<TextMesh>().text = theQuestion;
    }

    void setQuestion(int question)
    {
        questionText = GameObject.Find("Question Text");
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
        Debug.Log("Starting.");
        questionNumber = 1;
        setQuestion(questionNumber);
        questionNumber++;

        //Initilise the ChatBot with the secret Key
        BotDirectLineManager.Initialize("acpdzbZb2Oc.cwA.TB0.6a3lFLDlLNV_RMvX4uB8sm9vGsbXaOU7BIA6N1uyiws");
        //Ensure onBotResponse function is called when a response is received
        BotDirectLineManager.Instance.BotResponse += OnBotResponse;

        //Start the conversation.
        StartCoroutine(BotDirectLineManager.Instance.StartConversationCoroutine());

    }

	// Update is called once per frame
	void Update () { 
        //HERE TO SIMULATE USER SELECTING OPTION AT THE MOMENT.
        if (Input.GetKeyDown("space")){
            print("space key was pressed");
            //CHANGE 3RD OPTION TO THE OPTION SELECTED BY THE USER.
            StartCoroutine(BotDirectLineManager.Instance.SendMessageCoroutine(
                ConversationId, "UnityUserId", "1", "Unity User 1"));
        }
    }
    
}
