using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using GameAnalyticsSDK;

public class LevelManager : MonoBehaviour
{

    Scene scene;

    [SerializeField]
    string levelNameOverride = "";

    [SerializeField]
    int levelIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "level" + levelIndex.ToString(), this.GetLevelName());
    }

    void OnDestroy()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Undefined, "level" + levelIndex.ToString(), this.GetLevelName());
    }

    public void LevelEndSuccess()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "level" + levelIndex.ToString(), this.GetLevelName());
    }

    public void LevelEndFail()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "level" + levelIndex.ToString(), this.GetLevelName());
    }

    public string GetLevelName()
    {
        return (levelNameOverride != "") ? levelNameOverride : scene.name;
    }

    public NPC[] GetNPCS()
    {
        return GetComponents<NPC>();
    }

    public Character[] GetCharacters()
    {
        return GetComponents<Character>();
    }

    public BuildingBehaviour GetBuilding()
    {
        return GetComponent<BuildingBehaviour>();
    }

}
