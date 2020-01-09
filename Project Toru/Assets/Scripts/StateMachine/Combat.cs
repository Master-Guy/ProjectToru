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
    private targetDirection direction;

    public Combat(Weapon weapon, GameObject gameObject, CharacterStats stats, GameObject firePoint, Animator animator)
    {
        this.weapon = weapon;
        this.gameObject = gameObject;
        this.stats = stats;
        this.firePoint = firePoint;
        this.animator = animator;
    }

    public void Enter()
    {

    }

    public void Execute()
    {
        CheckTargetDirection();
        AdjustFirePoint();
        ChangeAnimations();
        weapon.Shoot();
    }

    public void Exit()
    {

    }

    void CheckTargetDirection()
    {
        if (Character.selectedCharacter.transform.position.x < gameObject.transform.position.x)
        {
            direction = targetDirection.left;
        }
        else
        {
            direction = targetDirection.right;
        }
    }

    void AdjustFirePoint()
    {
        if(direction == targetDirection.left)
        {
            firePoint.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            firePoint.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void ChangeAnimations()
    {
        if (direction == targetDirection.left)
        {
            animator.SetFloat("changeX", -1);
        }
        else
        {
            animator.SetFloat("changeX", 1);
        }
    }

    private enum targetDirection
    {
        left,
        right
    }
}
