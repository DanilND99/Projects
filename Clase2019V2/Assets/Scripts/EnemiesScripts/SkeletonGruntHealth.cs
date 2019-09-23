using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SkeletonGruntHealth : MonoBehaviour{
    [SerializeField] private int startingHealth = 20;
    [SerializeField] private float timeSinceLastHit = 0.5f;
    [SerializeField] private float dissapearSpeed = 2f;
    [SerializeField] private int currentHealth;
    private AudioSource audio;
    public AudioClip hurtAudio;
    public AudioClip deathAudio;
    private float timer = 0f;
    private Animator anim;
    private NavMeshAgent nav;
    private bool isAlive;
    private Rigidbody rigidbody;
    private CapsuleCollider capsuleCollider;
    private bool dissapearEnemy = false;
    private BoxCollider weaponCollider;
    public bool IsAlive{
        get{return isAlive;}
    }
    private DropItem dropItem;
    void Start(){
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        isAlive = true;
        currentHealth = startingHealth;
        weaponCollider = GetComponentInChildren<BoxCollider>();
        audio = GetComponent<AudioSource>();
        dropItem = GetComponent<DropItem>();
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
        if(currentHealth > 0){
            audio.PlayOneShot(hurtAudio);
            anim.Play("HurtFront");
            currentHealth -= 10;
        }
        if(currentHealth <= 0){
            isAlive = false;
            KillEnemy();
        }
    }
    void KillEnemy(){
        capsuleCollider.enabled = false;
        nav.enabled = false;
        audio.PlayOneShot(deathAudio);
        anim.SetTrigger("EnemyDie");
        GetComponent<Rigidbody>().isKinematic = true;
        weaponCollider.enabled = false;
        StartCoroutine(removeEnemy());
        dropItem.Drop();
    }
    IEnumerator removeEnemy(){
        yield return new WaitForSeconds(2f);
        dissapearEnemy = true;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
