using Unity.VisualScripting.FullSerializer;
using UnityEngine;

static public class WorldOptions 
{
    static public float JumpHeight = 10.0f; 
    static public float JumpDistance = 3.3f; // Just calculated during playtest
    static public int NumberOfGeneratingScreens = 3; 
    static public float CloudsFrequency = 12f;
    static public Vector2 screenSize; // initializing in GameManager(Awake)
}
