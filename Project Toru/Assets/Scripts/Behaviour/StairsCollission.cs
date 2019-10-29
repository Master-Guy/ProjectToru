using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StairsCollission : MonoBehaviour
{
	[SerializeField]
	StairsBehaviour stairsBehaviour;

	[SerializeField]
	bool DirectionIsUp = false;

	[SerializeField]
	Transform target = null;

	[SerializeField]
	TilemapRenderer tilemapRenderer = null;

	[SerializeField]
	Collider2D DisabledStairsBarrier = null;

	void OnTriggerEnter2D(Collider2D other)
	{
		stairsBehaviour.UseStairs(DirectionIsUp, other);
	}

	public void Enable()
	{
		tilemapRenderer.enabled = true;
	}

	public void Disable()
	{
		tilemapRenderer.enabled = false;
	}

	public Transform GetTarget()
	{
		return target;
	}
}
