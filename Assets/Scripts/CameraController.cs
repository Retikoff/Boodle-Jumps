using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject target;

    float highestPoint = 0f;
    Vector3 targetPos;
    Vector3 velocity;
    //public?
    float smoothTime = 0.3f;

    void Update()
    {
        if(target.transform.position.y > transform.position.y){
            highestPoint = target.transform.position.y;
            targetPos = new Vector3(transform.position.x,highestPoint, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position,targetPos,ref velocity,smoothTime);
        }
    }

}
