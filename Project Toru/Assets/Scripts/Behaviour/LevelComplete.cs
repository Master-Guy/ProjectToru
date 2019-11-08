using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{

	bool won = false;

	private void Update()
	{
		if(won)
		{
			GetComponent<Rigidbody2D>().MovePosition(new Vector2(transform.position.x + -0.1f, transform.position.y));
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		Character ch = collision.GetComponent<Character>();

		if (collision.isTrigger && ch != null)
		{
			if (ch.inventory.getMoney() > 0)
			{
				collision.gameObject.SetActive(false);
				won = true;
				Invoke("sceneSwither", 3);
			}
		}
	}

	void sceneSwither()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
