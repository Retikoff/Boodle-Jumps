using UnityEngine;

public class GameManager : MonoBehaviour
{
    GenerateUnits generator;
    [SerializeField] PlayerController player;
    private const float  GENERATION_OFFSET_Y = 7f;
    void Start()
    {
        generator = GetComponent<GenerateUnits>();
        generator.GenerateLayer();
    }

    void Update()
    {
        if(player.transform.position.y >= generator.GenerationBound - GENERATION_OFFSET_Y){
            generator.GenerateLayer();
        }
    }
}
