using Assets.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scriptables
{
	static class selectedCharacter
	{
		public static Character selected; 
	}

	[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
	public class ScriptableCharacter : ScriptableObject
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
		public ScriptableRoom location;

		public Assets.Domain.Character character;

		public ScriptableCharacter(ScriptableRoom startLocation)
		{
			this.inventory = new List<Item>();
			this.character = new Character("hallo", this);
			this.location = startLocation;
		}

		public void move(ScriptableRoom destination)
		{
			List<ScriptableRoom> route = new List<ScriptableRoom>();
			if (location.CalculatePath(destination, route).Count > 0)
			{
				// TODO move
			} else
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
}



