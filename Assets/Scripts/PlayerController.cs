using System.Collections;
using UnityEngine;

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
        if(transform.rotation.x != 0){
            transform.eulerAngles = new Vector3(0,transform.localEulerAngles.y,transform.localEulerAngles.z);
        }
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
            anim.SetBool("inAir", true);
        }

        if (GUI.Button(new Rect(110, 0, 100, 50), "Jump"))
        {
            this.Jump(10f);
            anim.SetBool("inAir", true);
        }
    }


}
