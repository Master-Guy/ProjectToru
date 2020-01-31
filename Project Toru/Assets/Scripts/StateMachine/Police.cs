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
	[SerializeField]
	Room Dest, LastRoom;

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
			weapon.weaponHolder = gameObject;
			weapon.RevealGun();
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
		if (currentRoom != null && currentRoom.charactersInRoom.Count > 0)
		{
			//GetComponent<ExecutePathFindingNPC>().StopPathFinding();
			PoliceForce.getInstance().Alert(currentRoom);
			//this.statemachine.ChangeState(new Combat(weapon, gameObject, firePoint, animator, currentRoom.charactersInRoom.First().gameObject));
		}
		/*else if (LastRoom != null && LastRoom.charactersInRoom.Count > 0)
		{
			PoliceForce.getInstance().Alert(LastRoom);
			setPos(LastRoom);
		}*/
		else if(!(PoliceForce.getInstance().GetCurrentlyRunningState() is Defensive) && (currentRoom == Dest || LastRoom == Dest))
		{
			statemachine.ChangeState(new Idle(animator));
			PoliceForce.getInstance().RequestOrders(this);
		}

		statemachine.ExecuteStateUpdate();

		AdjustOrderLayer();

		/*if (stats.currentHealth < stats.maxHealth)
		{	
			// @todo
			// this.statemachine.ChangeState(new Combat(this.weapon, this.gameObject, this.stats, this.firePoint, this.animator));
		}*/

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

	public override void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Room"))
		{
			LastRoom = currentRoom;
			currentRoom = other.gameObject.GetComponent<Room>();
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		/*if (other.CompareTag("Room") && other.gameObject.GetComponent<Room>() == currentRoom)
		{
			var temp = currentRoom;
			currentRoom = LastRoom;
			LastRoom = temp;
		}*/
	}
}
