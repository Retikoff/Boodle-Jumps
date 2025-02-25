using Unity.VisualScripting.FullSerializer;
using UnityEngine;

static public class WorldOptions 
{
    #region PLAYER
    static public float JumpHeight = 10.0f; 
    static public float JumpDistance = 3.3f; // Just calculated during playtest
    #endregion PLAYER
    
    #region GENERATION
    static public int NumberOfGeneratingScreens = 3; 
    #endregion GENERATION
    
    #region CLOUDS
    static public float CloudsFrequency = 12f;
    static public float CloudsSpeed = 0.3f;
    #endregion CLOUDS
    
    #region OTHER
    static public Vector2 screenSize; // initializing in GameManager(Awake)
    #endregion OTHER
}
