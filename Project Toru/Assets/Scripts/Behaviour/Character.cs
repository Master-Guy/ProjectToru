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

    private bool didUseStair = false;
    private bool isDisabled;
    private ParticleSystem ps;

    private float timer = 0;
    private float stairsDuration = 1;
    public bool playerOnTheStairs = false;

    public GameObject currentRoom;
    public static Character selectedCharacter;

    public float MaxWeight;

    public GameObject firePoint;
    public Weapon weapon;

    public List<Skills> skills = new List<Skills>();

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isDisabled = true;
        ps = GetComponent<ParticleSystem>();

        inventory = new Inventory(MaxWeight);

        weapon = GetComponentInChildren<Weapon>();

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
            if (Input.GetKey(KeyCode.F))
            {
                weapon.Shoot();
            }
        }

        AdjustOrderLayer();

        if (weapon != null)
        {
            FlipFirePoint();
        }
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
            if (selectedCharacter != null)

            {
                selectedCharacter.disableMovement();
            }

            selectedCharacter = this;

            this.enableMovement();

            inventory.UpdateUI();

            CameraBehaviour.freeLook = false;
            FindObjectOfType<TutorialTrigger>().TriggerDialogue();
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