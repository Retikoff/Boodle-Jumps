using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Generator platformGenerator, cloudGenerator, beeGenerator, birdGenerator, boostTwoGenerator, boostThreeGenerator;
    [SerializeField] GameObject platform;
    [SerializeField] int platformChanceToSpawn;
    [SerializeField] GameObject cloud;
    [SerializeField] int cloudChanceToSpawn; 
    [SerializeField] GameObject bee;
    [SerializeField] int beeChanceToSpawn; 
    [SerializeField] int beeCount;
    [SerializeField] GameObject bird;
    [SerializeField] int birdChanceToSpawn;
    [SerializeField] int birdCount;
    [SerializeField] GameObject boostTwo;
    [SerializeField] int boostTwoChanceToSpawn;
    [SerializeField] int boostTwoCount;
    [SerializeField] GameObject boostThree;
    [SerializeField] int boostThreeChanceToSpawn;
    [SerializeField] int boostThreeCount;
    [SerializeField] PlayerController player;
    [SerializeField] Transform startPoint;

    private List<Sprite> cloudSprites;
    private const string cloudResourcesPattern = "Cloud";
    private const float  GENERATION_OFFSET_Y = 7f;
    private const float DEATHZONE_OFFSET_Y = 1.5f; 
    private const float JUMP_OFFSET_Y = 0.35f;
    
    void Awake()
    {
        WorldOptions.screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width,Screen.height));
        print(SystemInfo.graphicsDeviceName);
        cloudSprites = new List<Sprite>();
        for(int i = 1; i <= 9; i++){
            cloudSprites.Add(Resources.Load<Sprite>(cloudResourcesPattern + i));
        }
    }

    void Start()
    {       
        platformGenerator = Generator.CreateGenerator(platform, startPoint.position, (int)((WorldOptions.screenSize.y * 2 + 1) / WorldOptions.JumpDistance) * WorldOptions.NumberOfGeneratingScreens,WorldOptions.JumpDistance - JUMP_OFFSET_Y, platformChanceToSpawn, 1f);
        platformGenerator.GenerateLayer();

        cloudGenerator = Generator.CreateGenerator(cloud,startPoint.position, (int)((WorldOptions.screenSize.y * 2 + 1) / WorldOptions.CloudsFrequency) * (WorldOptions.NumberOfGeneratingScreens - 1),WorldOptions.CloudsFrequency, cloudChanceToSpawn, 2f, true, cloudSprites);
        cloudGenerator.GenerateLayer();

        beeGenerator = Generator.CreateGenerator(bee, startPoint.position, beeCount, WorldOptions.screenSize.y, beeChanceToSpawn, WorldOptions.screenSize.y);
        beeGenerator.GenerateLayer();

        birdGenerator = Generator.CreateGenerator(bird, startPoint.position, birdCount, WorldOptions.screenSize.y, birdChanceToSpawn, WorldOptions.screenSize.y * 2);
        birdGenerator.GenerateLayer();

        boostTwoGenerator = Generator.CreateGenerator(boostTwo, startPoint.position, boostTwoCount, WorldOptions.screenSize.y, boostTwoChanceToSpawn, WorldOptions.screenSize.y * 2);
        boostTwoGenerator.GenerateLayer();

        boostThreeGenerator = Generator.CreateGenerator(boostThree, startPoint.position, boostThreeCount, WorldOptions.screenSize.y, boostThreeChanceToSpawn, WorldOptions.screenSize.y * 5);
        boostThreeGenerator.GenerateLayer();
    }

    void Update()
    {
        if(player.transform.position.y >= platformGenerator.GenerationBound - GENERATION_OFFSET_Y)
        {
            platformGenerator.GenerateLayer();
        }
        
        if(player.transform.position.y >= cloudGenerator.GenerationBound - GENERATION_OFFSET_Y){
            cloudGenerator.GenerateLayer();
        }

        if(player.transform.position.y >= beeGenerator.GenerationBound - GENERATION_OFFSET_Y){
            beeGenerator.GenerateLayer();
        }

        if(player.transform.position.y >= birdGenerator.GenerationBound - GENERATION_OFFSET_Y){
            birdGenerator.GenerateLayer();
        }

        if(player.transform.position.y >= boostTwoGenerator.GenerationBound - GENERATION_OFFSET_Y){
            boostTwoGenerator.GenerateLayer();
        }

        if(player.transform.position.y >= boostThreeGenerator.GenerationBound - GENERATION_OFFSET_Y){
            boostThreeGenerator.GenerateLayer();
        }

        if(player.transform.position.y <= Camera.main.transform.position.y - WorldOptions.screenSize.y - DEATHZONE_OFFSET_Y){
            SceneManager.LoadScene(0);
        }
    }
}
