using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level0 : LevelScript
{
	// Add objects
	// Ex: [SerializeField]    
	// Ex: Vault vault = null;
	
	void Start() {
		
		LevelManager.setLevel();
		
		DialogueText text = new DialogueText();
		text.name = "Hello";
		text.sentences.Add("What?");
		text.sentences.Add("Are");
		text.sentences.Add("You");
		text.sentences.Add("Doing?");
		
		dialogueManager.StartDialogue(text);
		
		
		/// Assigning Levelscripts to objects
		//Ex: vault.levelScript = this;
		
		/// Assigning Conditions
		/**
		Example: 
		{
            LevelCondition condition = new LevelCondition();
            condition.name = "CharacterMustEnterVan";

            LevelManager.AddCondition(condition);
        }
		*/
		
		/// Assigning callbacks
		/**
		Example: 
		on("vault_open", () => {
			vaultRoomDoor.Close();
		});
		*/
	}
}
