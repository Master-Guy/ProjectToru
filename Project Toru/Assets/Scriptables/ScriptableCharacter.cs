using Assets.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scriptables
{
	[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
	public class ScriptableCharacter : ScriptableObject
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

		public IEnumerable<Item> inventory;

		public ScriptableCharacter()
		{
			this.inventory = new List<Item>();
			this.state = State.none;
			this.trait = Trait.none;
			this.skill = Skill.none;
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



