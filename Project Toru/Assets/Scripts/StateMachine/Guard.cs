using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
