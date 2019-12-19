using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Van : MonoBehaviour
{

    List<Character> characters = new List<Character>();

    public Collider2D carCollider = null;

    public bool drive = false;

    // Start is called before the first frame update
    void Start()
    {
        LevelConditionInt condition = new LevelConditionInt();
        condition.name = "AllCharactersMustBeInVan";
        condition.targetValue = 1;
        condition.fullfillHandler = (LevelCondition c) =>
        {
            // Remove collider, when RigidBody bodytype is Dynamic, the collider is interacting with the game boundary
            carCollider?.gameObject.SetActive(false);

            // RigidBody is static by default, to prevent Van from moving by character
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

            // Move Van
            GetComponent<Van>().drive = true;

            LevelManager.EndLevel("You got away", "But the idea is that you steal some money...", 3);


            //Invoke("sceneSwitherWin", 3);
        };


        LevelManager.AddCondition(condition);

    }

    // Update is called once per frame
    void Update()
    {
        if (drive)
        {
            GetComponent<Rigidbody2D>().MovePosition(new Vector2(transform.position.x + -0.1f, transform.position.y));
        }
    }

    public void EnterCharacter(Character character)
    {
        characters.Add(character);
        character.gameObject.SetActive(false);

        if (1 == characters.Count)
        {
            LevelManager.Condition("AllCharactersMustBeInVan").Fullfill();
        }
    }

    public void ExitCharacter(Character character)
    {
        characters.Remove(character);
        character.gameObject.SetActive(true);
    }
}
