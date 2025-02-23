using UnityEngine;

public class Platform : MonoBehaviour,IInteractable
{
    PlayerController player;
    private const float CAMERA_OFFSET_Y = 10f;

    public void Perform(){
        player.Jump(WorldOptions.JumpHeight);
    }

    //too hard connection?
    public void SetPlayer(PlayerController player){  // should be called from generation script 
        this.player = player;
    }

    void Update()
    {
        if(transform.position.y < Camera.main.transform.position.y - CAMERA_OFFSET_Y){
            Destroy(this.gameObject);
        }
    }
}
