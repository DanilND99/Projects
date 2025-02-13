﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour{
    
    public static GameManager instance = null;
    [SerializeField] GameObject player;
    private bool gameOver = false;
    public bool GameOver{
        get{return gameOver;}
    }
    public GameObject Player{
        get{return player;}
    }
    void Awake(){
        if(instance == null){
            instance = this;
        }else if(instance != this){
            Destroy(gameObject);
        }
    }
    void Start(){
        
    }
    void Update(){
        
    }
    public void PlayerHit(int currentHP){
        if(currentHP > 0){
            gameOver = false;
        }else{
            gameOver = true;
        }
    }
}
