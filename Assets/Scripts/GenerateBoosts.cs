using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GenerateBoosts : MonoBehaviour
{
    [SerializeField] Transform startingPoint;
    public int GenerateChance = 70;
    public int BoostCount = 1;
    private List<GameObject> Boosts = new List<GameObject>();
    public float GenerationBound {get;private set;}
    private const float SCREEN_OFFSET_X = 2.5f;

    void Awake()
    {
        GenerationBound = startingPoint.position.y;
        Boosts.Add(Resources.Load<GameObject>("Boost Two"));   
        Boosts.Add(Resources.Load<GameObject>("Boost Three"));   
    }

    public void GenerateLayerOfBoosts(){
        if(UnityEngine.Random.Range(0,101) > GenerateChance){
            GenerationBound += WorldOptions.screenSize.y;
            return;
        }

        for(int i = 1;i <= BoostCount; i++){
            var newBoost = Instantiate(Boosts[UnityEngine.Random.Range(0,Boosts.Count)]);
            Vector3 newPos = new(UnityEngine.Random.Range(-WorldOptions.screenSize.x + SCREEN_OFFSET_X / 2, WorldOptions.screenSize.x - SCREEN_OFFSET_X / 2),UnityEngine.Random.Range(GenerationBound, GenerationBound + WorldOptions.screenSize.y),0);
            newBoost.transform.position = newPos;

            GenerationBound = newPos.y + WorldOptions.screenSize.y;
        }
    }
}
