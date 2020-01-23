using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Police : Guard
{
	public void Update()
	{
		if (currentRoom.charactersInRoom.Count > 0)
		{
			GetComponent<ExecutePathFindingNPC>().StopPathFinding();
			// TODO fight

		}
		else if(GetComponent<ExecutePathFindingNPC>().path.Count == 0)
		{
			PoliceForce.getInstance().RequestOrders(this);
		}
	}

	public void setPos(Room dest)
	{
		GetComponent<ExecutePathFindingNPC>().setPosTarget(dest.GetPosition());
	}
}
