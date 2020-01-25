using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : LevelScript
{	
	public Van van = null;
	
	void Awake() {
		
		LevelManager.setLevel();
			
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
		
		LevelManager.on("AllCharactersInVan", () => {
			
			LevelManager.Condition("DriveVan").Fullfill();
			
			// if (LevelManager.Condition("CharacterGotMoneyFromVault").fullfilled) {
			// 	LevelEndMessage.title = "Good job!";
			// 	LevelEndMessage.message = "You got some loot and you are not caught!";
			// 	LevelEndMessage.nextLevel = "Level 1";
			// 	LevelEndMessage.LevelSuccessfull = true;
			// 	LevelManager.EndLevel(3);
			// 	return;
			// }
			// else if (LevelManager.Condition("CopsTriggered").fullfilled) {
			// 	LevelEndMessage.title = "You got away!";
			// 	LevelEndMessage.message = "Sadly you could not get away with money...";
			// 	LevelEndMessage.nextLevel = "Level 0 - Tutorial";
			// 	LevelEndMessage.LevelSuccessfull = false;
			// 	LevelManager.EndLevel(3);
			// }
			//else
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
