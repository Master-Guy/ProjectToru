using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Police : NPC
{
	[SerializeField]
	Room Dest;

	protected override void Awake() {
		base.Awake();

		/*if (weapon != null)
		{
			weapon.RevealGun();
		}*/
	}

	protected override void Start()
	{
		base.Start();

		weapon.gameObject.transform.position = transform.position + new Vector3(.3f, -.3f);
		statemachine.ChangeState(new Idle(animator));
		PoliceForce.getInstance().RequestOrders(this);
	}

	protected override void Update()
	{	
		base.Update();
		
		if (currentRoom != null && currentRoom.charactersInRoom.Count > 0)
		{
			//GetComponent<ExecutePathFindingNPC>().StopPathFinding();
			PoliceForce.getInstance().Alert(currentRoom);
			if(!(statemachine.GetCurrentlyRunningState() is Combat))
				this.statemachine.ChangeState(new Combat(this, weapon, gameObject, firePoint, animator, currentRoom.charactersInRoom.First().gameObject));
		}
		/*else if (LastRoom != null && LastRoom.charactersInRoom.Count > 0)
		{
			PoliceForce.getInstance().Alert(LastRoom);
			setPos(LastRoom);
		}*/
		else if(!(PoliceForce.getInstance().GetCurrentlyRunningState() is Defensive) && !(statemachine.GetCurrentlyRunningState() is Combat) && currentRoom == Dest)
		{
			statemachine.ChangeState(new Idle(animator));
			PoliceForce.getInstance().RequestOrders(this);
		}

		/*if (stats.currentHealth < stats.maxHealth)
		{	
			// @todo
			// this.statemachine.ChangeState(new Combat(this.weapon, this.gameObject, this.stats, this.firePoint, this.animator));
		}*/
	}

	public void setPos(Room dest)
	{
		Dest = dest;
		GetComponent<ExecutePathFindingNPC>().setPosTarget(Dest.GetPosition());
	}
}
