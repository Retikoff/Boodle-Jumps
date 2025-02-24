using UnityEngine;

public class GameManager : MonoBehaviour
{
    GenerateUnits platformGenerator;
    GenerateClouds cloudGenerator;
    [SerializeField] PlayerController player;
    private const float  GENERATION_OFFSET_Y = 7f;

    void Awake()
    {
        WorldOptions.screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width,Screen.height));
    }

    void Start()
    {
        platformGenerator = GetComponent<GenerateUnits>();
        platformGenerator.GenerateLayer();
        cloudGenerator = GetComponent<GenerateClouds>();
        cloudGenerator.GenerateLayerOfClouds();
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
    }
}
