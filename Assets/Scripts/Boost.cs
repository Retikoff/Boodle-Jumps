using UnityEngine;

public class Boost : MonoBehaviour
{
    [SerializeField] int boostPower;
    private const float CAMERA_OFFSET_Y = 10f;
    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();

        player?.Jump(boostPower * WorldOptions.JumpHeight);
    }

    void Update()
    {
        if(transform.position.y < Camera.main.transform.position.y - CAMERA_OFFSET_Y)
        {
            Destroy(this.gameObject);
        }
    }
}
