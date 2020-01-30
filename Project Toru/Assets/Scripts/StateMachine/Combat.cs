using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : IState
{
    private Weapon weapon;
    private GameObject gameObject;
    private CharacterStats stats;
    private GameObject firePoint;
    private Animator animator;
    private GameObject target;

	float timer = 0.5f;
	bool moving = false;

    public Combat(Weapon weapon, GameObject gameObject, GameObject firePoint, Animator animator, GameObject target)
    {
        this.weapon = weapon;
        this.gameObject = gameObject;
        this.stats = stats;
        this.firePoint = firePoint;
        this.animator = animator;
		this.target = target;
    }

    public void Enter()
    {

    }

    public void Execute()
    {	
		timer -= Time.deltaTime;
		if (timer <= 0) {
			timer = 0.5f;
			moving = Move();
		}

		if (!moving) {
			CheckTargetDirection();
			AdjustFirePoint();
			weapon.Shoot();
		}
        
    }

    public void Exit()
    {

    }

	bool Move() {

		Vector3 distance = target.transform.position - gameObject.transform.position;
		
		if (Mathf.Abs(distance.x) > 4 || Mathf.Abs(distance.y) > 2 || Mathf.Abs(distance.x) < 1) {
			Vector3 target = this.target.transform.position;
			if (distance.x > 0) target.x -= 2;
			else				target.x += 2;

			gameObject.GetComponent<ExecutePathFindingNPC>().setPosTarget(target);

			return false;
		}

		return true;
	}

    void CheckTargetDirection()
    {
        Vector3 distance = target.transform.position - gameObject.transform.position;
		if (distance.x <= 0) {
			animator.SetFloat("moveX", -1f);
		} else {
			animator.SetFloat("moveX", 1f);
		}
    }

    void AdjustFirePoint()
    {
        if (animator.GetFloat("moveX") > 0)
        {
            firePoint.transform.rotation = Quaternion.Euler(0, 0, 0);
            firePoint.transform.position = gameObject.transform.position + new Vector3(.3f, -.3f);
            firePoint.GetComponent<SpriteRenderer>().sortingLayerName = "Guns";
        }
        else
        {
            firePoint.transform.rotation = Quaternion.Euler(0, 180, 0);
            firePoint.transform.position = gameObject.transform.position + new Vector3(-.3f, -.3f);
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

    // void ChangeAnimations()
    // {
    //     if (direction == targetDirection.left)
    //     {
    //         animator.SetFloat("moveX", -1);
    //     }
    //     else
    //     {
    //         animator.SetFloat("moveX", 1);
    //     }
    // }

    // private enum targetDirection
    // {
    //     left,
    //     right
    // }
}
