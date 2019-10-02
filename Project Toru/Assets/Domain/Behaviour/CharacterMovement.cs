using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
	public float speed;
	private Rigidbody2D myRigidbody;
	private Vector3 change;

	private Vector3 currentLocation;
	private Vector3 targetLocation;

    // Start is called before the first frame update
    void Start()
    {
		myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		change = Vector3.zero;
		change.x = Input.GetAxisRaw("Horizontal"); // Horizontal input (x)
		change.y = Input.GetAxisRaw("Vertical"); // Vertical input (y)

		MoveCharacter();
		MoveCharacterWithMouse();
	}

	void MoveCharacter()
	{
		change.Normalize();
		myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
	}

	void MoveCharacterWithMouse()
	{
		if (Input.GetMouseButtonDown(0))
		{
			currentLocation = transform.position;
			targetLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			targetLocation.z = 1;
		}

		if (currentLocation != targetLocation)
		{
			transform.position = Vector3.Lerp(currentLocation, targetLocation, speed * Time.deltaTime);
			currentLocation = transform.position;
		}
	}
}
