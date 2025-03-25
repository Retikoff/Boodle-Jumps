using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GenerateUnits platformGenerator;
    GenerateClouds cloudGenerator;
    GenerateEnemies enemiesGenerator;
    GenerateBoosts boostsGenerator;
    [SerializeField] PlayerController player;
    private const float  GENERATION_OFFSET_Y = 7f;
    private const float DEATHZONE_OFFSET_Y = 1.5f; 

    void Awake()
    {
        WorldOptions.screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width,Screen.height));
        print(SystemInfo.graphicsDeviceName);
    }

    void Start()
    {
        platformGenerator = GetComponent<GenerateUnits>();
        platformGenerator.GenerateLayer();
        cloudGenerator = GetComponent<GenerateClouds>();
        cloudGenerator.GenerateLayerOfClouds();
        enemiesGenerator = GetComponent<GenerateEnemies>();
        enemiesGenerator.GenerateLayerOfEnemies();
        boostsGenerator = GetComponent<GenerateBoosts>();
        boostsGenerator.GenerateLayerOfBoosts();
    }

    void Update()
    {
        if(player.transform.position.y >= platformGenerator.GenerationBound - GENERATION_OFFSET_Y)
        {
            platformGenerator.GenerateLayer();
        }
        
        if(player.transform.position.y >= cloudGenerator.GenerationBound - GENERATION_OFFSET_Y){
            cloudGenerator.GenerateLayerOfClouds();
        }

        if(player.transform.position.y >= enemiesGenerator.GenerationBound - GENERATION_OFFSET_Y){
            enemiesGenerator.GenerateLayerOfEnemies();
        }

        if(player.transform.position.y >= boostsGenerator.GenerationBound - GENERATION_OFFSET_Y){
            boostsGenerator.GenerateLayerOfBoosts();
        }

        if(player.transform.position.y <= Camera.main.transform.position.y - WorldOptions.screenSize.y - DEATHZONE_OFFSET_Y){
            SceneManager.LoadScene(0);
        }
    }
}
