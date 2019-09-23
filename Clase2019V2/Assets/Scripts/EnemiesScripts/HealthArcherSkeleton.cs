using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthArcherSkeleton : MonoBehaviour
{
    [SerializeField] private int startingHealth = 20;
    [SerializeField] private float timeSinceLastHit = 0.5f;
    [SerializeField] private float dissapearSpeed = 2f;
    [SerializeField] private int currentHealth;
    private float timer = 0f;
    private Animator anim;
    private bool isAlive;
    private Rigidbody rigidbody;
    private CapsuleCollider capsuleCollider;
    private bool dissapearEnemy = false;
    private AudioSource audio;
    public AudioClip hurtAudio;
    public AudioClip killAudio;
    private DropItem dropItems;
    public bool IsAlive{
        get{return isAlive;}
    }
    void Start(){
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
        isAlive = true;
        currentHealth = startingHealth;
        audio = GetComponent<AudioSource>();
        dropItems = GetComponent<DropItem>();
    }
    void Update(){
        timer += Time.deltaTime;
        if(dissapearEnemy){
            transform.Translate(-Vector3.up*dissapearSpeed*Time.deltaTime);
        }
    }
    void OnTriggerEnter(Collider other){
        if(timer >= timeSinceLastHit && !GameManager.instance.GameOver){
            if(other.tag == "PlayerWeapon"){
                takeHit();
                timer = 0f;
            }
        }
    }
    void takeHit(){
        if (currentHealth > 0){
            audio.PlayOneShot(hurtAudio);
            anim.Play("GetHitFront");
            currentHealth -= 10;
        }
        if(currentHealth <= 0){
            isAlive = false;
            KillEnemy();
        }
    }
    void KillEnemy(){
        capsuleCollider.enabled = false;
        audio.PlayOneShot(killAudio);
        anim.SetTrigger("EnemyDie");
        rigidbody.isKinematic = true;
        StartCoroutine(removeEnemy());
        dropItems.Drop();
    }
    IEnumerator removeEnemy(){
        yield return new WaitForSeconds(2f);
        dissapearEnemy = true;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

}
