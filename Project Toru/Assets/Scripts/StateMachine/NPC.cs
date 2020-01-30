using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class NPC : MonoBehaviour
{
    [SerializeField]
    private GameObject[] bag = null;

    [SerializeField]
    private GameObject TextBox = null;

    [NonSerialized]
    public Room currentRoom = null;

    [NonSerialized]
    public StateMachine statemachine = new StateMachine();

    [NonSerialized]
    public Vector3 startingPosition = Vector3.zero;

    [NonSerialized]
    public Animator animator = null;

    [NonSerialized]
    public CharacterStats stats;

	protected Weapon weapon;
	protected bool showWeapon = false;
	protected GameObject firePoint;

	Vector3 currentpos;
    Vector3 lastpos;
    Vector3 change;

	protected virtual void Awake() {

		startingPosition = transform.position;;
        stats = GetComponent<CharacterStats>();
		animator = GetComponent<Animator>();
        weapon = GetComponentInChildren<Weapon>();

		if(weapon != null)
        {
            firePoint = weapon.gameObject;
			weapon.weaponHolder = gameObject;
        }
	}

	protected virtual void Start() {

	}

	protected virtual void Update() {
		this.statemachine.ExecuteStateUpdate();
		AdjustOrderLayer();

		lastpos = currentpos;
        currentpos = transform.position;
        change = currentpos - lastpos;

		if(change != Vector3.zero && showWeapon)
        {
            FlipFirePoint();
        }
	}

    public void dropBag()
    {
        foreach (GameObject g in bag)
        {
            Instantiate(g, new Vector3(transform.position.x + UnityEngine.Random.Range(-1.5f, 1.5f), currentRoom.transform.position.y + 0.5f, 0), Quaternion.identity);
        }
    }

    public void Say(string text)
    {
        TextBox.GetComponent<TextMesh>().text = text;
        TextBox.SetActive(true);
        Invoke("disableTextBox", 3);
    }

    private void disableTextBox()
    {
        TextBox.SetActive(false);
    }

    public void AdjustOrderLayer()
    {
        GetComponent<SpriteRenderer>().sortingOrder = (int)(-transform.position.y * 1000);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Room"))
        {
            currentRoom = other.gameObject.GetComponent<Room>();
        }
    }

    private void OnMouseOver()
    {
        if (currentRoom.SelectedPlayerInRoom())
        {
            Tint.Apply(this.gameObject);
        }
    }

    public void PingPong()
    {
        this.statemachine.ChangeState(new PingPong(this.startingPosition, this.gameObject, this.animator));
    }

    private void OnMouseExit()
    {
        Tint.Reset(this.gameObject);
    }

    public bool HasKey(CardreaderColor color)
    {
        foreach (GameObject i in bag)
        {
            if (i.GetComponent<Key>())
            {
                if (i.GetComponent<Key>().color == color)
                {
                    return true;
                }
            }
        }
        return false;
    }

	public void ShootAt(Character character) {
		
		showWeapon = true;
		this.statemachine.ChangeState(new Combat(this, weapon, gameObject, firePoint, animator, character.gameObject));
	}

	protected virtual void FlipFirePoint()
    {
        if (animator.GetFloat("moveX") > 0)
        {
            firePoint.transform.rotation = Quaternion.Euler(0, 0, 0);
            firePoint.transform.position = transform.position + new Vector3(.3f, -.3f);
            firePoint.GetComponent<SpriteRenderer>().sortingLayerName = "Guns";
        }
        else
        {
            firePoint.transform.rotation = Quaternion.Euler(0, 180, 0);
            firePoint.transform.position = transform.position + new Vector3(-.3f, -.3f);
            firePoint.GetComponent<SpriteRenderer>().sortingLayerName = "Guns";
        }

        if (animator.GetFloat("moveY") > 0.1)
        {
            weapon.HideGun();
        }
        else
        {
            weapon.RevealGun();
        }
    }
}
