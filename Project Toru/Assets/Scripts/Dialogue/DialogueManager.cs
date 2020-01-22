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
	private Queue<DialogueText> dialogues = new Queue<DialogueText>();
	
	private DialogueText currentDialogue = null;

	void Start()
	{
		animator = GetComponent<Animator>();
	}
	
	public void QueueDialogue(DialogueText dialogue) {
		
		dialogues.Enqueue(dialogue);
		
		if (currentDialogue == null) {
			currentDialogue = dialogue;
			StartDialogue();
		}
	}
	
	public void StartDialogue()
	{
		if (dialogues.Count == 0) {
			return;
		}
		
		animator.SetBool("IsOpen", true);
		
		DialogueText dialogue = dialogues.Dequeue();
		nameText.text = dialogue.name;	

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
		sentences.Clear();
		animator.SetBool("IsOpen", false);
		LevelManager.Delay(0.2f, () => {
			StartDialogue();
			DialogueText oldCurrentDialogue = currentDialogue;
			currentDialogue = null;
			oldCurrentDialogue.callback?.Invoke();
		});
	}
}
