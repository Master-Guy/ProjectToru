using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{

	[SerializeField]
	GameObject LeftWall;

	[SerializeField]
	GameObject RightWall;

	public void EnableLeftWall(bool enable)
	{
		LeftWall.SetActive(enable);
	}

	public void EnableRightWall(bool enable)
	{
		RightWall.SetActive(enable);
	}
}
