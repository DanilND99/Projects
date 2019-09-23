using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpItem : MonoBehaviour{
    private GameObject player;
    private CharacterMovement characterMovement;
    private SphereCollider sphereCollider;
    private SpriteRenderer spriteRenderer;
    private ParticleSystem particleSystem;
    private AudioSource audio;
    public AudioClip pickAudio;
    private PowerItemExplode powerItemExplode;
    void Start(){
        audio = GetComponent<AudioSource>();
        player = GameManager.instance.Player;
        characterMovement = player.GetComponent<CharacterMovement>();
        particleSystem = player.GetComponent<ParticleSystem>();
        sphereCollider = GetComponent<SphereCollider>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        powerItemExplode = GetComponent<PowerItemExplode>();
    }
    void Update(){
        
    }
    void OnTriggerEnter(Collider other){
        if(other.gameObject == player){
            StartCoroutine(jumpRoutine());
        }
    }
    public IEnumerator jumpRoutine(){
        sphereCollider.enabled = false;
        spriteRenderer.enabled = false;
        particleSystem.enableEmission = true;
        audio.PlayOneShot(pickAudio);
        powerItemExplode.Pickup();
        characterMovement.jumpSpeed = 600.0f;
        yield return new WaitForSeconds(10f);
        characterMovement.jumpSpeed = 300.0f;
        particleSystem.enableEmission = false;
        Destroy(gameObject);
    }
}
