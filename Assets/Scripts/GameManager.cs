using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Generator platformGenerator, cloudGenerator, beeGenerator, birdGenerator, boostTwoGenerator, boostThreeGenerator;
    [SerializeField] GameObject platform, cloud, bee, bird, boostTwo, boostThree;
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
        platformGenerator = Generator.CreateGenerator(platform, startPoint.position, (int)((WorldOptions.screenSize.y * 2 + 1) / WorldOptions.JumpDistance) * WorldOptions.NumberOfGeneratingScreens,WorldOptions.JumpDistance - JUMP_OFFSET_Y, 100, 1f);
        platformGenerator.GenerateLayer();

        cloudGenerator = Generator.CreateGenerator(cloud,startPoint.position, (int)((WorldOptions.screenSize.y * 2 + 1) / WorldOptions.CloudsFrequency) * (WorldOptions.NumberOfGeneratingScreens - 1),WorldOptions.CloudsFrequency, 100, 2f, true, cloudSprites);
        cloudGenerator.GenerateLayer();

        beeGenerator = Generator.CreateGenerator(bee, startPoint.position, 1, WorldOptions.screenSize.y, 40, WorldOptions.screenSize.y);
        beeGenerator.GenerateLayer();

        birdGenerator = Generator.CreateGenerator(bird, startPoint.position, 1, WorldOptions.screenSize.y, 20, WorldOptions.screenSize.y * 2);
        birdGenerator.GenerateLayer();

        boostTwoGenerator = Generator.CreateGenerator(boostTwo, startPoint.position, 1, WorldOptions.screenSize.y, 35, WorldOptions.screenSize.y * 2);
        boostTwoGenerator.GenerateLayer();

        boostThreeGenerator = Generator.CreateGenerator(boostThree, startPoint.position, 1, WorldOptions.screenSize.y, 14, WorldOptions.screenSize.y * 5);
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
