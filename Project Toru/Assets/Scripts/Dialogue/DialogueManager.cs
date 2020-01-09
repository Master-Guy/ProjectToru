using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
	public Text nameText, dialogueText;

	private Animator animator;

	private Queue<string> sentences;

	void Start()
	{
		animator = GetComponent<Animator>();

		sentences = new Queue<string>();
	}

	public void StartDialogue(DialogueText dialogue)
	{
		animator.SetBool("IsOpen", true);

		nameText.text = dialogue.name;

		sentences.Clear();

		foreach(string s in dialogue.sentences)
		{
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
