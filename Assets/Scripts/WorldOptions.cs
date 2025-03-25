using Unity.VisualScripting.FullSerializer;
using UnityEngine;

static public class WorldOptions 
{
    #region PLAYER
    static public readonly float JumpHeight = 10.0f; 
    static public readonly float JumpDistance = 3.3f; // Just calculated during playtest
    #endregion PLAYER
    
    #region GENERATION
    static public readonly int NumberOfGeneratingScreens = 3; 
    #endregion GENERATION
    
    #region ENEMIES
    static public readonly float BirdSpeed = 0.95f;
    #endregion

    #region CLOUDS
    static public readonly float CloudsFrequency = 10f;
    static public readonly float CloudsSpeed = 0.3f;
    #endregion CLOUDS
    
    #region OTHER
    static public Vector2 screenSize; // initializing in GameManager(Awake)
    #endregion OTHER
}
