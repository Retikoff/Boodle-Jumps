using System;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GenerateClouds : MonoBehaviour
{
    [SerializeField] Transform startingPoint;
    GameObject cloud;
    public float GenerationBound {get; private set;}
    int unitsCount = 0;
    private const string sortingLayer = "Clouds";
    private const string resourcesPattern = "Cloud";
    private const float SCREEN_OFFSET_X = 2.5f;
    private const float GENERATIONBOUND_OFFSET_Y = 1f;
    void Start()
    {
        cloud = Resources.Load("Cloud") as GameObject;

        unitsCount = (int)((WorldOptions.screenSize.y * 2 + 1) / WorldOptions.CloudsFrequency);
        
        unitsCount *= WorldOptions.NumberOfGeneratingScreens;
        GenerationBound = startingPoint.position.y;
    }

    public void GenerateLayerOfClouds()
    {
        int[] uniques = GenerateRandomUniqueArray(9);

        for(int i = 1; i <= unitsCount; i++)
        {
            GameObject tmp = Instantiate(cloud);

            tmp.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(resourcesPattern + uniques[i % 9]) ;
            tmp.GetComponent<SpriteRenderer>().sortingLayerName = sortingLayer;

            Vector3 newPos = new(UnityEngine.Random.Range(-WorldOptions.screenSize.x + SCREEN_OFFSET_X / 2, WorldOptions.screenSize.x - SCREEN_OFFSET_X / 2),UnityEngine.Random.Range(GenerationBound, GenerationBound + WorldOptions.CloudsFrequency ),0);
            tmp.transform.position = newPos;

            GenerationBound = newPos.y + GENERATIONBOUND_OFFSET_Y;
        }
    }

    private int[] GenerateRandomUniqueArray(int size)
    {
        int[] result = Enumerable.Range(1, size).ToArray(); 
        System.Random r = new System.Random();
        for (int i = size - 1; i >= 1; i--)
        {
        int j = r.Next(i + 1);
        
        int temp = result[j];
        result[j] = result[i];
        result[i] = temp;
        }

        return result;
    }
}
