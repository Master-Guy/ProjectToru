using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class Aggressive : PoliceState
{
	static Aggressive instance;
	public static Aggressive getInstance()
	{
		if (instance == null)
			instance = new Aggressive();
		return instance;
	}

	public override void Enter()
	{
	}

	public override void Execute()
	{
	}

	public override void Exit()
	{
	}

	public override void MoveCop(Police p)
	{
		// TODO set p's destination last known position
	}
}
