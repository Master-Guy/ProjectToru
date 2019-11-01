using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Options
{
	public abstract class Option
	{
		private readonly string Description;
		public Skills? Prerequisite = null;

		public Option(string desc)
		{
			Description = desc;
		}

		public string getInfo()
		{
			return Description;
		}

		public abstract void Activate(Character c);
	}
}
