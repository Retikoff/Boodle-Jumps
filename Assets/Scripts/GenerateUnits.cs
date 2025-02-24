using System;
using Unity.VisualScripting;
using UnityEngine;

public class GenerateUnits : MonoBehaviour
{
    //Удалить строчку ниже!!! 
    [SerializeField] PlayerController player;
    [SerializeField] Camera _camera;
    [SerializeField] Transform startingPoint;
    GameObject platform;
    Vector2 screenSize;
    public float GenerationBound {get;private set;}
    int unitsCount;
    private const float GENERATIONBOUND_OFFSET_Y = 1f;
    private const float SCREEN_OFFSET_X = 2f; //length of platform 
    private const int NUMBER_OF_GENERATED_SCREENS = 3;
    
    void Awake()
    {
        platform = Resources.Load("Platform") as GameObject;

        screenSize = new Vector2(Screen.width, Screen.height);
        screenSize = _camera.ScreenToWorldPoint(screenSize);
        Debug.Log(screenSize);

        GenerationBound = startingPoint.position.y;

        unitsCount = (int)((screenSize.y * 2 + 1) / WorldOptions.JumpDistance); // times too cause screenSize.y - y coordinate of object in screen axises

        Debug.Log("Units count: " + unitsCount); 
        unitsCount *= NUMBER_OF_GENERATED_SCREENS;
    }

    public void GenerateLayer()
    {
        for(int i = 1; i <= unitsCount; i++)
        {
            GameObject newPlatform = Instantiate(platform);
            Vector3 newPos = new(UnityEngine.Random.Range(-screenSize.x + SCREEN_OFFSET_X / 2, screenSize.x - SCREEN_OFFSET_X / 2),UnityEngine.Random.Range(GenerationBound, GenerationBound + WorldOptions.JumpHeight / 4),0);
            newPlatform.transform.position = newPos;
            newPlatform.GetComponent<Platform>().SetPlayer(player);
            GenerationBound = newPos.y + GENERATIONBOUND_OFFSET_Y;
        }
    }
    
}
