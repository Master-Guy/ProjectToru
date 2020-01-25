using System;
using Assets.Scripts.Options;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public enum Skills
{
	hacker
}

public class Character : MonoBehaviour
{
	public float speed;
	private Rigidbody2D myRigidbody;
	public Vector3 change;
	private Animator animator;

	public Inventory inventory;

	private ParticleSystem ps;

	public GameObject currentRoom;
	public static Character selectedCharacter;

	public float MaxWeight;

	public GameObject firePoint;
	public Weapon weapon;

	public List<Skills> skills = new List<Skills>();

	private bool outline = false;

	// Start is called before the first frame update
	void Start()
	{
		myRigidbody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();

		ps = GetComponent<ParticleSystem>();

		inventory = new Inventory(MaxWeight);

		weapon = GetComponentInChildren<Weapon>();
	}

	// Update is called once per frame
	void Update()
	{
		if (selectedCharacter == this && !outline)
		{
			Outline.SetOutline(this.gameObject, Resources.Load<Material>("Shaders/Character-Outline"));
			outline = true;
		}

		if (selectedCharacter != this)
		{
			Outline.RemoveOutline(this.gameObject);
			outline = false;
		}

		if (this.Equals(selectedCharacter))
		{
			if (Input.GetKey(KeyCode.F))
			{
				weapon.Shoot();
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
			selectedCharacter = this;

			inventory.UpdateUI();
		}
	}

	private void FlipFirePoint()
	{
		GameObject firePoint = weapon.gameObject;

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

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Room"))
		{
			currentRoom = other.gameObject;
		}
	}
}