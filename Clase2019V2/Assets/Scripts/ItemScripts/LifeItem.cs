using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeItem : MonoBehaviour{
    private GameObject player;
    private LifeManager lifeManager;
    private SpriteRenderer spriteRenderer;
    public GameObject pickUpEffect;
    private PowerItemExplode powerItemExplode;
    private BoxCollider boxCollider;
    private AudioSource audio;
    public AudioClip pickAudio;
    void Start(){
        player = GameManager.instance.Player;
        lifeManager = FindObjectOfType<LifeManager>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        powerItemExplode = GetComponent<PowerItemExplode>();
        boxCollider = GetComponent<BoxCollider>();
        audio = GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider other){
        if(other.gameObject == player){
            PickLife();
            print("Life Collected");
        }
    }
    public void PickLife(){
        lifeManager.GiveLife();
        audio.PlayOneShot(pickAudio);
        powerItemExplode.Pickup();
        spriteRenderer.enabled = false;
        boxCollider.enabled = false;
        Destroy(gameObject);
    }
    void Update(){
        
    }
}
