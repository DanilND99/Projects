﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDestruction : MonoBehaviour{
    public float lifeSpan = 2.0f;
    private AudioSource audio;
    public AudioClip shootAudio;
    void Start(){
        audio = GetComponent<AudioSource>();
        audio.PlayOneShot(shootAudio);
        Destroy(gameObject,lifeSpan);
    }
    void Update(){
        
    }
    void OnTriggerEnter(Collider other){
        if(other.gameObject){
            Destroy(this.gameObject);
        }
    }
}
