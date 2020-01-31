using System;
using Assets.Scripts.Options;
using System.Collections.Generic;
using UnityEngine;

public enum Skills
{
	hacker
}

public class Character : MonoBehaviour
{
	public float speed;
	private Rigidbody2D myRigidbody;
	public Vector3 change;
    [NonSerialized]
	public Animator animator;

	public Inventory inventory;

	private ParticleSystem ps;

	public GameObject currentRoom;
	public static Character selectedCharacter;

	public float MaxWeight;

	//public GameObject firePoint;
	public Weapon weapon;
	bool weaponKeyRelease = true;

	public List<Skills> skills = new List<Skills>();

	// Start is called before the first frame update
	void Start()
	{
		myRigidbody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();

		ps = GetComponent<ParticleSystem>();

		inventory = new Inventory(MaxWeight);

		weapon = GetComponentInChildren<Weapon>();
		if (weapon != null)
		{
			weapon.weaponHolder = this.gameObject;
			weapon.gameObject.transform.position = transform.position + new Vector3(.3f, -.3f);
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (this.Equals(selectedCharacter))
		{
			if(weapon != null) {
				if (Input.GetKey(KeyCode.F))
				{
					if (weaponKeyRelease)
						LevelManager.emit("PlayerHasUsedGun");
					
					weaponKeyRelease = false;
					weapon?.Shoot();
				} else {
					weaponKeyRelease = true;
				}
                if (Input.GetKeyDown(KeyCode.H))
                {
					if (weapon.weaponOut)
					{
						weapon.HideGun();
					}
					else
					{
						weapon.RevealGun();
					}
                }
			}
		}

		if(weapon != null)
		{
			FlipFirePoint();
		}
	}

	public bool HasKey(CardreaderColor color)
	{
		foreach (Item i in inventory.getItemsList())
		{
			if (i is Key && ((Key)i).color == color)
				return true;
		}
		return false;
	}

	void OnMouseDown()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (selectedCharacter != null)
			{
				selectedCharacter.transform.Find("SelectedTriangle").gameObject.GetComponent<SpriteRenderer>().enabled = false;
			}

			selectedCharacter = this;
			
			LevelManager.emit("CharacterHasBeenSelected");

			inventory.UpdateUI();

			transform.Find("SelectedTriangle").gameObject.GetComponent<SpriteRenderer>().enabled = true;
		}
	}

	private void FlipFirePoint()
	{
		GameObject firePoint = weapon.gameObject;

		if (animator.GetFloat("moveX") > 0.1)
		{
			firePoint.transform.rotation = Quaternion.Euler(0, 0, 0);
			firePoint.transform.position = transform.position + new Vector3(.3f, -.3f);
			firePoint.GetComponent<SpriteRenderer>().sortingLayerName = "Guns";
		}

		if (animator.GetFloat("moveX") < -0.1)
		{
			firePoint.transform.rotation = Quaternion.Euler(0, 180, 0);
			firePoint.transform.position = transform.position + new Vector3(-.3f, -.4f);
			firePoint.GetComponent<SpriteRenderer>().sortingLayerName = "Guns";
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Room"))
		{
			currentRoom = other.gameObject;
			LevelManager.emit("CharacterIsInRoom", currentRoom.name);
		}
	}
}