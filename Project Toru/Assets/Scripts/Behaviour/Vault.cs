using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.Behaviour;

public class Vault : Furniture
{
	public GameObject money = null;
	public LevelScript levelScript = null;

    bool closed = true;

    /*void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<Character>() && closed)
            {
				
                Open();
                collision.gameObject.GetComponent<Character>().inventory.addItem(money.GetComponent<Money>());
            }
        }
    }*/

    public bool Open()
    {
        closed = false;
        GetComponent<Animator>().SetBool("OpenVault", true);

		levelScript?.emit("vault_open");
        // door.Close();

        StartCoroutine(WaitForAnimationEndTimer());
        return true;
    }

    IEnumerator WaitForAnimationEndTimer()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public bool IsOpen()
    {
        return !closed;
    }

    public bool IsClosed()
    {
        return closed;
    }
}