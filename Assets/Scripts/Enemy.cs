using UnityEditor.Callbacks;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const float CAMERA_OFFSET_Y = 10f;
    private const float MOVEMENT_OFFSET_X = 1f;
    private float direction;
    
    void Start()
    {
        direction = Mathf.Sign(transform.position.x) ;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        player?.Die();
    }

    void Update()
    {
        if(transform.position.y < Camera.main.transform.position.y - CAMERA_OFFSET_Y){
            Destroy(this.gameObject);
        }

        if(transform.position.x > WorldOptions.screenSize.x - MOVEMENT_OFFSET_X || transform.position.x < -WorldOptions.screenSize.x + MOVEMENT_OFFSET_X){
            direction *= -1;
        }

        if(gameObject.name == "Bird(Clone)" ){
            GetComponent<Rigidbody2D>().linearVelocityX = WorldOptions.BirdSpeed * direction;
        }

        GetComponent<SpriteRenderer>().flipX = direction > 0 ;
    }
}
