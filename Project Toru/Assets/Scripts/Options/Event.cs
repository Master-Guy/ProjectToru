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
		public List<Option> Options = new List<Option>();

		public Event(string s, GameObject g, List<Option> o)
		{
			Description = s;
			Object = g;
			Options = o;
		}

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
	}
}