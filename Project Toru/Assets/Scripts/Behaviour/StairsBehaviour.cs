using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class StairsBehaviour : MonoBehaviour
{

	public StairsBehaviour Upstair = null;
	public StairsBehaviour Downstairs = null;

	[SerializeField]
	StairsCollission GoUpStairs = null;

	[SerializeField]
	StairsCollission GoDownStairs = null;

	void Start()
	{

		// Hide stairs when floor is not connected
		if (Upstair == null && GoUpStairs != null)
		{
			GoUpStairs?.Disable();
		}

		if (Downstairs == null && GoUpStairs != null)
		{
			GoDownStairs?.Disable();
		}
	}

	/// <summary>
	/// Returns target for Go Up movement
	/// </summary>
	/// <returns>Target set by GoDown</returns>
	public Transform GetUpTarget()
	{

		// If GoUpstairs is not set, return null
		if (GoDownStairs == null) return null;

		// Find PositionTarget
		// We need to get the Downstairs target, because the character must end there.
		return GoDownStairs?.GetTarget();
	}

	/// <summary>
	/// Returns target for Go Down movement
	/// </summary>
	/// <returns>Target set by GoUp</returns>
	public Transform GetDownTarget()
	{

		// If GoUpstairs is not set, return null
		if (GoUpStairs == null) return null;

		// Find PositionTarget
		// We need to get the Downstairs target, because the character must end there.
		return GoUpStairs?.GetTarget();
	}

	/// <summary>
	/// Receives message from collider script in stairs
	/// Gets target and sends information to character
	/// </summary>
	/// <param name="GoUp">True when movement is up direction, false when down direction</param>
	/// <param name="collider">The character collider</param>
	public void UseStairs(bool GoUp, Collider2D collider)
	{

		Transform target = (GoUp ? Upstair?.GetUpTarget() : Downstairs?.GetDownTarget());

		if (target == null)
		{
			Debug.LogWarning("Stair Target is not set, illegal direction");
			return;
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
			character.StairsTransistion();
		}
	}
}