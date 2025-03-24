using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    Rigidbody2D rb;
    private Collider2D _collider;
    Animator anim; 
    SpriteRenderer _renderer;
    float inputX;
    Vector3 targetPos;
    private int ForceLayers = 384; //bitMask for Enemies, Platform (1100 0000)x0 = 192 * 2 = 384
    private int ForceLayersWhileFlying = 256; //bitMask for Enemies (1000 0000)x0 = 128 * 2 = 256
    private int ContactLayers = 448; //bitMask for Interactable, Platform, Enemies (1110 0000)x0 = 224 * 2 = 448
    private int ContactLayersWhileFlying = 320; //bitMask for Interactable, Enemies (1010 0000)x0 = 160 * 2 = 320 
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        inputX = Input.GetAxis("Horizontal") * speed;
        rb.linearVelocityX = inputX;

        _renderer.flipX = inputX != 0 ? inputX < 0 : _renderer.flipX;
        //maybe this code doesnt effect at all
        //lock x rotation
        if(transform.rotation.x != 0)
        {
            transform.eulerAngles = new Vector3(0, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }

        //teleport to another bound 
        if(transform.position.x < -WorldOptions.screenSize.x)
        {
            transform.position = new Vector3(WorldOptions.screenSize.x, transform.position.y, 0);
        }
        //teleport to another bound 
        if(transform.position.x > WorldOptions.screenSize.x)
        {
            transform.position = new Vector3(-WorldOptions.screenSize.x, transform.position.y, 0);
        }

        CheckForPickHeight();
    }

    public void Die()
    {
        _collider.enabled = false;
        rb.linearVelocityX = -0.5f;
        rb.linearVelocityX = 0;
        this.enabled = false;
    }

   
    public void Jump(float distance)
    {
        rb.linearVelocityY = distance;
        
        StartCoroutine(PlayAnimation());
    }

    public void DoNotCollide(){
        _collider.forceReceiveLayers = ForceLayersWhileFlying;
        _collider.contactCaptureLayers = ContactLayersWhileFlying;
    }

    private void CheckForPickHeight(){
        if(_collider.contactCaptureLayers == ContactLayersWhileFlying && rb.linearVelocityY <= 0){
            _collider.forceReceiveLayers = ForceLayers;
            _collider.contactCaptureLayers = ContactLayers;
        }
    }

    private IEnumerator PlayAnimation()
    {
        anim.SetBool("inAir", false);

        yield return new WaitForEndOfFrame();

        anim.SetBool("inAir", true);
    }
    
    private void OnGUI() // only for debug 
    {
        if (GUI.Button(new Rect(0, 0, 100, 50), "SetHigh"))
        {
            targetPos = new Vector3(transform.position.x, transform.position.y + 10f, transform.position.z);
            transform.position = targetPos;
            
        }

        if (GUI.Button(new Rect(110, 0, 100, 50), "Jump"))
        {
            this.Jump(10f);
        }
    }
}
