using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class Defensive : PoliceState
{
	static Defensive instance;
	public static Defensive getInstance()
	{
		if (instance == null)
			instance = new Defensive();
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
		entrances.OrderBy(o => o.getNPCsInRoom().Where(npc => npc.GetComponent<Police>() != null).Count());
		// TODO set p's destination to most understaffed entrance
	}
}
