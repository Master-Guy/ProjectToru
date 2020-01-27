using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ExecutePathFindingNPC))]
public abstract class NPC : MonoBehaviour
{
    [SerializeField]
    private GameObject[] bag = null;

    [SerializeField]
    private GameObject TextBox = null;

    [SerializeField]
    public GameObject[] escapePath = null;

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
            Outline.SetOutline(this.gameObject);
        }
    }

    public void PingPong()
    {
        this.statemachine.ChangeState(new PingPong(this.startingPosition, this.gameObject, this.animator));
    }

    private void OnMouseExit()
    {
        Outline.RemoveOutline(this.gameObject);
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
}
