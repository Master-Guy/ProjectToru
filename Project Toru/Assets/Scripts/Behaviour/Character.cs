using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;

    public Inventory inventory;

    private bool didUseStair = false;
    private bool isDisabled;
    private ParticleSystem ps;

    private float timer = 0;
    private float stairsDuration = 1;
    public bool playerOnTheStairs = false;


    public GameObject currentRoom;

    public float MaxWeight;

	public static Character selectedCharacter;

	// Start is called before the first frame update
	void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isDisabled = true;
        ps = GetComponent<ParticleSystem>();

		inventory = new Inventory(MaxWeight);

        AdjustOrderLayer();
    }

    // Update is called once per frame
    void Update()
    {

        if (playerOnTheStairs)
        {
            timer += Time.deltaTime;

            if (timer > stairsDuration)
            {
                playerOnTheStairs = false;
                timer = 0;
                this.GetComponent<Renderer>().enabled = true;
                this.enableMovement();
            }
        }

        if (!isDisabled)
        {
            //Camera.main.GetComponent<CameraBehaviour>().target = transform;
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

	public Character getCurrentCharacter()
	{
		return selectedCharacter;
	}

	public bool HasKey(CardReader.CardreaderColor color)
    {
        foreach (Item i in inventory.getItemsList())
        {
            if (i is Key && ((Key)i).color == color)
                return true;
        }
        return false;
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
        playerOnTheStairs = true;
        this.GetComponent<Renderer>().enabled = false;
        this.disableMovement();

		//Go to next transform in pathfinding
		GetComponent<ExecutePathFinding>().current++;
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
        if (Input.GetMouseButtonDown(0))
        {
			if(selectedCharacter == null)
			{
				selectedCharacter = this;
				selectedCharacter.enableMovement();
			}
			else
			{
				selectedCharacter.disableMovement();
				selectedCharacter = this;
				selectedCharacter.enableMovement();
			}
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
