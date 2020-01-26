﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level0 : LevelScript
{
	// Add objects
	// Ex: [SerializeField]    
	// Ex: Vault vault = null;
	public Van van = null;
	
	protected override void Awake() {
		
		base.Awake();
		
		/// Assigning Conditions
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
            condition.name = "PlayerDidUseCameraControls";

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
				text.sentences.Add("I am in the building...");
				text.sentences.Add("Now I must try to find the vault");
				
				dialogueManager.QueueDialogue(text);
			};
			LevelManager.AddCondition(condition);
		}
		
		{
			LevelCondition condition = new LevelCondition();
			condition.name = "CharacterIsInRoomL0_R";
			condition.fullfillHandler = (LevelCondition c) => {
				
				DialogueText text = new DialogueText();
				text.name = "Watch out!";
				text.sentences.Add("The employee will call the cops when you enter the vaultroom");
				text.sentences.Add("[Left Click] on the employee to keep him under shot");
				text.sentences.Add("Press [F] to fire and try to kill the employee");
				text.sentences.Add("... you don't have to kill him");
				
				dialogueManager.QueueDialogue(text);
			};
			LevelManager.AddCondition(condition);
		}
		
		{
			LevelCondition condition = new LevelCondition();
			condition.name = "FirstDoorOpenened";
			
			condition.failHandler = (LevelCondition c) => {
				DialogueText text = new DialogueText();
				text.name = "Locked";
				text.sentences.Add("This door seems locked...");
				text.sentences.Add("Maybe I can find something to open it");
				
				dialogueManager.QueueDialogue(text);
			};
			
			condition.fullfillHandler = (LevelCondition c) => {
				DialogueText text = new DialogueText();
				text.name = "Nice";
				text.sentences.Add("That key opened the door");
				
				dialogueManager.QueueDialogue(text);
			};
			
			LevelManager.AddCondition(condition);
		}
		
		{
			LevelCondition condition = new LevelCondition();
			condition.name = "PlayerFoundKey";
			
			condition.fullfillHandler = (LevelCondition c) => {
				DialogueText text = new DialogueText();
				text.name = "A Key!";
				text.sentences.Add("I found a key!...");
				
				if (LevelManager.Condition("FirstDoorOpenened").failed) {
					text.sentences.Add("Maybe it will open the door");
				} else {
					text.sentences.Add("For what can I use this?");
				}
				
				dialogueManager.QueueDialogue(text);
			};
			
			LevelManager.AddCondition(condition);
		}
		
		{
			LevelCondition condition = new LevelCondition();
			condition.name = "CopsTriggered";
			
			condition.fullfillHandler = (LevelCondition c) => {
				PoliceSiren.startSiren = true;
				PoliceSiren.gameObject.SetActive(true);
				
				LevelManager.Delay(10, () => {
					SpawnPoliceCar();
				});
			};
			
			LevelManager.AddCondition(condition);
		}
		
		{
			LevelCondition condition = new LevelCondition();
			condition.name = "CharacterIsInRoomVault";
			
			LevelManager.AddCondition(condition);
		}
		
		{
			LevelCondition condition = new LevelCondition();
			condition.name = "PlayerHasUsedGun";
			
			condition.fullfillHandler = (LevelCondition c) => {
				LevelManager.Delay(3, () => {
				
					if (!LevelManager.Condition("CopsTriggered").fullfilled) {
						DialogueText text = new DialogueText();
						text.name = "O no";
						text.sentences.Add("I think someone heard me shooting");
						text.sentences.Add("I hear the police from a distance, I have to hurry");
						
						text.callback = () => {
							LevelManager.Condition("CopsTriggered").Fullfill();
						};
						
						dialogueManager.QueueDialogue(text);
					}
				});
			};
			
			LevelManager.AddCondition(condition);
		}
		
		{
			LevelCondition condition = new LevelCondition();
			condition.name = "EmployeeFleed";
			
			condition.fullfillHandler = (LevelCondition c) => {
				if (!LevelManager.Condition("CopsTriggered").fullfilled) {
					DialogueText text = new DialogueText();
					text.name = "O no";
					text.sentences.Add("I think that employee has called the cops");
					text.sentences.Add("I hear the police from a distance, I have to hurry");
						
					text.callback = () => {
						LevelManager.Condition("CopsTriggered").Fullfill();
					};
					
					dialogueManager.QueueDialogue(text);
				}
			};
			
			LevelManager.AddCondition(condition);
		}
		
		{
			LevelCondition condition = new LevelCondition();
			condition.name = "CharacterIsInVaultRoom";
			
			condition.fullfillHandler = (LevelCondition c) => {
				if (!LevelManager.Condition("CopsTriggered").fullfilled) {
					DialogueText text = new DialogueText();
					text.name = "O no";
					text.sentences.Add("I hear the police from a distance, I have to hurry");
					text.sentences.Add("I think that employee called the police");
					
					text.callback = () => {
						LevelManager.Condition("CopsTriggered").Fullfill();
					};
					
					dialogueManager.QueueDialogue(text);
				}
			};
			
			LevelManager.AddCondition(condition);
		}
		
		{
			LevelCondition condition = new LevelCondition();
			condition.name = "CharacterGotMoneyFromVault";
			
			LevelManager.AddCondition(condition);
		}
		
		{
			LevelCondition condition = new LevelCondition();
			condition.name = "DriveVan";
			
			condition.fullfillHandler = (LevelCondition c) => {
				if (c.failed) 
					return;
				
				van?.Drive();
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
				LevelManager.Condition("CharacterIsInRoomL0_L").Fullfill();
			}
			
			else if (value == "L0 Room R") {
				LevelManager.Condition("CharacterIsInRoomL0_R").Fullfill();
			}
			
			else if (value == "VaultRoom") {
				LevelManager.emit("CharacterIsInVaultRoom");	
			}
		});
		
		LevelManager.on("PlayerTriedOpeningDoorButWasLocked", () => {
			if (!LevelManager.Condition("FirstDoorOpenened").fullfilled)
				LevelManager.Condition("FirstDoorOpenened").Fail();
		});
		
		LevelManager.on("PlayerTriedOpeningDoorSuccesfull", () => {
			LevelManager.Condition("FirstDoorOpenened").Fullfill();
		});
		
		LevelManager.on("PlayerFoundKey", () => {
			LevelManager.Condition("PlayerFoundKey").Fullfill();
		});
		
		LevelManager.on("PlayerDidUseCameraControls", () => {
			LevelManager.Condition("PlayerDidUseCameraControls").Fullfill();
		});
		
		LevelManager.on("PlayerHasUsedGun", () => {
			LevelManager.Condition("PlayerHasUsedGun").Fullfill();
		});
		
		LevelManager.on("EmployeeFleed", () => {
			LevelManager.Condition("EmployeeFleed").Fullfill();
		});

		LevelManager.on("CharacterIsInVaultRoom", () => {
			LevelManager.Condition("CharacterIsInRoomVault").Fullfill();
		});
		
		LevelManager.on("CharacterGotMoneyFromVault", () => {
			LevelManager.Condition("CharacterGotMoneyFromVault").Fullfill();
		});
		
		LevelManager.on("AllCharactersInVan", () => {
			
			LevelManager.Condition("DriveVan").Fullfill();
			
			
			
			if (LevelManager.Condition("CharacterGotMoneyFromVault").fullfilled) {
				LevelEndMessage.title = "Good job!";
				LevelEndMessage.message = "You got some loot and you are not caught!";
				LevelEndMessage.nextLevel = "Level 1";
				LevelEndMessage.LevelSuccessfull = true;
				LevelManager.EndLevel(3);
				return;
			}
			else if (LevelManager.Condition("CopsTriggered").fullfilled) {
				LevelEndMessage.title = "You got away!";
				LevelEndMessage.message = "Sadly you could not get away with money...";
				LevelEndMessage.nextLevel = "Level 0 - Tutorial";
				LevelEndMessage.LevelSuccessfull = false;
				LevelManager.EndLevel(3);
			}
			else {
				LevelEndMessage.title = "You got away!";
				LevelEndMessage.message = "But the idea is that you try to steal some money...";
				LevelEndMessage.nextLevel = "Level 0 - Tutorial";
				LevelEndMessage.LevelSuccessfull = false;
				LevelManager.EndLevel(3);
			}
		});
		
		
		// Trigger first dialogue
		LevelManager.Delay(2, () => {
			
			DialogueText text = new DialogueText();
			text.name = "Welcome";
			text.sentences.Add("You are going to steal some money!");
				
			if (!LevelManager.Condition("CharacterHasBeenSelected").fullfilled) {
				text.sentences.Add("Select a character by clicking on him with your [left mouse]");
			}
			
			if (!LevelManager.Condition("PlayerDidUseCameraControls").fullfilled) {
				text.sentences.Add("Use [up] [down] [left] [right] keys to look through the level");
				text.sentences.Add("Use [scroll] to zoom in and out");
			}
				
			dialogueManager.QueueDialogue(text);
		});
	}
}
