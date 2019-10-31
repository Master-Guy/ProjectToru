using Assets.Domain.Behaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	public float speed;
	private Rigidbody2D myRigidbody;
	private Vector3 change;
	private Animator animator;

	private Inventory inventory;
	
	private bool didUseStair = false;
	private static CharacterManager cm = new CharacterManager();
	private bool isDisabled;
	private ParticleSystem ps;

	public GameObject currentRoom;
	public static GameObject selectedCharacter;

	// Start is called before the first frame update
	void Start()
	{
		myRigidbody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		isDisabled = true;
		ps = GetComponent<ParticleSystem>();

		inventory = new Inventory();
		AdjustOrderLayer();
	}

	// Update is called once per frame
	void Update()
	{
		if (!isDisabled)
		{
			Camera.main.GetComponent<CameraBehaviour>().target = transform;
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
	}

	public bool HasKey(int key)
	{
		foreach(Item i in inventory.getItemsList())
		{
			if (i is Key && ((Key)i).privateKey == key)
				return true;
		}
		return false;
	}

	public void AddItemToList(Item item)
	{
		//Debug.Log(item);
		inventory.addItem(item);
	}

	void UpdateAnimationsAndMove()
	{
		if (change != Vector3.zero)
		{
			AdjustOrderLayer();
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
		//Debug.Log("MovedStairs");
	}

	public void enableMovement()
	{
		isDisabled = false;
	}

	public void disableMovement()
	{
		isDisabled = true;
	}

	void OnMouseDown()
	{
		if (Input.GetMouseButtonDown(0)) {
			cm.disableCharacterMovement();
			selectedCharacter = this.gameObject;
			enableMovement();
			ps.Play();
			inventory.UpdateUI();
		}
	}

	void AdjustOrderLayer()
	{
		GetComponent<SpriteRenderer>().sortingOrder = (int)(-transform.position.y * 1000);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Room"))
		{
			currentRoom = other.gameObject;
		}
	}
}
