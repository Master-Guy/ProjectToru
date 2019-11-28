using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    List<GameObject> players;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transform.GetChild(0).name);
        int loop = 0;
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
        {
            Sprite s = obj.GetComponent<SpriteRenderer>().sprite;
            transform.GetChild(0).GetChild(loop).GetComponent<CharacterSlot>().AddCharacter(obj.GetComponent<Character>());
            transform.GetChild(0).GetChild(loop).GetComponent<CharacterSlot>().setSprite(s);
            transform.GetChild(0).GetChild(loop).GetComponent<CharacterSlot>().SelectCharacter();
            loop++;
        }

        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateUI()
    {
        
    }
}
