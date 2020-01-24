using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    public GameObject prefab;

    void Start()
    {
        int loop = 0;

		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

		foreach (GameObject obj in players)
        {
            GameObject spawn = Instantiate(prefab);
            spawn.transform.SetParent(transform.GetChild(0));
           
            Sprite s = obj.GetComponent<SpriteRenderer>().sprite;
            transform.GetChild(0).GetChild(loop).GetComponent<CharacterSlot>().AddCharacter(obj.GetComponent<Character>());
            transform.GetChild(0).GetChild(loop).GetComponent<CharacterSlot>().setSprite(s);
            loop++;
        }

		transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(100 , players.Length * 100);
    }
}
