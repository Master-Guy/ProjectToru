using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float damage;
    public float maxHealth;
    [NonSerialized]
    public float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Destroy(this.gameObject);
			Debug.Log(gameObject.name + " has died");
        }

        Debug.Log(gameObject.name + " has " + currentHealth + " health.");
    }
}
