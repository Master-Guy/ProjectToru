using System;
using System.Collections.Generic;

namespace Assets.Domain
{
	interface FurnitureInterfaceOld
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

		ItemInterfaceOld[] items
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
