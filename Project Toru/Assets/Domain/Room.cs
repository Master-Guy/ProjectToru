using System;
using System.Collections.Generic;

namespace Assets.Domain
{
	interface Room
	{
		// Variables //
		int posX
		{
			get;
			set;
		}
		int posY
		{
			get;
			set;
		}

		string background
		{
			get;
			set;
		}
		bool uncovered
		{
			get;
			set;
		}
		ref Character[] characters
		{
			get;
		}
		string name
		{
			get;
			set;
		}
		Dictionary<Object, short> Objects
		{
			get;
			set;
		}

		// Functions //
		int draw();
		Dictionary<string, Delegate> getOptions();
	}
}
