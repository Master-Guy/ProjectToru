using Assets.Domain.Behaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour
{
	[SerializeField]
	private Room roomLeft = null, roomRight = null;

	[SerializeField]
	private int key;

    private bool closed = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.CompareTag("Player"))
            if (collision.gameObject.GetComponent<Character>().hasKey(key) && closed)
                OpenDoor();
	}

    private void OpenDoor()
    {
        Debug.Log("Opening door");
        closed = false;
        GetComponent<Animator>().SetBool("openDoor", true);
        GetComponent<Collider2D>().enabled = false;
    }
}
