using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : LevelScript
{	
	public Van van = null;
	public Room VaultRoom = null;
	public Character player = null;
	
	protected override void Awake() {
		
		base.Awake();
			
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
		
		{
			LevelCondition condition = new LevelCondition();
			condition.name = "PlayerFoundKey";
			
			condition.fullfillHandler = (LevelCondition c) => {
				{
					DialogueText text = new DialogueText();
					text.name = "You found a key";
					text.sentences.Add("I am sure this will open many doors");
					
					dialogueManager.QueueDialogue(text);
				}
				
				{
					DialogueText text = new DialogueText();
					text.name = "Karen:";
					text.sentences.Add("The brutality!");
					
					dialogueManager.QueueDialogue(text);
				}
				
				Debug.Log("Dialogue");
			};
			
			LevelManager.AddCondition(condition);
		}
		
		{
			LevelCondition condition = new LevelCondition();
			condition.name = "GuardsAlerted";
			
			condition.fullfillHandler = (LevelCondition c) => {
				
				VaultRoom.door.Close();
				
				LevelManager.Delay(Random.Range(10, 20), () => {
					SpawnPoliceCar();
				});
			};
			
			LevelManager.AddCondition(condition);
		}
		
		{
			LevelCondition condition = new LevelCondition();
			condition.name = "CopsTriggered";
			
			LevelManager.AddCondition(condition);
		}
		
		{
			LevelCondition condition = new LevelCondition();
			condition.name = "CameraWasDisabled";
			
			condition.fullfillHandler = (LevelCondition c) => {
				DialogueText text = new DialogueText();
				text.name = "Camera looks off";
				text.sentences.Add("It looks like that camera is not working!");
				
				dialogueManager.QueueDialogue(text);
			};
			
			LevelManager.AddCondition(condition);
		}	
		
		{
			LevelCondition condition = new LevelCondition();
			condition.name = "CameraDetectedPlayer";
			
			condition.fullfillHandler = (LevelCondition c) => {
				
				foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Room"))
				{
					if (obj.name.Equals("Security Room"))
					{
						obj.GetComponent<CameraRoom>()?.AlertGuard();
					}
				}
				
				if (!LevelManager.Condition("GuardsAlerted").fullfilled) {
					LevelManager.Condition("CameraWasDisabled").Fullfill();
				}
			};
			
			LevelManager.AddCondition(condition);
		}
		
		{
			LevelCondition condition = new LevelCondition();
			condition.name = "PlayerEntersCameraRoom";
			
			condition.fullfillHandler = (LevelCondition c) => {
				
				player?.gameObject.GetComponent<ExecutePathFinding>()?.StopPathFinding();
				
				DialogueText text = new DialogueText();
				text.name = "Watch out!";
				text.sentences.Add("There is a camera in this room!");
				
				dialogueManager.QueueDialogue(text);
			};
			
			LevelManager.AddCondition(condition);
		}
		
		{
			LevelCondition condition = new LevelCondition();
			condition.name = "PlayerTriedOpeningDoorButWasLocked";
			
			condition.fullfillHandler = (LevelCondition c) => {
				DialogueText text = new DialogueText();
				text.name = "Door is locked";
				text.sentences.Add("I don't have the correct key");
				text.sentences.Add("I got stuck here");
				
				text.callback = () => {
					LevelEndMessage.title = "You got stuck in the vaultroom";
					LevelEndMessage.message = "You didn't play everything out";
					LevelEndMessage.nextLevel = "Level 1";
					LevelEndMessage.LevelSuccessfull = false;
					LevelManager.EndLevel(1);
				};
				
				dialogueManager.QueueDialogue(text);
			};
			
			LevelManager.AddCondition(condition);
		}
		
		{
			LevelCondition condition = new LevelCondition();
			condition.name = "SomeoneHeardShooting";
			
			condition.fullfillHandler = (LevelCondition c) => {
				LevelManager.Delay(Random.Range(10, 20), () => {
					SpawnPoliceCar();
				});
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
			condition.name = "LetKarenTalk";
			
			condition.fullfillHandler = (LevelCondition c) => {
				
				LevelManager.Delay(1, () => {
					DialogueText text = new DialogueText();
				
					text.name = "Karen:";
					text.sentences.Add("Hello! Welcome to Bank of Clyde");
					text.sentences.Add("You are holding a nice gun");
					text.sentences.Add("I'm sure you won't use that here because that will kill people");
					
					dialogueManager.QueueDialogue(text);
				});
				
			};
			
			LevelManager.AddCondition(condition);
		}
		
		{
			LevelCondition condition = new LevelCondition();
			condition.name = "KarenSurrendered";
			
			condition.fullfillHandler = (LevelCondition c) => {
				DialogueText text = new DialogueText();
				
				text.name = "Karen:";
				text.sentences.Add("Don't point that on me! Why are you doing that?!?");
				text.sentences.Add("I only can open a bank account for you... No money here!");
				text.sentences.Add("But don't look into my desk!!");
					
				dialogueManager.QueueDialogue(text);
			};
			
			LevelManager.AddCondition(condition);
		}
		
		
		LevelManager.on("CameraDetectedPlayer", (string roomname) => {
			LevelManager.Condition("CameraDetectedPlayer").Fullfill();
		});
		
		LevelManager.on("GuardsAlerted", () => {
			LevelManager.Condition("GuardsAlerted").Fullfill();
		});
		
		LevelManager.on("PlayerFoundKey", () => {
			LevelManager.Condition("PlayerFoundKey").Fullfill();
		});
		
		LevelManager.on("CharacterIsInRoom", (string roomname) => {
			
			if (roomname == "L0 Room L") {
				LevelManager.Condition("LetKarenTalk").Fullfill();
			}
			else if (roomname == "L1 Room") {
				LevelManager.Condition("PlayerEntersCameraRoom").Fullfill();
			}
		});
		
		LevelManager.on("KarenSurrendered", () => {
			LevelManager.Condition("KarenSurrendered").Fullfill();
		});
		
		LevelManager.on("PlayerTriedOpeningDoorButWasLocked", (string roomname) => {
			if (roomname == "L1 Room") {
				LevelManager.Condition("PlayerTriedOpeningDoorButWasLocked").Fullfill();
			}
		});
		
		LevelManager.on("EmployeeFleed", () => {
			LevelManager.Delay(Random.Range(10, 20), () => {
				SpawnPoliceCar();
				
				if (LevelManager.RandomChange(70)) {
					LevelManager.Delay(1, () => {
						DialogueText text = new DialogueText();
						text.name = "I hear the polce";
						text.sentences.Add("That bloody employee called the cops, i am sure!");
						
						dialogueManager.QueueDialogue(text);
					});
				}
			});
		});
		
		LevelManager.on("PlayerHasUsedGun", () => {
			SpawnPoliceCar();
			if (LevelManager.RandomChange(10)) {
				//LevelManager.Condition("SomeoneHeardShooting").Fullfill();
			}
		});
		
		LevelManager.on("AllCharactersInVan", () => {
			
			LevelManager.Condition("DriveVan").Fullfill();
			
			if (LevelManager.Condition("CharacterGotMoneyFromVault").fullfilled) {
				LevelEndMessage.title = "Good job!";
				LevelEndMessage.message = "You got some loot and you are not caught!";
				LevelEndMessage.nextLevel = "Level 2";
				LevelEndMessage.LevelSuccessfull = true;
				LevelManager.EndLevel(3);
				return;
			}
			else if (LevelManager.Condition("CopsTriggered").fullfilled) {
				LevelEndMessage.title = "You got away!";
				LevelEndMessage.message = "Sadly you could not get away with money...";
				LevelEndMessage.nextLevel = "Level 1";
				LevelEndMessage.LevelSuccessfull = false;
				LevelManager.EndLevel(3);
			}
			else
			{
				LevelEndMessage.title = "You got away!";
				LevelEndMessage.message = "But the idea is that you try to steal some money...";
				LevelEndMessage.nextLevel = "Level 1";
				LevelEndMessage.LevelSuccessfull = false;
				LevelManager.EndLevel(3);
			}
		});
		
	}
}
