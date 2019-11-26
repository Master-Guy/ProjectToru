using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float speed = 20;
	public Rigidbody2D rb;

    void Start()
    {
		rb.velocity = transform.right * speed;
		Invoke("DestroyObject", 1);
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("NPC"))
		{
			other.GetComponent<CharacterStats>().TakeDamage(20);
			DestroyObject();
		}
		if (other.CompareTag("Walls"))
		{
			DestroyObject();
		}
	}
	
	private void DestroyObject()
	{
		Destroy(this.gameObject);
	}
}
