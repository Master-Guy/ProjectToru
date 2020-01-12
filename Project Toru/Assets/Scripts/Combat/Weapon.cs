using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public GameObject bullet;

    public float damage = 10;
    public float RoundsPerMinute = 300;
    private float Timer = 0;

    private void Start()
    {
        bullet.GetComponent<Bullet>().weapon = this;
    }

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
            Timer = 60 / RoundsPerMinute;
            Instantiate(bullet, transform.position, transform.rotation);
        }
	}
}
