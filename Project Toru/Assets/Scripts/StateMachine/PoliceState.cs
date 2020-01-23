using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class PoliceState : IState
{
	protected static LinkedList<Room> LastKnownPositions = new LinkedList<Room>();
	protected static List<Room> entrances;

	public PoliceState()
	{
		if(entrances == null)
		{
			entrances = new List<Room>();
			// get entrances from building
		}
	}
	public virtual void AddPosition(Room room)
	{
		LastKnownPositions.AddFirst(room);
	}
	public abstract void Enter();
	public abstract void Execute();
	public abstract void Exit();
	public abstract void MoveCop(Police p);
}