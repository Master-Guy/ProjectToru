using UnityEngine;

public class Flee : IState
{
	private GameObject[] path;
	private Vector3 change;
	private GameObject gameObject;
	private Animator animator;
	private Transform trans;
	private int arrayNumber = 0;
	private bool endReached = false;

	public Flee(GameObject[] path,GameObject gameObject, Animator animator)
	{
		this.path = path;
		this.gameObject = gameObject;
		this.animator = animator;
	}

	public void Enter()
	{
		trans = gameObject.GetComponent<Transform>();
	}

	public void Execute()
	{
		if(arrayNumber == path.Length)
		{
			endReached = true;
			animator.SetBool("isMoving", false);
			animator.SetFloat("changeX", 0);
			animator.SetFloat("changeY", 0);

			if(path.Length > 0 && path[path.Length-1].name == "Despawn")
			{
				gameObject.SetActive(false);
			}
		}

		if (!endReached && path.Length > 0)
		{
			change = Vector3.MoveTowards(trans.position, path[arrayNumber].transform.position, Time.deltaTime * 3.5f);

			if (trans.position != change)
			{
				animator.SetBool("isMoving", true);
				animator.SetFloat("changeX", change.x - trans.position.x);
				animator.SetFloat("changeY", change.y - trans.position.y);
				trans.position = change;
			}

			if (trans.position == path[arrayNumber].transform.position)
			{
				arrayNumber++;
			}
		}
	}

	public void Exit()
	{

	}
}
