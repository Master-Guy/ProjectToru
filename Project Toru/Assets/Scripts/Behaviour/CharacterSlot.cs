using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    public Sprite icon;
    public Character character;

    public void AddCharacter(Character newCharacter)
    {
        character = newCharacter;
    }

    public void setSprite(Sprite s)
    {
        this.icon = s;
        transform.GetComponent<Image>().sprite = this.icon;
        transform.GetComponent<Mask>().showMaskGraphic = true;
        transform.GetChild(0).GetComponent<Button>().interactable = true;

    }

    public void SelectCharacter()
    {
        Character.selectedCharacter = character;
    }
}