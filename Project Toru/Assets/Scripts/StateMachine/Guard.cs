using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : NPC
{
    public Weapon weapon;
    GameObject firePoint;

    Vector3 currentpos;
    Vector3 lastpos;
    Vector3 change;

    void Start()
    {
        startingPosition = transform.position;;
        stats = GetComponent<CharacterStats>();
		animator = GetComponent<Animator>();
        weapon = GetComponentInChildren<Weapon>();
        if(weapon != null)
        {
            firePoint = weapon.gameObject;
			weapon.weaponHolder = gameObject;
			weapon.RevealGun();
        }
		PingPong();
		if (weapon != null)
		{
			animator.SetBool("isHoldingGun", true);
		}

		weapon.gameObject.transform.position = transform.position + new Vector3(.3f, -.3f);
	}

	bool release = true;
    void Update()
    {
		this.statemachine.ExecuteStateUpdate();
		AdjustOrderLayer();

		if(stats.currentHealth < stats.maxHealth)
		{
            // this.statemachine.ChangeState(new Combat(this.weapon, this.gameObject, this.stats, this.firePoint,this.animator));
		}

        lastpos = currentpos;
        currentpos = transform.position;
        change = currentpos - lastpos;

        if(change != Vector3.zero && weapon != null)
        {
            FlipFirePoint();
        }
    }

	Character targetCharacter = null;

	public void ShootAt(Character character) {
		
		// this.statemachine.ChangeState(new Idle(animator));
		this.statemachine.ChangeState(new Combat(weapon, gameObject, firePoint, animator, character.gameObject));
	}


    private void FlipFirePoint()
    {
        if (animator.GetFloat("moveX") > 0)
        {
            firePoint.transform.rotation = Quaternion.Euler(0, 0, 0);
            firePoint.transform.position = transform.position + new Vector3(.3f, -.3f);
            firePoint.GetComponent<SpriteRenderer>().sortingLayerName = "Guns";
        }
        else
        {
            firePoint.transform.rotation = Quaternion.Euler(0, 180, 0);
            firePoint.transform.position = transform.position + new Vector3(-.3f, -.3f);
            firePoint.GetComponent<SpriteRenderer>().sortingLayerName = "Guns";
        }

        if (animator.GetFloat("moveY") > 0.1)
        {
            weapon.HideGun();
        }
        else
        {
            weapon.RevealGun();
        }
    }
}
