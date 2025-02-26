using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GenerateEnemies : MonoBehaviour
{
    [SerializeField] Transform startingPoint;
    public int GenerateChance = 100;
    public int EnemiesCount = 1;
    private List<GameObject> Enemies = new List<GameObject>();
    public float GenerationBound {get; private set;}
    private const float SCREEN_OFFSET_X = 2f;
    private const float GENERATIONBOUND_OFFSET_Y = 4f;

    void Awake()
    {
        GenerationBound = startingPoint.position.y;
        Enemies.Add(Resources.Load<GameObject>("Bee"));
        Enemies.Add(Resources.Load<GameObject>("Bird"));
    }

    public void GenerateLayerOfEnemies()
    {
        if(UnityEngine.Random.Range(0,101) > GenerateChance)
            return;
        
        for(int i = 1;i<=EnemiesCount;i++)
        {
            try
            { // I dont remember how i fix it so try catch stays
                var newEnemy = Instantiate(Enemies[UnityEngine.Random.Range(0,Enemies.Count)]);
                Vector3 newPos = new(UnityEngine.Random.Range(-WorldOptions.screenSize.x + SCREEN_OFFSET_X / 2, WorldOptions.screenSize.x - SCREEN_OFFSET_X / 2),UnityEngine.Random.Range(GenerationBound, GenerationBound + WorldOptions.screenSize.y),0);
                newEnemy.transform.position = newPos;

                GenerationBound = newPos.y + GENERATIONBOUND_OFFSET_Y;
            }
            catch(Exception e)
            {
                Debug.Log(e);
            }
        }
    }
}
