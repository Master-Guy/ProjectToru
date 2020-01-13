using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRoom : MonoBehaviour
{
    Room roomObj;

    GameObject[] cameras;

    void Awake()
    {
        roomObj = this.gameObject.GetComponent<Room>();
        cameras = GameObject.FindGameObjectsWithTag("Camera");
    }

    public void AlertGuard()
    {
        if (roomObj.getNPCsInRoom().Count > 0)
        {
            foreach (GameObject npc in roomObj.getNPCsInRoom())
            {
                npc.GetComponent<NPC>().Say("I am warned!");
            }

            {
                LevelCondition condition = new LevelCondition();
                condition.name = "GuardCalled_CharacterMustBeInVanBeforeArrival";

                condition.failHandler = (LevelCondition c) =>
                {
                    Debug.Log("Player is caught");

                    // RELEASE THE KRA.. COPS
                    FindObjectOfType<Cops>()?.Spawn();

                    LevelManager.EndLevel("", "", 5);
                };

                LevelManager.AddCondition(condition);
            }
        }

        Invoke("AlertCops", 18);
    }

    void AlertCops()
    {
        //GameObject.FindGameObjectWithTag("levelComplete").GetComponent<LevelComplete>().EnableLose();
        LevelManager.Condition("GuardCalled_CharacterMustBeInVanBeforeArrival").Fail();
    }
}
