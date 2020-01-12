using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void LevelScriptCallback();
public delegate void LevelScriptCallbackBool(bool value);
public delegate void LevelScriptCallbackString(string value);
public delegate void LevelScriptCallbackInt(int value);
public delegate void LevelScriptCallbackFloat(float value);

/// Yes, this can be better than copy/paste for each value type. But can't figure out how.
public class LevelScript : MonoBehaviour
{	
	static Dictionary<string, LevelScriptCallback> events 				= new Dictionary<string, LevelScriptCallback>();
	static Dictionary<string, LevelScriptCallbackBool> eventsBool 		= new Dictionary<string, LevelScriptCallbackBool>();
	static Dictionary<string, LevelScriptCallbackString> eventsString 	= new Dictionary<string, LevelScriptCallbackString>();
	static Dictionary<string, LevelScriptCallbackInt> eventsInt 		= new Dictionary<string, LevelScriptCallbackInt>();
	static Dictionary<string, LevelScriptCallbackFloat> eventsFloat 	= new Dictionary<string, LevelScriptCallbackFloat>();
	
	public void emit(string eventString)
	{
		Debug.Log("Emitting " + eventString);
		events[eventString]?.Invoke();
	}
	
	public void emit(string eventString, bool value)
	{
		eventsBool[eventString]?.Invoke(value);
	}
	
	public void emit(string eventString, string value)
	{
		eventsString[eventString]?.Invoke(value);
	}
	
	public void emit(string eventString, int value)
	{
		eventsInt[eventString]?.Invoke(value);
	}
	
	public void emit(string eventString, float value)
	{
		eventsFloat[eventString]?.Invoke(value);
	}
	
	public void on(string eventString, LevelScriptCallback callback)
	{
		events.Add(eventString, callback);	
	}
	
	public void on(string eventString, LevelScriptCallbackBool callback)
	{
		eventsBool.Add(eventString, callback);	
	}
	
	public void on(string eventString, LevelScriptCallbackString callback)
	{
		eventsString.Add(eventString, callback);	
	}
	
	public void on(string eventString, LevelScriptCallbackInt callback)
	{
		eventsInt.Add(eventString, callback);	
	}
	
	public void on(string eventString, LevelScriptCallbackFloat callback)
	{
		eventsFloat.Add(eventString, callback);	
	}
}
