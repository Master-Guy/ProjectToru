using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class PoliceState : IState
{
	private static LinkedList<Room> LastKnownPositions = new LinkedList<Room>();
	public virtual void AddPosition(Room room)
	{
		LastKnownPositions.AddFirst(room);
	}
	public abstract void Enter();
	public abstract void Execute();
	public abstract void Exit();
	public abstract void MoveCop(Police p);
}