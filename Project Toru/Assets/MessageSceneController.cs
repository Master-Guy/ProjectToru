using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSceneController : MonoBehaviour
{
    [SerializeField]
    TextMesh Title = null;

    [SerializeField]
    TextMesh Message = null;

    // Start is called before the first frame update
    void Start()
    {

        // Checking is Title is set
        if (LevelEndMessage.title == "")
        {
            Debug.LogError("No title set prior to Level Scene");
            LevelEndMessage.title = ":)";
        }

        // Update interface
        Title.text = LevelEndMessage.title;
        Message.text = LevelEndMessage.message;

        // Reset LevelEndMessage
        LevelEndMessage.Reset();
    }
}
