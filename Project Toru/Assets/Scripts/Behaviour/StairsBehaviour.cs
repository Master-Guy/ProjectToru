using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class StairsBehaviour : MonoBehaviour
{

	public StairsBehaviour Upstair = null;
	public StairsBehaviour Downstairs = null;

	[SerializeField]
	GameObject GoUpStairs = null;

	[SerializeField]
	GameObject GoDownStairs = null;

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

	public Transform GetUpTarget()
	{

		Debug.Log("1");
		if (GoUpStairs == null) return null;
		Debug.Log("2");
		Transform[] objects = GoUpStairs.GetComponentsInChildren<Transform>();
		Debug.Log("3");
		foreach (Transform transformObject in objects)
		{
			Debug.Log("4");
			if (transformObject.tag == "PositionTarget")
			{
				Debug.Log("5");
				return transformObject;
			}
		}
		Debug.Log("6");
		return null;
	}

	public Transform GetDownTarget()
	{

		if (GoDownStairs == null) return null;

		return Downstairs.GetComponent(typeof(Transform)) as Transform;
	}


	public void UseStairs(bool GoUp, Collider2D collider)
	{

		Transform target = (GoUp ? Upstair.GetUpTarget() : Downstairs.GetDownTarget());

		if (target == null)
		{
			Debug.LogError("Stair Target is not set, illegal direction");
		}

		// Move character
		collider.transform.position = target.position;

		// Let character know it is using a stairs
		// Get GameObject from collider
		GameObject gameobject = collider.gameObject;

		// Check if this gameobject has an script Character
		Character character = (Character)gameobject.GetComponent(typeof(Character));

		if (character != null)
		{
			Debug.Log("Character is using stairs");
			character.StairsTransistion();
		}
	}
}