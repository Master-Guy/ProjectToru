using Assets.Domain.Behaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class SelectedCharacter
{
	static Character Selected;
}
public class Character : MonoBehaviour
{
	public float speed;
	private Rigidbody2D myRigidbody;
	private Vector3 change;
	private Animator animator;

	private List<Item> inventory;

	Character()
	{
		inventory = new List<Item>();
	}

	// Start is called before the first frame update
	void Start()
	{
		myRigidbody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		change = Vector3.zero;
		change.x = Input.GetAxisRaw("Horizontal");
		change.y = Input.GetAxisRaw("Vertical");
		UpdateAnimationsAndMove();
	}

	public bool hasKey(int key)
	{
		foreach(Item i in inventory)
		{
			if (i is Key && ((Key)i).privateKey == key)
				return true;
		}
		return false;
	}

	public void addItem(Item i)
	{
		inventory.Add(i);
	}

	void UpdateAnimationsAndMove()
	{
		if (change != Vector3.zero)
		{
			MoveCharacter();
			animator.SetFloat("moveX", change.x);
			animator.SetFloat("moveY", change.y);
			animator.SetBool("moving", true);
		}
		else
		{
			animator.SetBool("moving", false);
		}
	}

	void MoveCharacter()
	{
		change.Normalize();
		myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
	}
}
