using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class Guard : NPC
{
    

    protected override void Start()
    {	
		base.Start();
        PingPong();
	}

	protected override void Update()
    {	
		base.Update();
        
		if(stats.currentHealth < stats.maxHealth)
		{
            // this.statemachine.ChangeState(new Combat(this.weapon, this.gameObject, this.stats, this.firePoint,this.animator));
		}
	}

	public override void Surrender()
    {
        base.Surrender();
		Say("You will regret this");
    }

	public override void StopShooting() {
		base.StopShooting();
		GetComponent<ExecutePathFindingNPC>().setPosTarget(startingPosition);
	}

	public void Arrest(Character character) {
		if (!surrender)
		this.statemachine.ChangeState(new Arrest(this, weapon, gameObject, firePoint, animator, character.gameObject));
	}
}
