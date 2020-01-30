using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatGuy : NPC
{
    private bool surrender = false;
    private bool fleeTrue = false;

    protected override void Start()
    {
        base.Start();
        PingPong();
    }

    protected override void Update()
    {
        base.Update();
        
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
        if (currentRoom == null) return;

        if (!currentRoom.AnyCharacterInRoom() && surrender)
        {
            this.surrender = false;
            this.statemachine.ChangeState(new Idle(this.animator));
			gameObject.GetComponent<ExecutePathFindingNPC>().setPosTarget(-30, 1);
            fleeTrue = true;
        }
    }

    void showCountdown()
    {
        if (fleeTrue)
        {
            Invoke("startCountDown", 8);
            fleeTrue = false;
        }
    }

    void startCountDown()
    {
		LevelManager.emit("EmployeeFleed");
    }
}
