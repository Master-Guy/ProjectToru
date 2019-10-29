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

	//public Transform GetUpTarget()
	//{

	//}

	//public Transform GetDownTarget()
	//{

	//}


	public void UseStairs(bool GoUp, Collider2D character)
	{
		Debug.Log("UseStairs");

		//// Move character
		//other.transform.position = target.position;

		//// Let character know it is using a stairs
		//// Get GameObject from collider
		//GameObject gameobject = other.gameObject;

		//// Check if this gameobject has an script Character
		//Character character = (Character)gameobject.GetComponent(typeof(Character));

		//if (character != null)
		//{
		//	Debug.Log("Character is using stairs");
		//	character.StairsTransistion();
		//}
	}
}