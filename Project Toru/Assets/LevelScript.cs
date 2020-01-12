using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void LevelScriptCallback();

public class LevelScript : MonoBehaviour
{
	
	static Dictionary<string, LevelScriptCallback> events = new Dictionary<string, LevelScriptCallback>();
	
	public void emit(string eventString)
	{
		Debug.Log("Emitting event " + eventString);
		events[eventString]?.Invoke();
	}
	
	public void on(string eventString, LevelScriptCallback callback)
	{
		
		events.Add(eventString, callback);
		
	}
}
