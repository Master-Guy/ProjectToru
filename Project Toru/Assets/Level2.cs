using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : LevelScript
{
	// Add objects
	// Ex: [SerializeField]    
	// Ex: Vault vault = null;
	
	void Start() {
		
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
