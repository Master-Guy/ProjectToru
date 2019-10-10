using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
	public float speed;
	private Rigidbody2D myRigidbody;
	private Vector3 change;
	private Animator animator;

	private bool didUseStair = false;

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

		// If player releases UP, reset Stair
		if (didUseStair && change.y > 0)
		{
			change.y = 0;

		}

		// If player wants to go up, ignore movement
		else if (didUseStair && change.y <= 0)
		{
			didUseStair = false;
		}

		UpdateAnimationsAndMove();
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
		else if (didUseStair)
		{
			animator.SetBool("moving", false);
			animator.SetFloat("moveY", -1);

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

	public void StairsTransistion()
	{
		didUseStair = true;
	}
}
