using System;
using System.Collections.Generic;

namespace Assets.Domain
{
	interface Furniture
	{
		string name
		{
			get;
			set;
		}

		string sprite
		{
			get;
			set;
		}

		Item[] items
		{
			get;
			set;
		}

		bool uncovered
		{
			get;
			set;
		}

		Dictionary<string, Delegate> options
		{
			get;
			set;
		}
		void draw();
		Dictionary<string, Delegate> getOptions();
	}
}
