using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerItem : MonoBehaviour{
    private GameObject player;
    private AudioSource audio;
    //private Invincible invincible;
    private PlayerHealth playerHealth;
    private ParticleSystem particleSystem;
    private MeshRenderer meshrenderer;
    private ParticleSystem brainParticles;
    private PowerItemExplode powerItemExplode;
    private SphereCollider sphereCollider;
    public GameObject pickupEffect;
    public AudioClip pickAudio;
    void Pickup(){
        Instantiate(pickupEffect,transform.position,transform.rotation);
    }
    void Start(){
        player = GameManager.instance.Player;
        playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.enabled = true;
        particleSystem = player.GetComponent<ParticleSystem>();
        particleSystem.enableEmission = false;
        meshrenderer = GetComponentInChildren <MeshRenderer>();
        brainParticles = GetComponent<ParticleSystem>();
        powerItemExplode = GetComponent<PowerItemExplode>();
        sphereCollider = GetComponent<SphereCollider>();
        audio = GetComponent<AudioSource>();
    }
    void Update(){
        
    }
    void OnTriggerEnter(Collider other){
        if(other.gameObject == player){
            StartCoroutine(InvincibleRoutine());
            meshrenderer.enabled = false;
        }
    }
    public IEnumerator InvincibleRoutine(){
        print("pick invincible");
        audio.PlayOneShot(pickAudio);
        powerItemExplode.Pickup();
        particleSystem.enableEmission = true;
        playerHealth.enabled = false;
        brainParticles.enableEmission = false;
        sphereCollider.enabled = false;
        yield return new WaitForSeconds(10f);
        print("no more invincible");
        particleSystem.enableEmission = false;
        playerHealth.enabled = true;
        Destroy(gameObject);
    }
}
