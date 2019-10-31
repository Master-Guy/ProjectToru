using UnityEngine;

[CreateAssetMenu(fileName = "new NPCinfo", menuName = "NPCinfo")]
public class NPCinfo : ScriptableObject
{
	public npcType type;
	public npcState state;
	public RuntimeAnimatorController animatorController;
}
