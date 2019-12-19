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

    public void Start()
    {
        {
            LevelCondition condition = new LevelCondition();
            condition.name = "CharacterMustHaveMoney";
            condition.required = true;

            LevelManager.AddCondition(condition);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            if (collision.gameObject.GetComponent<Character>() && closed)
            {
                Open();
                collision.gameObject.GetComponent<Character>().inventory.addItem(money.GetComponent<Money>());

                if (collision.gameObject.GetComponent<Character>().HasKey(CardReader.CardreaderColor.Blue))
                {
                    LevelManager.Condition("CharacterMustHaveKeyInVaultRoom").Fullfill();
                }
                else
                {
                    LevelManager.Condition("CharacterMustHaveKeyInVaultRoom").Fail();
                }
            }
    }

    public bool Open()
    {
        Debug.Log("You open the vault and take the gold.");

        LevelManager.Condition("CharacterMustHaveMoney").Fullfill();

        GameAnalytics.NewDesignEvent("VaultOpened");
        //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "NewStairs");

        closed = false;
        GetComponent<Animator>().SetBool("OpenVault", true);


        {
            LevelCondition condition = new LevelCondition();
            condition.name = "CharacterMustHaveKeyInVaultRoom";
            condition.required = true;

            condition.failHandler = (LevelCondition c) =>
            {
                Debug.Log("Player got stuck");
                LevelManager.FinishLevel();
            };

            LevelManager.AddCondition(condition);
        }



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
