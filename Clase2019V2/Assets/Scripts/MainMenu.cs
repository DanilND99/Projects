﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour{
    public void QuitGame(){
        print("Quit");
        Application.Quit();
    }

    public void PlayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void Start(){
        
    }
    void Update(){
        
    }
}
