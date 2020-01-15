using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : NPC
{
    Weapon weapon;
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
        }
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

        lastpos = currentpos;
        currentpos = transform.position;
        change = currentpos - lastpos;

        if(change != Vector3.zero && weapon != null)
        {
            FlipFirePoint();
        }
    }

    private void FlipFirePoint()
    {
        if (change.x > 0)
        {
            firePoint.transform.rotation = Quaternion.Euler(0, 0, 0);
            firePoint.transform.position = transform.position + new Vector3(.3f, -.3f);
            firePoint.GetComponent<SpriteRenderer>().sortingLayerName = "Guns";
        }
        if (change.x < 0)
        {
            firePoint.transform.rotation = Quaternion.Euler(0, 180, 0);
            firePoint.transform.position = transform.position + new Vector3(-.3f, -.3f);
            firePoint.GetComponent<SpriteRenderer>().sortingLayerName = "Guns";
        }
        if (change.y > 0)
        {
            firePoint.GetComponent<SpriteRenderer>().sortingLayerName = "Background Items";
            firePoint.transform.position = transform.position + new Vector3(0, -.3f);
        }
        if (change.y < 0)
        {
            firePoint.GetComponent<SpriteRenderer>().sortingLayerName = "Guns";
            firePoint.transform.position = transform.position + new Vector3(0, -.3f);
        }
    }

}
