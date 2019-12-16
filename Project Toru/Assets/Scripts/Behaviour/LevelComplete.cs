using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{

    public GameObject copsSpawnPoint = null;
    public GameObject copsPrefab = null;

    public Collider2D rigidbodyCollider = null;

    private List<GameObject> copsList = new List<GameObject>();

    bool won = false;
    bool lose = false;
    bool BlockFan = false;

    void Start()
    {
        {
            LevelCondition condition = new LevelCondition();
            condition.name = "CharacterMustEnterVan";
            condition.required = true;



            LevelDirector.Instance().AddCondition(condition);
        }

        {
            LevelCondition condition = new LevelCondition();
            condition.name = "CharacterMustHaveMoney";
            condition.required = true;

            LevelDirector.Instance().AddCondition(condition);
        }
    }

    void Update()
    {
        if (won || BlockFan)
        {

            // Remove collider, when RigidBody bodytype is Dynamic, the collider is interacting with the game boundary
            rigidbodyCollider.enabled = false;

            // RigidBody is static by default, to prevent Van from moving by character
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

            // Move Van
            GetComponent<Rigidbody2D>().MovePosition(new Vector2(transform.position.x + -0.1f, transform.position.y));
            GetComponent<LevelManager>().LevelEndSuccess();
        }

        if (lose)
        {
            GameObject obj = Instantiate(copsPrefab, new Vector3(copsSpawnPoint.transform.position.x - 0.5f, copsSpawnPoint.transform.position.y - 0.5f, 0), Quaternion.identity);
            copsList.Add(obj);
            lose = false;
            Invoke("sceneSwitcherLose", 5);
            BlockFan = true;

            GetComponent<LevelManager>().LevelEndFail();
        }

        foreach (GameObject move in copsList)
        {
            move.GetComponent<Rigidbody2D>().MovePosition(new Vector2(move.transform.position.x + 0.1f, move.transform.position.y));
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Character ch = collision.GetComponent<Character>();


        if (collision.isTrigger && ch != null)
        {
            LevelDirector.Instance().Condition("CharacterMustEnterVan").Fullfill();


            if (ch.inventory.getMoney() > 0)
            {
                LevelDirector.Instance().Condition("CharacterMustHaveMoney").Fullfill();

                //collision.gameObject.SetActive(false);
                //won = true;
                //Invoke("sceneSwitherWin", 3);
            }

            LevelDirector.Instance().FinishLevel();
        }
    }

    void sceneSwitherWin()
    {
        SceneManager.LoadScene("Complete");
    }

    void sceneSwitcherLose()
    {
        SceneManager.LoadScene("Fail");
    }

    public void EnableLose()
    {
        lose = true;
    }
}
