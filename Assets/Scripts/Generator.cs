using UnityEngine;

public class Generator : ScriptableObject
{
    private GameObject generateItem;
    public float GenerationBound {get; private set;}
    private int spawnRateChance;
    private int unitsCount = 0;
    private float GenerationRangeY;
    private float GenerationBoundOffsetY;

    private const float SCREEN_OFFSET_X = 2.5f;
    public Generator(GameObject generateItem, Vector3 startPoint, int unitsCount, float GenerationRangeY, int spawnRateChance, float GenerationBoundOffsetY){
        this.generateItem = generateItem;
        GenerationBound = startPoint.y;
        this.unitsCount = unitsCount;
        this.GenerationRangeY = GenerationRangeY;
        this.spawnRateChance = spawnRateChance;
        this.GenerationBoundOffsetY = GenerationBoundOffsetY;
    }

    public void GenerateLayer(){
        if(UnityEngine.Random.Range(0,101) > spawnRateChance){
            //mb netakoi offset nado
            GenerationBound += WorldOptions.screenSize.y;
            return;
        }

        for(int i = 1; i <= unitsCount; i++){
            GameObject newObj = Instantiate(generateItem);

            Vector3 newPos = new(UnityEngine.Random.Range(-WorldOptions.screenSize.x + SCREEN_OFFSET_X / 2, WorldOptions.screenSize.x - SCREEN_OFFSET_X / 2),UnityEngine.Random.Range(GenerationBound, GenerationBound + GenerationRangeY),0);
            newObj.transform.position = newPos;

            GenerationBound = newPos.y + GenerationBoundOffsetY;
        }
    }
}
