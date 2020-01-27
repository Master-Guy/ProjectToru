using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Karen : NPC
{
	
	bool surrender = false;
	
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
			animator.SetFloat("moveX", 0);
            this.statemachine.ChangeState(new Surrender(this.animator));
			
			LevelManager.emit("KarenSurrendered");
        }
    }
}
