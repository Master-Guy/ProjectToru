using Assets.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


static class selectedCharacter
{
	public static CharacterBehaviour selected;
}

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class CharacterOld : Drawable
{
	public new string name;
	public Sprite sprite;

	public Gender gender;
	public State state = State.none;
	public Trait trait = Trait.none;
	public Skill skill = Skill.none;

	public float strength;
	public float intimidation;
	public float charm;
	public float weaponHandling;

	public IEnumerable<Item> inventory;
	public Room location;

	public CharacterBehaviour character;

	// TODO fix
	public CharacterOld(string sprite, Room startLocation) : base(sprite)
	{
		this.inventory = new List<Item>();
		this.character = new CharacterBehaviour(this);
		this.location = startLocation;
	}

	public override void Draw()
	{
		throw new NotImplementedException();
	}

	public void move(Room destination)
	{
		List<Room> route = new List<Room>();
		if (location.CalculatePath(destination, route).Count > 0)
		{
			// TODO move
		}
		else
		{
			return;
		}
	}
}

public enum Gender
{
	Male,
	Female
}

public enum State
{
	none,
	Guilty,
	In_love,
	Griefing
}

public enum Trait
{
	none,
	Charming,
	Quickly_guilty
}

public enum Skill
{
	none,
	Hacker,
	Weapons_specialists
}




