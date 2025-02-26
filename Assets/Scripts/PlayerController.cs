using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    Rigidbody2D rb;
    Animator anim; 
    SpriteRenderer _renderer;
    float inputX;
    Vector3 targetPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
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
    }

    public void Die()
    {
        GetComponent<Collider2D>().enabled = false;
        rb.linearVelocityX = 0;
        this.enabled = false;
    }

    public void Jump(float distance)
    {
        rb.linearVelocityY = distance;
        
        StartCoroutine(PlayAnimation());
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
