using UnityEngine;

public class Cloud : MonoBehaviour
{
    private const float CAMERA_OFFSET_Y = 10f;
    void Update()
    {
        if(transform.position.y <= Camera.main.transform.position.y - CAMERA_OFFSET_Y)
        {
            Destroy(this.gameObject);
        }
    }
}
