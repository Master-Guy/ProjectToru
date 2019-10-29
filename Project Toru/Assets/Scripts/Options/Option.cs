﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Options
{
	public class Option
	{
		private readonly string Description;
		public Skills? Prerequisite = null;

		Option(string desc)
		{
			Description = desc;
		}

		public string getInfo()
		{
			return Description;
		}
	}
}
