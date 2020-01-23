using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PoliceForce : StateMachine//: MonoBehaviour
{
	List<Police> Cops = new List<Police>();
	
	static PoliceForce instance;

	public static PoliceForce getInstance()
	{
		if (instance == null)
			instance = new PoliceForce();
		return instance;
	}

	//	public void Start()
	private PoliceForce()
	{
		ChangeState(Defensive.getInstance());
	}

	public void Alert(Room seen)
	{
		((PoliceState)GetCurrentlyRunningState()).AddPosition(seen);
		foreach (var p in Cops)
			RequestOrders(p);
	}

	public void AlertKill()
	{
		ChangeState(Aggressive.getInstance());
	}

	public void AddCop(Police cop)
	{
		Cops.Add(cop);
		RequestOrders(cop);
	}

	public void RequestOrders(Police cop)
	{
		((PoliceState)GetCurrentlyRunningState()).MoveCop(cop);
	}
}