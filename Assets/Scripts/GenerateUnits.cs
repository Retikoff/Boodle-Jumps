using System;
using Unity.VisualScripting;
using UnityEngine;

public class GenerateUnits : MonoBehaviour
{
    [SerializeField] Transform startingPoint;
    GameObject platform;
    public float GenerationBound {get;private set;}
    int unitsCount;
    private const float GENERATIONBOUND_OFFSET_Y = 1f;
    private const float SCREEN_OFFSET_X = 2f; //length of platform 
    private const float JUMP_OFFSET_Y = 0.35f;
    
    void Start()
    {
        platform = Resources.Load("Platform") as GameObject;

        GenerationBound = startingPoint.position.y;

        unitsCount = (int)((WorldOptions.screenSize.y * 2 + 1) / WorldOptions.JumpDistance); // times two cause screenSize.y - y coordinate of object in screen axises
        unitsCount *= WorldOptions.NumberOfGeneratingScreens;
    }

    public void GenerateLayer()
    {
        for(int i = 1; i <= unitsCount; i++)
        {
            GameObject newPlatform = Instantiate(platform);

            Vector3 newPos = new(UnityEngine.Random.Range(-WorldOptions.screenSize.x + SCREEN_OFFSET_X / 2, WorldOptions.screenSize.x - SCREEN_OFFSET_X / 2),UnityEngine.Random.Range(GenerationBound, GenerationBound + WorldOptions.JumpDistance - JUMP_OFFSET_Y),0);
            newPlatform.transform.position = newPos;

            GenerationBound = newPos.y + GENERATIONBOUND_OFFSET_Y;
        }
    }
    
}
