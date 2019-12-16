using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameAnalyticsSDK;

public class Vault : MonoBehaviour
{
    [SerializeField]
    Collider2D vaultCollider = null;

    public Door door = null;
    public GameObject money = null;

    bool closed = true;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            if (collision.gameObject.GetComponent<Character>() && closed)
            {
                Open();
                collision.gameObject.GetComponent<Character>().inventory.addItem(money.GetComponent<Money>());
            }
    }

    public bool Open()
    {
        Debug.Log("You open the vault and take the gold.");
        GameAnalytics.NewDesignEvent("VaultOpened");
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "NewStairs");

        closed = false;
        GetComponent<Animator>().SetBool("OpenVault", true);

        door.Close();

        StartCoroutine(WaitForAnimationEndTimer());
        return true;
    }

    IEnumerator WaitForAnimationEndTimer()
    {
        yield return new WaitForSeconds(0.5f);
        vaultCollider.enabled = false;
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
