using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
	public Text nameText, dialogueText;
	
	[SerializeField]
	private Animator animator = null;

	private Queue<string> sentences = new Queue<string>();

	void Start()
	{
		animator = GetComponent<Animator>();
	}

	public void StartDialogue(DialogueText dialogue)
	{
		animator.SetBool("IsOpen", true);

		nameText.text = dialogue.name;

		sentences.Clear();
		
		Debug.Log("Hello");
		foreach(string s in dialogue.sentences)
		{
			Debug.Log(s);
			sentences.Enqueue(s);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		if(sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";

		foreach (char l in sentence.ToCharArray())
		{
			dialogueText.text += l;
			yield return null;
		}
	}

	private void EndDialogue()
	{
		animator.SetBool("IsOpen", false);
	}
}
