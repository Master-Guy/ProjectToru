using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SubmitController : MonoBehaviour
{
   
    [SerializeField]
    TextMesh Title = null;

    [SerializeField]
    TextMesh Message = null;
	
	[SerializeField]
    TextMesh ContinueButton = null;
	
	[SerializeField]
    SceneSwitcherSubmit ContinueButtonScript = null;


	void Start() {

		ContinueButtonScript.scene = "MainMenu";

		if (WebRequest.totalTime == 0) {
			Message.text = "You finished the game";
			return;
		}

		Message.text = "You finished the game in " + WebRequest.totalTime + " seconds";

		ContinueButtonScript.webRequest = GetComponent<WebRequest>();
	}
}
