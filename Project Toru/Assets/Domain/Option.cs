using UnityEngine;
using System.Collections;

namespace Assets.Domain
{
	interface Option
	{

		string description { get; set; }
		//Dictionary<Skill, float>; implementeren met skill
		//implementeren met stat

		void execute();


	}

}