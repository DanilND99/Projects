using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class CharacterMovement : MonoBehaviour
{
    public float maxSpeed = 6.0f;
    public bool facingRight = true;
    public float moveDirection;
    new Rigidbody rigidbody;
    public float jumpSpeed = 300.0f;
    private Animator anim;
    public bool grounded = false;
    public Transform groundCheck;
    public float groundRadious = 0.2f;
    public LayerMask whatIsGround;
    public float swordSpeed = 600.0f;
    public Transform swordSpawn;
    public Rigidbody swordPrefab;
    private AudioSource audio;
    public AudioClip jumpAudio;
    public AudioClip attackAudio;
    private ParticleSystem particleSystem;
    Rigidbody clone;
    void Awake(){
        groundCheck = GameObject.Find("GroundCheck").transform;
        swordSpawn = GameObject.Find("SwordSpawn").transform;
    }
    void Start(){
        rigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        particleSystem = GetComponent<ParticleSystem>();
        particleSystem.enableEmission = false;
    }
    void Update(){
        moveDirection = CrossPlatformInputManager.GetAxis("Horizontal");
        if(grounded && CrossPlatformInputManager.GetButtonDown("Jump")){
            anim.SetTrigger("isJumping");
            audio.PlayOneShot(jumpAudio);
            rigidbody.AddForce(new Vector2(0,jumpSpeed));
        }
        if(CrossPlatformInputManager.GetButtonDown("Fire1")){
            Attack();
        }
        if(moveDirection > 0.0f && !facingRight){
            Flip();
        }else if(moveDirection < 0.0f && facingRight){
            Flip();
        }
        anim.SetFloat("Speed", Mathf.Abs(moveDirection));
    }
    private void FixedUpdate(){
        grounded = Physics2D.OverlapCircle(groundCheck.position,groundRadious,whatIsGround);
        rigidbody.velocity = new Vector2(moveDirection * maxSpeed, rigidbody.velocity.y);
    }
    void Flip(){
        facingRight = !facingRight;
        transform.Rotate(Vector3.up, 180.0f, Space.World);
    }
    public void CallFireProjectile(){
        clone = Instantiate(swordPrefab,swordSpawn.position,swordSpawn.rotation) as Rigidbody;
        clone.AddForce(swordSpawn.transform.right * swordSpeed);
    }
    void Attack(){
        audio.PlayOneShot(attackAudio);
        anim.SetTrigger("attacking");
    }
}
