using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class LevelScript : MonoBehaviour
{
	protected DialogueManager dialogueManager = null;
	public PoliceSirenOverlay PoliceSiren = null;
	private Queue<PoliceCar> _PoliceCars = new Queue<PoliceCar>();
	public List<PoliceCar> PoliceCars = new List<PoliceCar>();

	
	protected void SpawnPoliceCar() {
		if (_PoliceCars.Count == 0) {
			return;
		}
		
		PoliceSiren.Activate();
		
		LevelManager.Delay(Random.Range(5, 10), () => {
			try {
				_PoliceCars.Dequeue().Drive();
			} catch {
				// Queue is empty
			}
		});
		
		LevelManager.Condition("CopsTriggered")?.Fullfill();
	}
	
	protected virtual void Awake() {
		LevelManager.setLevel();
		dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
		
		foreach(var policecar in PoliceCars) {
			_PoliceCars.Enqueue(policecar);
		}
	}
	
	void Update() {
		if (Input.GetKeyDown(KeyCode.O))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
	}
}
