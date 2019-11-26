using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : NPC
{
	private bool flee = false;

    void Start()
    {
		startingPosition = transform.position;
        stats = GetComponent<CharacterStats>();
		animator = GetComponent<Animator>();
		PingPong();
	}

    void Update()
    {
		this.statemachine.ExecuteStateUpdate();
		AdjustOrderLayer();

		if(stats.health < 100 && !flee)
		{
			this.statemachine.ChangeState(new Flee(this.escapePath, this.gameObject, this.animator));
			flee = true;
		}
	}

}
