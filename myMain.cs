using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Networking;
using Assets.BotDirectLine;
using SimpleJSON;
using System.Text;
using System.Threading;

public class myMain : MonoBehaviour {

	string conversationId = "123";

	Boolean first_conversation = true;

	int counter =0;

	string[] answers = {
		"Not at all", "Several days", "More than half the days", "Nearly every day",
		"Yes", "No",
		"Your health","Your weight or how your look","Little or no sexual desire or pleasure during sex","Difficulties with your partner","The stress of taking care of family members","Stress at work, school or outside home","By financial problems or worreis","Having no one to turn to","Something bad that happened recently", "None of the above"
	};

	string[]incoming_questions= new string[60];
	int[] response = new int[18];
	int highest = 0;

	// Use this for initialization
	void Start () {


		Debug.Log ("we started!");
		BotDirectLineManager.Initialize("acpdzbZb2Oc.cwA.TB0.6a3lFLDlLNV_RMvX4uB8sm9vGsbXaOU7BIA6N1uyiws");
		BotDirectLineManager.Instance.BotResponse += OnBotResponse;
		Debug.Log ("About to call start coroutine");
		StartCoroutine(BotDirectLineManager.Instance.StartConversationCoroutine());

	}
	
	// Update is called once per frame
	void Update () {


		//if ( first_conversation == true && conversationId != "123") {
			
		//	StartCoroutine (BotDirectLineManager.Instance.SendMessageCoroutine (
		//		conversationId, "UnityUserId", "update", "Unity User 1"));
		//	first_conversation = false;

		//}
	}

	private void OnBotResponse(object sender, Assets.BotDirectLine.BotResponseEventArgs e)
	{
		
		switch (e.EventType)
		{
		case EventTypes.ConversationStarted:
			this.conversationId = e.ConversationId;
		
			Debug.Log ("conversation started! Convo ID: "+e.ConversationId);
			StartCoroutine (BotDirectLineManager.Instance.SendMessageCoroutine (
				conversationId, "UnityUserId", "Hello bot starting the conversation!", "Unity User 1"));	
			break;

		case EventTypes.MessageSent:
			
			if (!string.IsNullOrEmpty (conversationId)) {
				// Get the bot's response(s)
				StartCoroutine (BotDirectLineManager.Instance.GetMessagesCoroutine (this.conversationId));
			}

			break;
		case EventTypes.MessageReceived:
			// Handle the received message(s)
			Debug.Log ("Message received." + e.ToString () + "number of messages: " + e.Messages.Count);

			//if (counter < 18 && i > 2) {
			if ( first_conversation == true && conversationId != "123")
			{
				StartCoroutine (BotDirectLineManager.Instance.SendMessageCoroutine (
					conversationId, "BotUserId", "update", "System user 1"));
				first_conversation = false;

			} else {
				for (int i = highest; i < e.Messages.Count; i++) {
					if (e.Messages [i].FromName == "VRARBot") {
						string question = e.Messages [i].Text;
						Debug.Log ("The incoming question is printed here: " + question  + "  counter ==" + counter + "i== " + i);
						incoming_questions[counter]=(e.Messages [i].Text);
						highest = i+1 ;
						counter = counter + 1;

						StartCoroutine (BotDirectLineManager.Instance.SendMessageCoroutine (
							this.conversationId, "UnityUserId", "{“index”:2,”entity”:”More than half the days”,”score”:1}", "unity_answer"));
					
					} else {
						continue;
					}
			
				}
			}

			break;
		case EventTypes.Error:
			// Handle the error
			break;
		}

	}
}
