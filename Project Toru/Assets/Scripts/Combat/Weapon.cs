using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public Transform firePoint;
	public GameObject bullet;

    public float RateOfFire;
    private float Timer = 0;

    private void Update()
    {
        if(Timer > 0)
        {
            Timer -= Time.deltaTime;
        }
    }

    public void Shoot()
	{
        if(Timer <= 0)
        {
            Timer = RateOfFire;
            Instantiate(bullet, firePoint.position, firePoint.rotation);
        }
	}
}
