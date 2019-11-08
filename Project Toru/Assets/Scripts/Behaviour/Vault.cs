using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vault : MonoBehaviour
{
    [SerializeField]
    Collider2D collider = null;

    bool closed = true;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            if (collision.gameObject.GetComponent<Character>() && closed)
            {
                Open();
            }
    }

    public bool Open()
    {
        Debug.Log("You open the vault and take the gold.");
        closed = false;
        GetComponent<Animator>().SetBool("OpenVault", true);

        StartCoroutine(WaitForAnimationEndTimer());
        return true;
    }

    IEnumerator WaitForAnimationEndTimer()
    {
        yield return new WaitForSeconds(0.5f);
        collider.enabled = false;
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
