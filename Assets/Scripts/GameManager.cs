using System.Collections.Generic;
using System.Linq;
using TMPro;
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
    [SerializeField] Transform startPointForEnvironment;
    [SerializeField] Transform startPointForInteractables;
    [SerializeField] TextMeshProUGUI scoreLabel;
    [SerializeField] Canvas DeathScreen;
    
    private int highScore = 0;
    private List<Sprite> cloudSprites;
    private const float  GENERATION_OFFSET_Y = 7f;
    private const float DEATHZONE_OFFSET_Y = 1.5f; 
    private const float JUMP_OFFSET_Y = 0.35f;

    void Awake()
    {
        WorldOptions.screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width,Screen.height));
        print(SystemInfo.graphicsDeviceName);
        cloudSprites = new List<Sprite>();
        for(int i = 1; i <= 9; i++){
            cloudSprites.Add(Resources.Load<Sprite>("Cloud" + i));
        }
        DeathScreen.enabled = false;
    }

    void Start()
    {       
        platformGenerator = Generator.CreateGenerator(platform, startPointForEnvironment.position, (int)((WorldOptions.screenSize.y * 2 + 1) / WorldOptions.JumpDistance) * WorldOptions.NumberOfGeneratingScreens,WorldOptions.JumpDistance - JUMP_OFFSET_Y, platformChanceToSpawn, 1f);
        platformGenerator.GenerateLayer();

        cloudGenerator = Generator.CreateGenerator(cloud,startPointForEnvironment.position, (int)((WorldOptions.screenSize.y * 2 + 1) / WorldOptions.CloudsFrequency) * (WorldOptions.NumberOfGeneratingScreens - 1),WorldOptions.CloudsFrequency, cloudChanceToSpawn, 2f, true, cloudSprites);
        cloudGenerator.GenerateLayer();

        beeGenerator = Generator.CreateGenerator(bee, startPointForInteractables.position, beeCount, WorldOptions.screenSize.y, beeChanceToSpawn, WorldOptions.screenSize.y);
        beeGenerator.GenerateLayer();

        birdGenerator = Generator.CreateGenerator(bird, startPointForInteractables.position, birdCount, WorldOptions.screenSize.y, birdChanceToSpawn, WorldOptions.screenSize.y * 2);
        birdGenerator.GenerateLayer();

        boostTwoGenerator = Generator.CreateGenerator(boostTwo, startPointForInteractables.position, boostTwoCount, WorldOptions.screenSize.y, boostTwoChanceToSpawn, WorldOptions.screenSize.y * 2);
        boostTwoGenerator.GenerateLayer();

        boostThreeGenerator = Generator.CreateGenerator(boostThree, startPointForInteractables.position, boostThreeCount, WorldOptions.screenSize.y, boostThreeChanceToSpawn, WorldOptions.screenSize.y * 5);
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

        if(player.transform.position.y > highScore){
            highScore = (int)player.transform.position.y;
            scoreLabel.text = highScore.ToString();
        }

        if(player.transform.position.y <= Camera.main.transform.position.y - WorldOptions.screenSize.y - DEATHZONE_OFFSET_Y){
            player.Freeze();
            DeathScreen.enabled = true;
        }
    }

    public void RestartGame(){
        SceneManager.LoadScene(0);
    }

    private void OnGUI() // only for debug 
    {
        if (GUI.Button(new Rect(0, 0, 100, 50), "Jump"))
        {
            player.Jump(10f);
        }

        if(GUI.Button(new Rect(110,0,100,50), "DeathScreen")){
            DeathScreen.enabled = !DeathScreen.isActiveAndEnabled;
        }
    }
}
