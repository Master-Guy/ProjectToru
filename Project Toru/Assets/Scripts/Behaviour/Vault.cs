using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.Behaviour;

public class Vault : Furniture
{
	public GameObject money = null;
	public LevelScript levelScript = null;

    [SerializeField]
    public CardreaderColor keycardColor = CardreaderColor.Disabled;
    SpriteRenderer ColorIndicator = null;

    bool closed = true;


    void Start()
    {
        foreach(Component comp in GetComponentsInChildren<SpriteRenderer>())
        {
            if (comp.name == "Color Indicator")
            {
                ColorIndicator = (SpriteRenderer)comp;
            }
        }
        UpdateColor();
    }

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

    private void UpdateColor()
    {
        // Set color indicator
        switch (keycardColor)
        {
            case CardreaderColor.Blue:
                ColorIndicator.color = ColorZughy.cyan;
                break;
            case CardreaderColor.Purple:
                ColorIndicator.color = ColorZughy.purple;
                break;
            case CardreaderColor.Yellow:
                ColorIndicator.color = ColorZughy.yellow;
                break;
            default:
                ColorIndicator.color = ColorZughy.grey;
                break;
        }
    }
}