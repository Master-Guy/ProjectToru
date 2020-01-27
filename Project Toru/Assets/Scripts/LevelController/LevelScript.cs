using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
	
	protected DialogueManager dialogueManager = null;
	public PoliceSirenOverlay PoliceSiren = null;
	public PoliceCar PoliceCar = null;
	
	void Awake() {
		dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
	}
}
