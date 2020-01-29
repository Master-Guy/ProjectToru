using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Karen : NPC
{
	
	bool surrender = false;
	bool fleeTrue = false;
	
    // Start is called before the first frame update
    void Start()
    {
		animator = GetComponent<Animator>();
        animator.SetFloat("moveX", -1);
    }
	
	public void Update()
    {
        this.statemachine.ExecuteStateUpdate();
        AdjustOrderLayer();
		FleeIfPossible();
    }

    void OnMouseDown()
    {
        Surrender();
    }
	
	public void Surrender()
    {
        if (currentRoom.SelectedPlayerInRoom() && !surrender)
        {
            this.surrender = true;
			animator.SetFloat("moveX", 0);
            this.statemachine.ChangeState(new Surrender(this.animator));
			
			LevelManager.emit("KarenSurrendered");
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
}
