using Assets.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scriptables
{
	[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
	public class CharacterInformation : ScriptableObject
	{
		public new string name;
		public Sprite sprite;

		public Gender gender;
		public State state;
		public Trait trait;
		public Skill skill;

		public float strength;
		public float intimidation;
		public float charm;
		public float weaponHandling;

		// private IEnumerable<Item> inventory;
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



