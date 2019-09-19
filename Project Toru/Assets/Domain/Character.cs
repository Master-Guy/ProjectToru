using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Domain
{
    interface Character
    {
		Dictionary<string, string> sprite { get; set; }
		string name { get; set; }
		Dictionary<string, float> stats { get; set; }
		Skill[] skills { get; set; }
		Dictionary<int, Delegate> getOptions();
		int getSalaries();
    }

    enum gender
    {
		male,
		female,
		apache_helicopter
    }

	enum state
	{
		guilty,
		in_love,
		griefing
	}

	enum trait
	{
		charming,
		quickly_guilty
	}
}

