using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : NPC
{

    void Start()
    {
        stats = GetComponent<CharacterStats>();
	}

    void Update()
    {
		this.statemachine.ExecuteStateUpdate();
		AdjustOrderLayer();
	}

	void OnMouseDown()
	{
        if (currentRoom.SelectedPlayerInRoom())
        {
            stats.TakeDamage(20);
            Debug.Log("health: " + stats.health);
        }
	}
}
