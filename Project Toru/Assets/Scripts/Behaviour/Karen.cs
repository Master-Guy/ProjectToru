using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Karen : NPC
{
    // Start is called before the first frame update
    protected override void Start()
    {
		base.Start();
    }
	
	protected override void Update()
    {
		base.Update();

		FleeIfPossible();
    }

	public override void Surrender() {
		base.Surrender();

		Say("Don't shoot!");
	}

	public void BeKaren(Character character) {
		statemachine.ChangeState(new BeKaren(this, character, firePoint, weapon));
	}

}
