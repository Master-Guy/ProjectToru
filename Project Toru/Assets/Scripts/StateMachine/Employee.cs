using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee : NPC
{
	private bool surrender = false;
    private bool fleeTrue = false;

	public void Start()
	{
		this.startingPosition = transform.position;
		this.animator = GetComponent<Animator>();
		PingPong();
	}

	public void Update()
	{
		this.statemachine.ExecuteStateUpdate();
		AdjustOrderLayer();
		FleeIfPossible();
        showCountdown();

    }

	void OnMouseDown()
	{
		Surrender();
	}

	

	void Surrender()
	{
		if (currentRoom.SelectedPlayerInRoom() && !surrender)
		{
			this.surrender = true;
			this.statemachine.ChangeState(new Surrender(this.animator));
			Say("Don't shoot!");
			dropBag();
        }
	}

	void FleeIfPossible()
	{
		if (!currentRoom.AnyCharacterInRoom() && surrender)
		{
			this.surrender = false;
			this.statemachine.ChangeState(new Flee(this.escapePath, this.gameObject, this.animator));
            fleeTrue = true;
        }
	}


    void showCountdown()
    {
        if(fleeTrue)
        {
            Invoke("startCountDown", 5);
        }
        
    }

    void startCountDown()
    {
       
    }

}
