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
    Vector2 screenSize;
    float inputX;
    Vector3 targetPos;

    private const float DEATHZONE_OFFSET_Y = 1.5f; 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();

        screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width,Screen.height));
    }

    private void Update()
    {
        inputX = Input.GetAxis("Horizontal") * speed;
        rb.linearVelocityX = inputX;

        _renderer.flipX = inputX != 0 ? inputX < 0 : _renderer.flipX;
        //maybe this code doesnt effect at all
        if(transform.rotation.x != 0)
        {
            transform.eulerAngles = new Vector3(0, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }

        if(transform.position.x < -screenSize.x)
        {
            transform.position = new Vector3(screenSize.x, transform.position.y, 0);
        }

        if(transform.position.x > screenSize.x)
        {
            transform.position = new Vector3(-screenSize.x, transform.position.y, 0);
        }

        if(transform.position.y < Camera.main.transform.position.y - screenSize.y - DEATHZONE_OFFSET_Y)
        {
            Die();
        }
    }

    public void Die()
    {
        SceneManager.LoadScene(0);
    }

    public void Jump(float distance)
    {
        //Debug.Log("Player have jumped for " + distance + " meters");
        rb.linearVelocityY = distance;
        
        StartCoroutine(PlayAnimation());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IInteractable objectScript = collision.transform.gameObject.GetComponent<IInteractable>();
        
        objectScript?.Perform(); // if(objectScript != null) Perform()

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
