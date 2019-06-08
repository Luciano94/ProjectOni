﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject optionsPanel;

    private bool pauseState = false;
    
    private void Update(){
        if(Input.GetButtonDown("Pause")){
            Debug.Log(pauseState);   
            pauseState = !pauseState;
            MenuManager.Instance.StartMenu = pauseState;
            if (!pauseState && optionsPanel.activeSelf){
                UIManager.Instance.ChangeState(optionsPanel);
            }
            pausePanel.SetActive(pauseState);
        }
    }
}
