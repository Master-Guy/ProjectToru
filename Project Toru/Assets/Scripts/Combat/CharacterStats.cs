using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
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
            NPC npc = this.gameObject.GetComponent<NPC>();
            if(npc != null)
            {
                npc.dropBag();
            }
			
			LevelManager.emit("Killed", gameObject);
            gameObject.SetActive(false);
        }
    }
}
