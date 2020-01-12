using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : LevelScript
{
	[SerializeField]    
	Vault vault = null;
	
	[SerializeField]   
	Door vaultRoomDoor = null;
	
	void Start() {
		vault.levelScript = this;
		
		on("vault_open", () => {
			vaultRoomDoor.Close();
		});
	}
}
