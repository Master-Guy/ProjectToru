using Assets.Domain.Behaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorBehaviour : MonoBehaviour
{
	[SerializeField]
	public RoomBehaviour roomLeft = null, roomRight = null;

	[SerializeField]
	private Tilemap appearance = null;

	[SerializeField]
	private int key;

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
			if (collision.gameObject.GetComponent<Character>().hasKey(key))
				this.gameObject.SetActive(false);
	}
}
