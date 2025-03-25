using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // GenerateUnits platformGenerator;
    // GenerateClouds cloudGenerator;
    // GenerateEnemies enemiesGenerator;
    // GenerateBoosts boostsGenerator;
    private Generator platformGenerator, cloudGenerator, beeGenerator, birdGenerator, twoBoostsGenerator, threeBoostsGenerator;
    [SerializeField] GameObject platform, cloud, bee, bird, twoBoost, threeBoost;
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
        for(int i = 1; i<= 9; i++){
            //NullReferenceExceptions
            cloudSprites.Add(Resources.Load<Sprite>(cloudResourcesPattern + i));
        }

        Debug.Log(cloudSprites.Count);
        Debug.Log(cloudSprites[8]);
    }

    void Start()
    {       //Get rid of warning here
        platformGenerator = new Generator(platform, startPoint.position, (int)((WorldOptions.screenSize.y * 2 + 1) / WorldOptions.JumpDistance) * WorldOptions.NumberOfGeneratingScreens,WorldOptions.JumpDistance - JUMP_OFFSET_Y, 100, 1f);
        platformGenerator.GenerateLayer();

        cloudGenerator = new Generator(cloud,startPoint.position, (int)((WorldOptions.screenSize.y * 2 + 1) / WorldOptions.CloudsFrequency) * WorldOptions.NumberOfGeneratingScreens,WorldOptions.CloudsFrequency, 100,1f);
        cloudGenerator.GenerateLayer();
        // platformGenerator = GetComponent<GenerateUnits>();
        // platformGenerator.GenerateLayer();
        // cloudGenerator = GetComponent<GenerateClouds>();
        // cloudGenerator.GenerateLayerOfClouds();
        // enemiesGenerator = GetComponent<GenerateEnemies>();
        // enemiesGenerator.GenerateLayerOfEnemies();
        // boostsGenerator = GetComponent<GenerateBoosts>();
        // boostsGenerator.GenerateLayerOfBoosts();
    }

    void Update()
    {
        if(player.transform.position.y >= platformGenerator.GenerationBound - GENERATION_OFFSET_Y)
        {
            platformGenerator.GenerateLayer();
        }
        
        if(player.transform.position.y >= cloudGenerator.GenerationBound - GENERATION_OFFSET_Y){
            cloud.GetComponent<SpriteRenderer>().sprite = cloudSprites[UnityEngine.Random.Range(0,9)];
            cloudGenerator.GenerateLayer();
        }

        // if(player.transform.position.y >= enemiesGenerator.GenerationBound - GENERATION_OFFSET_Y){
        //     enemiesGenerator.GenerateLayerOfEnemies();
        // }

        // if(player.transform.position.y >= boostsGenerator.GenerationBound - GENERATION_OFFSET_Y){
        //     boostsGenerator.GenerateLayerOfBoosts();
        // }

        if(player.transform.position.y <= Camera.main.transform.position.y - WorldOptions.screenSize.y - DEATHZONE_OFFSET_Y){
            SceneManager.LoadScene(0);
        }
    }
}
