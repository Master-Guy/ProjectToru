using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : LevelScript
{	
	
	public Character architect;
	public Muscle muscle;

	public Karen karen;
	public Guard guardDownStairs;
	public Guard guardUpstairs;

	public Employee employeeDownstairsLeft;
	public Employee employeeDownstairsRight;
	public Employee employeeUpstairs;

	public FatGuy fatGuy;

	public Door DownStairsDoor;
	

	protected override void Awake() {
		
		base.Awake();

		{
			LevelCondition condition = new LevelCondition();
			condition.name = "ArchitectInRoom";
			
			condition.fullfillHandler = (LevelCondition c) => {
				if (LevelManager.Condition("MuscleInRoom").fullfilled) return;

				karen.Say("You again!");
			};
			
			LevelManager.AddCondition(condition);
		}

		{
			LevelCondition condition = new LevelCondition();
			condition.name = "MuscleInRoom";
			
			condition.fullfillHandler = (LevelCondition c) => {
				if (LevelManager.Condition("ArchitectInRoom").fullfilled) return;

				karen.Say("Hi Handsome");
				LevelManager.Delay(2, () => {
					if (LevelManager.Condition("PlayerFoundKey").fullfilled) return;

					karen.Say("How can I help you?");
				});
			};
			
			LevelManager.AddCondition(condition);
		}

		{
			LevelCondition condition = new LevelCondition();
			condition.name = "PlayerFoundKey";
			
			LevelManager.AddCondition(condition);
		}

		{
			LevelCondition condition = new LevelCondition();
			condition.name = "KarenFleed";
			
			
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

		{
			LevelCondition condition = new LevelCondition();
			condition.name = "EmployeesTalk";
			
			condition.fullfillHandler = (LevelCondition c) => {
				DialogueText text = new DialogueText();
				text.name = "Employee:";
				text.sentences.Add("Are you our new colleague?");
				text.sentences.Add("You must be from the other office");
				text.sentences.Add("Here, have the key to your office");
				
				text.callback = () => {
					employeeDownstairsRight.Say("Hello!");
				};
				
				dialogueManager.QueueDialogue(text);

				employeeDownstairsLeft.dropBag();
			};
			
			LevelManager.AddCondition(condition);
		}

		{
			LevelCondition condition = new LevelCondition();
			condition.name = "EmployeesSurrender";
			
			condition.fullfillHandler = (LevelCondition c) => {
				employeeDownstairsLeft.Surrender();
				employeeDownstairsRight.Surrender();
			};
			
			LevelManager.AddCondition(condition);
		}

		{
			LevelCondition condition = new LevelCondition();
			condition.name = "KarenTalksToTheManager";
			
			condition.fullfillHandler = (LevelCondition c) => {

				karen.animator.SetFloat("moveX", 1);

				DialogueText text = new DialogueText();
				text.name = "Karen:";
				text.sentences.Add("You are my manager!!!!!!");
				text.sentences.Add("Why are you so stupid??");
				text.sentences.Add("It was a guy with a gun! The same one as last year!");
				text.sentences.Add("It was...");

				dialogueManager.QueueDialogue(text);
				
				text.callback = () => {
					karen.animator.SetFloat("moveX", -1);
					karen.Say("YOU");
					karen.BeKaren(architect);
					LevelManager.Delay(0.5f, () => {
						employeeUpstairs.Say("WHOA! Easy!");
					});
				};
			};
			
			LevelManager.AddCondition(condition);
		}
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
			else if (character.currentRoom.name == "L0 Room M") {
				if (character.weapon.weaponOut) {
					LevelManager.Condition("EmplyeesSurrender").Fullfill();
				} else {
					LevelManager.Condition("EmployeesTalk").Fullfill();
				}
			}
			else if (character.currentRoom.name == "L1 Room L") {
				guardUpstairs.Say("What are you doing here?");
				if (character.weapon.weaponOut) {
					guardUpstairs.ShootAt(character);
				} else {
					guardUpstairs.Arrest(character);
				}
			}
			else if (character.currentRoom.name == "L1 Room R") {
				LevelManager.Delay(0.5f, () => {
					LevelManager.Condition("KarenTalksToTheManager").Fullfill();
				});
				
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

			employeeDownstairsLeft.animator.SetFloat("moveX", -1);
			employeeDownstairsRight.animator.SetFloat("moveX", -1);
			karen.animator.SetFloat("moveX", -1);
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

		

		LevelManager.on("PlayerFoundKey", (GameObject gameObject) => {
			if (LevelManager.Condition("PlayerFoundKey").fullfilled) return;

			LevelManager.Condition("PlayerFoundKey").Fullfill();
			Character character = gameObject.GetComponent<Character>();

			if (character.currentRoom.name == "L0 Room L") {
				Guard guard = character.currentRoom.GetGuardFromRoom();
				if (guard == null) {
					return;
				};

				guard.Arrest(character);

				if (!LevelManager.Condition("KarenFleed").fullfilled)
					karen.Say("GUARD HELP!");
			}
		});

		LevelManager.on("Surrendered", (GameObject gameObject) => {
			NPC npc = gameObject.GetComponent<NPC>();

			if (npc.currentRoom.name == "L0 Room L") {
				LevelManager.Condition("KarenFleed").Fullfill();

				karen.pathfinder.setPosTarget(9, 1);
				LevelManager.Delay(0.5f, () => {

					npc.currentRoom.door.Open();
				
					karen.pathfinder.setPosTarget(17.3f, 1);
					LevelManager.Delay(1, () => {
						DownStairsDoor.Open();

						LevelManager.Delay(1, () => {
							karen.pathfinder.setPosTarget(21, 5);

							LevelManager.Delay(1, () => {
								DownStairsDoor.Close();

								LevelManager.Delay(3, () => {
									karen.transform.position = new Vector3(30, 5.2f);
								});
							});
						});
					});
				});
			}

			else {
				foreach(var _npc in npc.currentRoom.npcsInRoom) {
					if (_npc == gameObject) continue;

					_npc.GetComponent<NPC>().Surrender();
				}
			}

			
			// if (gameObject.name == "Karen")
				// LevelManager.Condition("KarenSurrendered").Fullfill();
		});

		// LevelManager.emit("StartLevel");
	}

	
}
