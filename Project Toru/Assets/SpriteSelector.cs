﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpriteSelector : MonoBehaviour
{
    // The name of the sprite sheet to use
    public string SpriteSheetName;

    // The name of the currently loaded sprite sheet
    private string LoadedSpriteSheetName;


    // If exists, use spritesheet with gun
    public bool enableGun = false;
    private bool loadedGunState = false;


    // The dictionary containing all the sliced up sprites in the sprite sheet
    private Dictionary<string, Sprite> spriteSheet;

    // The Unity sprite renderer so that we don't have to get it multiple times
    private SpriteRenderer spriteRenderer;

    // Path to find the spritesheets
    private const string spritesheetPath = "Characters/";

    // Use this for initialization
    private void Start()
    {
        if (SpriteSheetName == "")
        {
            this.enabled = false;
        }

        // Get and cache the sprite renderer for this game object
        this.spriteRenderer = GetComponent<SpriteRenderer>();

        this.LoadSpriteSheet();
    }

    // Runs after the animation has done its work
    private void LateUpdate()
    {
        // Check if the sprite sheet name has changed (possibly manually in the inspector)
        if (this.LoadedSpriteSheetName != this.SpriteSheetName || loadedGunState != enableGun)
        {
            // Load the new sprite sheet
            this.LoadSpriteSheet();
        }

        // Swap out the sprite to be rendered by its name
        string[] words = this.spriteRenderer.sprite.name.Split('_');

        this.spriteRenderer.sprite = this.spriteSheet[SpriteSheetName + "_" + words.Last<string>()];
    }

    // Loads the sprites from a sprite sheet
    private void LoadSpriteSheet()
    {
        // Load the sprites from a sprite sheet file (png). 
        // Note: The file specified must exist in a folder named Resources
        string path = spritesheetPath + this.SpriteSheetName;
        if (enableGun)
        {
            path += "_gun";
        }

        var sprites = Resources.LoadAll<Sprite>(path);

        // Remember the name of the sprite sheet in case it is changed later
        this.LoadedSpriteSheetName = this.SpriteSheetName;

        // Update gun state
        loadedGunState = enableGun;

        if (sprites.Count<Sprite>() == 0)
        {
            Debug.Log("Warning, asset " + SpriteSheetName + " not found in Assets/Resources/" + path);
            return;
        }

        this.spriteSheet = sprites.ToDictionary(x => x.name, x => x);


    }
}