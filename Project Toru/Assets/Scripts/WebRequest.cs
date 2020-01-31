using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System;

public class WebRequest : MonoBehaviour
{

	public static Int32 timeStart = 0;
	public static Int32 totalTime = 0;

	public static string playerName = "player";

	public void setTime() {
		timeStart = getTime();
		playerName = "";
	}

	private Int32 getTime() {
		return (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
	}
	
    public void stopTime() {
		if (timeStart == 0) return;

		totalTime = getTime() - timeStart;
		timeStart = 0;
	}

	public void GoUpload() {
		StartCoroutine(Upload());
	}

	static IEnumerator Upload()
	{	
		Debug.Log("1");
		if (totalTime == 0) yield return 0;
		if (playerName == "") yield return 0;
		Debug.Log("2");

		WWWForm form = new WWWForm();
		form.AddField("key", "70cd531b-03a2-408f-ba82-956e382cd407");
		form.AddField("name", playerName);
		form.AddField("seconds", (int) totalTime);
		totalTime = 0;
		playerName = "";

		Debug.Log("3");
		
		using (UnityWebRequest www = UnityWebRequest.Post("https://clyde.ducosebel.nl/api/submit.php", form))
		{
			Debug.Log("4");
			yield return www.SendWebRequest();
			Debug.Log("5");
			if (www.isNetworkError || www.isHttpError)
			{
				Debug.Log(www.error);
			}
			else
			{
				Debug.Log("Form upload complete!");
			}
		}
	}
}
