using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : NPC
{
    Weapon weapon;
    GameObject firePoint;

    void Start()
    {
        startingPosition = transform.position;
        weapon = GetComponent<Weapon>();
        stats = GetComponent<CharacterStats>();
		animator = GetComponent<Animator>();
        firePoint = transform.GetChild(1).gameObject;
        PingPong();
	}

    void Update()
    {
		this.statemachine.ExecuteStateUpdate();
		AdjustOrderLayer();

		if(stats.currentHealth < stats.maxHealth)
		{
            this.statemachine.ChangeState(new Combat(this.weapon, this.gameObject, this.stats, this.firePoint,this.animator));
		}
	}

}
