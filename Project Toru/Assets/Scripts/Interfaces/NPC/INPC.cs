using UnityEngine;

public interface INPC
{
	void Idle();
	void Surrender();
	void Defend();
	void Flee();
	void CallForHelp();
	GameObject GetCurrentRoom();
}
