using System.Collections.Generic;
using UnityEngine;

public class Surrender : IState
{
	private Animator animator;
	private GameObject gameObject;
	private NPC employee;
	private Room currentRoom;
	private bool surrender = false;

	public Surrender(Animator animator, GameObject gameObject, Room currentRoom)
	{
		this.currentRoom = currentRoom;
		this.animator = animator;
		this.gameObject = gameObject;
		employee = gameObject.GetComponent<NPC>();
	}

	public void Enter()
	{
		animator.SetBool("Surrendering", true);
		employee.Say("Don't shoot!");
	}

	public void Execute()
	{

	}

	public void Exit()
	{
		animator.SetBool("Surrendering", false);
	}
}
