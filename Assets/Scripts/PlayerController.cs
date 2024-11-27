
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Controls { mobile,pc}

public class PlayerController : MonoBehaviour
{
    public bool sadfa;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float doubleJumpForce = 8f;
    public LayerMask groundLayer;
    public Transform groundCheck;

    private Rigidbody2D rb;

    public Animator playeranim;

    public Controls controlmode;
    private AudioSource audioSource;
   

    private float moveX;
    public bool isPaused = false;

    public ParticleSystem footsteps;
    private ParticleSystem.EmissionModule footEmissions;

    public ParticleSystem ImpactEffect;

   // public GameObject projectile;
   // public Transform firePoint;

    public float fireRate = 0.5f; // Time between each shot
    private float nextFireTime = 0f; // Time of the next allowed shot


    




    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        footEmissions = footsteps.emission;
        audioSource = GetComponent<AudioSource>();
        

    }

    private void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);
        
        sadfa = IsGrounded();
        SetAnimations();
        if (IsGrounded())
        {
            
            if (Input.GetButtonDown("Jump"))
            {
                Jump(jumpForce);
                
            }
            if (moveX != 0)
            {
             FlipSprite(moveX);
             footsteps.Play();
             if(audioSource.isPlaying==false) audioSource.Play();
             
            }
            else
            {
                audioSource.Stop();
            }
        }
        else
            {
                audioSource.Stop();
            }
        
        

        
    }
    public void SetAnimations()
    {
        if (moveX != 0 && IsGrounded())
        {
            playeranim.SetBool("run", true);
            footEmissions.rateOverTime= 35f;
        }
        else
        {
            playeranim.SetBool("run",false);
            footEmissions.rateOverTime = 0f;
        }

       
    }

    private void FlipSprite(float direction)
    {
        if (direction > 0)
        {
            // Moving right, flip sprite to the right
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction < 0)
        {
            // Moving left, flip sprite to the left
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void FixedUpdate()
    {
        // Player movement
        
        
            
    }

    private void Jump(float jumpForce)
    {
        
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // Zero out vertical velocity
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        playeranim.SetTrigger("jump");
        
        ImpactEffect.Play();
    }

    private bool IsGrounded()
    {
        float rayLength = 0.25f;
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, -groundCheck.up, rayLength, groundLayer);
        return hit.collider != null;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "killzone")
        {
            GameManager.instance.Death();
        }
    }
  
}