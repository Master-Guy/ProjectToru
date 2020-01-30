using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatGuy : NPC
{
    // Start is called before the first frame update
    protected override void Start()
    {
		base.Start();
        PingPong();
    }
	
	protected override void Update()
    {
		base.Update();

		FleeIfPossible();
    }

}
