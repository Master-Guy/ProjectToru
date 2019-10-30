using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Options
{
	class getInformation : Option
	{
		getInformation() : base("search for information") {	}
		public getInformation(string description) : base(description) { }
	}
}
