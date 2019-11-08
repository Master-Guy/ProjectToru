using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Options
{
	class getInformation : Option
	{
		public override void Activate(Character c)
		{
			// TODO gather information
			Debug.Log("information found by " + c.name);
		}
	}
}
