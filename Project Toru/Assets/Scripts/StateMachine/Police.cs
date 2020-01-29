using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Police : NPC
{
	public Weapon weapon;
	GameObject firePoint;
	Room Dest;

	Vector3 currentpos;
	Vector3 lastpos;
	Vector3 change;

	public void Start()
	{

		startingPosition = transform.position; ;
		stats = GetComponent<CharacterStats>();
		animator = GetComponent<Animator>();
		weapon = GetComponentInChildren<Weapon>();
		if (weapon != null)
		{
			firePoint = weapon.gameObject;
		}
		if (weapon != null)
		{
			animator.SetBool("isHoldingGun", true);
		}

		weapon.gameObject.transform.position = transform.position + new Vector3(.3f, -.3f);
		statemachine.ChangeState(new Idle(animator));
		PoliceForce.getInstance().RequestOrders(this);
	}

	public void Update()
	{
		if (currentRoom.charactersInRoom.Count > 0 || currentRoom == Dest)
		{
			GetComponent<ExecutePathFindingNPC>().StopPathFinding();
			// TODO fight
			weapon.Shoot();
			this.statemachine.ChangeState(new Combat(this.weapon, this.gameObject, this.stats, this.firePoint, this.animator));
		}
		else if(!(PoliceForce.getInstance().GetCurrentlyRunningState() is Defensive) && currentRoom == Dest)
		{
			PoliceForce.getInstance().RequestOrders(this);
		}

		AdjustOrderLayer();

		if (stats.currentHealth < stats.maxHealth)
		{
			this.statemachine.ChangeState(new Combat(this.weapon, this.gameObject, this.stats, this.firePoint, this.animator));
		}

		lastpos = currentpos;
		currentpos = transform.position;
		change = currentpos - lastpos;

		if (change != Vector3.zero && weapon != null)
		{
			FlipFirePoint();
		}
	}

	public void setPos(Room dest)
	{
		Dest = dest;
		GetComponent<ExecutePathFindingNPC>().setPosTarget(dest.GetPosition());
	}

	private void FlipFirePoint()
	{
		if (change.x > 0)
		{
			firePoint.transform.rotation = Quaternion.Euler(0, 0, 0);
			firePoint.transform.position = transform.position + new Vector3(.3f, -.3f);
			firePoint.GetComponent<SpriteRenderer>().sortingLayerName = "Guns";
		}
		if (change.x < 0)
		{
			firePoint.transform.rotation = Quaternion.Euler(0, 180, 0);
			firePoint.transform.position = transform.position + new Vector3(-.3f, -.3f);
			firePoint.GetComponent<SpriteRenderer>().sortingLayerName = "Guns";
		}
		if (change.y > 0)
		{
			firePoint.GetComponent<SpriteRenderer>().sortingLayerName = "Background Items";
			firePoint.transform.position = transform.position + new Vector3(0, -.3f);
		}
		if (change.y < 0)
		{
			firePoint.GetComponent<SpriteRenderer>().sortingLayerName = "Guns";
			firePoint.transform.position = transform.position + new Vector3(0, -.3f);
		}
	}
}
