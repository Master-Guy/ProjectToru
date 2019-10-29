using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsCollission : MonoBehaviour
{
	[SerializeField]
	private StairsBehaviour stairsBehaviour;

	[SerializeField]
	private bool DirectionIsUp = false;



	void OnTriggerEnter2D(Collider2D other)
	{
		stairsBehaviour.UseStairs(DirectionIsUp, other);
	}
}
