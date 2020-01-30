using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : LevelScript
{	
	
	public Character architect;
	public Muscle muscle;
	public Karen karen;

	protected override void Awake() {
		
		base.Awake();

		{
			LevelCondition condition = new LevelCondition();
			condition.name = "ArchitectInRoom";
			
			condition.fullfillHandler = (LevelCondition c) => {
				karen.Say("You again!");
				LevelManager.Delay(2, () => {
					karen.Say("I won't say anything");
				});
			};
			
			LevelManager.AddCondition(condition);
		}

		{
			LevelCondition condition = new LevelCondition();
			condition.name = "MuscleInRoom";
			
			condition.fullfillHandler = (LevelCondition c) => {
				karen.Say("Hi Handsome");
				LevelManager.Delay(2, () => {
					karen.Say("How can I help you?");
				});
			};
			
			LevelManager.AddCondition(condition);
		}

		// LevelManager.on("CharacterIsInRoom", (string roomname) => {
		// 	if (roomname == "L0 Room L") {
		// 		if (architect.currentRoom != null) {
		// 			LevelManager.Condition("ArchitectInRoom").Fullfill();
		// 		}
		// 		else
		// 		{
		// 			LevelManager.Condition("MuscleInRoom").Fullfill();
		// 		}
		// 	}
		// });

		LevelManager.on("CharacterIsInRoom", (GameObject gameObject) => {

			Character character = gameObject.GetComponent<Character>();

			if (character.currentRoom.name == "L0 Room L") {
				if (gameObject == muscle.gameObject)
				{
					LevelManager.Condition("MuscleInRoom").Fullfill();
				}
				else
				{
					LevelManager.Condition("ArchitectInRoom").Fullfill();
				}
			}
		});

		LevelManager.on("StartLevel", () => {
			LevelManager.Delay(1, () => {
				DialogueText text = new DialogueText();
				text.name = "You have a companion!";
				text.sentences.Add("He will help you to keep the hostages under control");
				text.sentences.Add("Control him the same way you control the Architect");
					
				text.callback = () => {
					LevelManager.Delay(0.5f, () => {
						architect.Say("Where is that pacman machine...");
					});
				};

				dialogueManager.QueueDialogue(text);
			});
		});

		LevelManager.on("IsHoldingGun", (GameObject gameObject) => {
			
			Character character = gameObject.GetComponent<Character>();
			if (character == null) {
				return;
			}

			if (character.currentRoom == null) {
				return;
			}
			
			Guard guard = character.currentRoom.GetComponent<Room>()?.GetGuardFromRoom();
			if (guard == null) {
				return;
			};

			// At this stage, character is holding gun while guard is in room
			guard.ShootAt(character);
			guard.Say("HOLD YOUR GUN DOWN!");
		});

		LevelManager.emit("StartLevel");
	}
}
