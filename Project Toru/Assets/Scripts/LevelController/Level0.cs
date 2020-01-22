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
		
		/// Assigning Levelscripts to objects
		
		/// Assigning Conditions
		/**
		Example: 
		{
            LevelCondition condition = new LevelCondition();
            condition.name = "CharacterMustEnterVan";

            LevelManager.AddCondition(condition);
        }
		*/
		
		{
            LevelCondition condition = new LevelCondition();
            condition.name = "CharacterHasBeenSelected";
			
			condition.fullfillHandler = (LevelCondition c) => {
				LevelManager.Delay(1, () => {
					if (!LevelManager.Condition("CharacterHasBeenMoved").fullfilled) {
					
						DialogueText text2 = new DialogueText();
						text2.name = "Find your way into the building";
					
						text2.sentences.Add("Click with your [right mouse] to move your character");
						dialogueManager.QueueDialogue(text2);
					}	
				});
			};

            LevelManager.AddCondition(condition);
        }
		
		{
            LevelCondition condition = new LevelCondition();
            condition.name = "CharacterHasBeenMoved";

            LevelManager.AddCondition(condition);
        }
		
		{
			LevelCondition condition = new LevelCondition();
			condition.name = "CharacterIsInRoomL0_L";
			condition.fullfillHandler = (LevelCondition c) => {
				
				DialogueText text = new DialogueText();
				text.name = "Find the Vault";
				text.sentences.Add("You are in the building...");
				text.sentences.Add("Now try to find the vault");
				
				dialogueManager.QueueDialogue(text);
			};
			LevelManager.AddCondition(condition);
		}
		
		/// Assigning callbacks
		LevelManager.on("CharacterHasBeenSelected", () => {
			LevelManager.Condition("CharacterHasBeenSelected").Fullfill();
		});
		
		LevelManager.on("CharacterHasBeenMoved", () => {
			LevelManager.Condition("CharacterHasBeenMoved").Fullfill();
		});
		
		LevelManager.on("CharacterIsInRoom", (string value) => {
			
			if (value == "L0 Room L") {
				Debug.Log("Ok");
				LevelManager.Condition("CharacterIsInRoomL0_L").Fullfill();
			}
		});
		
		// Go
		LevelManager.Delay(2, () => {
			
			DialogueText text = new DialogueText();
			text.name = "Welcome";
			text.sentences.Add("You are going to steal some money!");
				
			if (!LevelManager.Condition("CharacterHasBeenSelected").fullfilled) {
				text.sentences.Add("Select a character by clicking on him with your [left mouse]");
			}
			text.callback = () => {
				
			};
				
			dialogueManager.QueueDialogue(text);
		});
	}
}
