using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Options
{
	public class Event
	{
		string Description;
		GameObject Object;
		List<Character> Actors = new List<Character>();
		List<Character> ActorShortList = new List<Character>();
		List<Option> Options;
		List<Option> OptionShortList = new List<Option>();

		public Event(string s, GameObject g, List<Option> o, Character c)
		{
			Description = s;
			Object = g;
			Options = o;
			Actors.Add(c);
		}

		/// <summary>
		/// returns true if events were merged
		/// </summary>
		/// <param name="E"></param>
		/// <returns></returns>
		public bool Merge(Event E)
		{
			if (E.Object != Object)
				return false;
			foreach (var a in E.Actors)
				if (!Actors.Contains(a))
					Actors.Add(a);
			// this second loop can be left out if options don't change for objects
			foreach (var a in E.Options)
				if (!Options.Contains(a))
					Options.Add(a);
			return true;
		}

		/// <summary>
		/// return the amount of actors left, -1 if the gameobject does not match
		/// </summary>
		/// <param name="E"></param>
		/// <returns></returns>
		public int Remove(GameObject g, Character c)
		{
			if (g != Object)
				return -1;
			Actors.Remove(c);
			return Actors.Count;
		}

		private void BuildOptionShortList()
		{
			// return a list with options that have no prerequisite
			OptionShortList = Options.Where(x => x.Prerequisite == null ||
					// or where there is at least one actor that has the required skill
					Actors.Where(a => a.skills.Contains(x.Prerequisite.Value)).Count() != 0).ToList();
		}

		private void BuildActorShortList(Option o)
		{
			if (o.Prerequisite == null)
				ActorShortList = Actors;
			else
				ActorShortList = Actors.Where(x => x.skills.Contains(o.Prerequisite.Value)).ToList();
		}

		public string GetOptionText()
		{
			string temp = Actors[0].name;
			for(int i = 1; i < Actors.Count - 1; i++)
			{
				temp += ", " + Actors[i].name;
			}
			if (Actors.Count > 1)
				temp += " and " + Actors[Actors.Count - 1].name;
			temp += " " + Description + System.Environment.NewLine;

			BuildOptionShortList();

			foreach (var option in OptionShortList)
			{
				temp += "<link>[";
				foreach (Character c in Actors)
					if (option.Prerequisite == null || c.skills.Contains(option.Prerequisite.Value))
						temp += c.name + " ";
				temp += "] " + option.getInfo() + "</link>" + System.Environment.NewLine;
			}
			return temp;
		}

		public string GetActorText()
		{
			string text = "Who will perform this action" + System.Environment.NewLine;
			foreach(var a in ActorShortList)
				text += "<link>" + a.name + "</link>" + System.Environment.NewLine;
			return text;
		}

		/// <summary>
		/// returns true if an option was activated, false if an actor has to be selected
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public bool ActivateOption(int index)
		{
			BuildActorShortList(OptionShortList[index]);
			if (ActorShortList.Count > 1)
				return false;
			OptionShortList[index].Activate(ActorShortList[0]);
			return true;
		}

		public void ActivateOption(int indexOption, int indexCharacter)
		{
			OptionShortList[indexOption].Activate(ActorShortList[indexCharacter]);
		}
	}
}