using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class StairsBehaviour : MonoBehaviour
{

	public StairsBehaviour Upstair = null;
	public StairsBehaviour Downstairs = null;

	[SerializeField]
	GameObject GoUpStairs;

	[SerializeField]
	GameObject GoDownStairs;

	void Start()
	{
		if (Upstair == null)
		{
			GoUpStairs.SetActive(false);
		}

		if (Downstairs == null)
		{
			GoDownStairs.SetActive(false);
		}
	}

}