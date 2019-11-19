using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{

	public GameObject copsSpawnPoint;
	public GameObject copsPrefab;

	private List<GameObject> copsList = new List<GameObject>();

	bool won = false;
	bool lose = false;
	bool BlockFan = false;

	private void Update()
	{
		if (won || BlockFan)
		{
			GetComponent<Rigidbody2D>().MovePosition(new Vector2(transform.position.x + -0.1f, transform.position.y));
		}

		if (lose)
		{
			GameObject obj = Instantiate(copsPrefab, new Vector3(copsSpawnPoint.transform.position.x - 0.5f, copsSpawnPoint.transform.position.y - 0.5f, 0), Quaternion.identity);
			copsList.Add(obj);
			lose = false;
			Invoke("sceneSwitcherLose", 5);
			BlockFan = true;
		}

		foreach (GameObject move in copsList)
		{
			move.GetComponent<Rigidbody2D>().MovePosition(new Vector2(move.transform.position.x + 0.1f, move.transform.position.y));
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
				Invoke("sceneSwitherWin", 3);
			}
		}
	}

	void sceneSwitherWin()
	{
		SceneManager.LoadScene("Complete");
	}

	void sceneSwitcherLose()
	{
		SceneManager.LoadScene("Fail");
	}

	public void EnableLose()
	{
		lose = true;
	}
}
