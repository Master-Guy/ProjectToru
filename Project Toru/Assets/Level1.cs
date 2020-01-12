using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : LevelScript
{
	[SerializeField]    
	Vault vault = null;
	
	[SerializeField]   
	Door vaultRoomDoor = null;
	
	[SerializeField]  
	Van van = null;
	
	void Start() {
		
		/// Assigning Levelscripts to objects
		vault.levelScript = this;
		van.levelScript = this;
		
		/// Assigning Conditions
		{
            LevelConditionInt condition = new LevelConditionInt();
            condition.name = "AllCharactersMustBeInVan";
            condition.targetValue = 1;
            condition.fullfillHandler = (LevelCondition c) =>
            {
                // Move Van
                van.drive = true;
                if (LevelManager.Condition("CharacterMustHaveMoney") != null) {
					if (LevelManager.Condition("CharacterMustHaveMoney").fullfilled) {
                    	LevelManager.EndLevel("Good job!", "You got some loot and you are not caught!", 3);
					}
					else
					{
						LevelManager.EndLevel("You got away", "But the idea is that you steal some money...", 3);
					}
				} else {
					LevelManager.EndLevel("Finished...", "..", 3);
				}
               	
            };

            LevelManager.AddCondition(condition);
        }
		
		{
            LevelCondition condition = new LevelCondition();
            condition.name = "CharacterMustEnterVan";

            LevelManager.AddCondition(condition);
        }
		
		/// Assigning callbacks
		on("vault_open", () => {
			vaultRoomDoor.Close();
		});
		
		on("CharacterEntersVan", () => {
			LevelManager.Condition("CharacterMustEnterVan").Fullfill();
		});
		
		on("AllCharactersInVan", () => {
			LevelManager.Condition("AllCharactersMustBeInVan").Fullfill();
		});
		
		
	}
}
