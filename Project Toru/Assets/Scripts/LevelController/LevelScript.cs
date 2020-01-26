using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class LevelScript : MonoBehaviour
{
	protected DialogueManager dialogueManager = null;
	public PoliceSirenOverlay PoliceSiren = null;
	public List<PoliceCar> PoliceCars = new List<PoliceCar>();
	
	int PoliceCarsSpawned = 0;
	
	protected void SpawnPoliceCar() {
		if (PoliceCarsSpawned >= PoliceCars.Count) {
			return;
		}
		
		PoliceSiren.Activate();
		
		PoliceCars[PoliceCarsSpawned].Drive();
		PoliceCarsSpawned++;
	}
	
	protected virtual void Awake() {
		LevelManager.setLevel();
		dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
	}
	
	void Update() {
		if (Input.GetKeyDown(KeyCode.O))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
	}
}
