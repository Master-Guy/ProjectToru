using UnityEngine;

public class PingPong : IState
{
	private Vector3 startPos;
	private Vector3 change;
	private GameObject gameObject;
	private Animator animator;


	public PingPong(Vector3 startPos, GameObject gameObject, Animator animator)
	{
		this.startPos = startPos;
		this.gameObject = gameObject;
		this.animator = animator;
	}

	public void Enter()
	{
		
	}

	public void Execute()
	{
		var trans = gameObject.GetComponent<Transform>();
		change = new Vector3(Mathf.PingPong(Time.time, 1.5f) + startPos.x, trans.position.y, trans.position.z);

		if(trans.position != change)
		{
			animator.SetBool("isMoving", true);
			animator.SetFloat("changeX", change.x - trans.position.x);
			animator.SetFloat("changeY", change.y - trans.position.y);
			trans.position = change;
		}

	}

	public void Exit()
	{
		animator.SetBool("isMoving", false);
		animator.SetFloat("changeX", 0);
		animator.SetFloat("changeY", 0);
	}
}
