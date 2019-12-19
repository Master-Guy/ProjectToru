using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is static, so the content remains even when we have a scene change.
/// Because of this, we can pass data to other scenes
/// </summary>
public static class LevelEndMessage
{
    public static string title;
    public static string message;

    public static void Reset()
    {
        title = "";
        message = "";
    }
}
