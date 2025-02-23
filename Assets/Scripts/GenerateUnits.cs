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
    private const float BOUND_OFFSET_Y = 1f;
    private const float SCREEN_OFFSET_X = 3f;
    
    void Awake()
    {
        platform = Resources.Load("Platform") as GameObject;

        screenSize = new Vector2(Screen.width, Screen.height);
        screenSize = _camera.ScreenToWorldPoint(screenSize);
        Debug.Log(screenSize);

        GenerationBound = startingPoint.position.y;

        unitsCount = (int)(screenSize.y + 1 / WorldOptions.JumpHeight);
        Debug.Log("Units count: " + unitsCount); 
        unitsCount *= 2;
    }

    public void GenerateLayer()
    {
        for(int i = 1; i <= unitsCount; i++)
        {
            GameObject newPlatform = Instantiate(platform);
            Vector3 newPos = new(UnityEngine.Random.Range(-screenSize.x + SCREEN_OFFSET_X, screenSize.x - SCREEN_OFFSET_X),UnityEngine.Random.Range(GenerationBound, GenerationBound + WorldOptions.JumpHeight / 4),0);
            newPlatform.transform.position = newPos;
            newPlatform.GetComponent<Platform>().SetPlayer(player);
            GenerationBound = newPos.y + BOUND_OFFSET_Y;
        }
    }
    
}
