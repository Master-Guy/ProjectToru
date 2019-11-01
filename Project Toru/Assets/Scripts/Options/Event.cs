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
		public string Description;
		public GameObject Object;
		public List<Character> Actors = new List<Character>();
		public List<Option> Options;

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

		public List<Option> GetOptions()
		{
			// return a list with options that have no prerequisite
			return Options.Where(x => x.Prerequisite == null ||
					// or where there is at least one actor that has the required skill
					Actors.Where(a => a.skills.Contains(x.Prerequisite.Value)).Count() != 0).ToList();
		}

		public string GetText(out List<Option> o)
		{
			Debug.Log(Actors.Count);
			string temp = Actors[0].name;
			for(int i = 1; i < Actors.Count - 1; i++)
			{
				temp += ", " + Actors[i].name;
			}
			if (Actors.Count > 1)
				temp += " and " + Actors[Actors.Count].name;
			temp += " " + Description + System.Environment.NewLine;

			o = GetOptions();

			foreach (var option in o)
			{
				temp += "<link>[";
				if (option.Prerequisite == null)
					temp += "anyone";
				else
					foreach (Character c in Actors)
						if (c.skills.Contains(option.Prerequisite.Value))
							temp += c.name + " ";
				temp += "] " + option.getInfo() + "</link>" + System.Environment.NewLine;
			}
			return temp;
		}
	}
}