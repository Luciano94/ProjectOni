﻿using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private OptionsController optionsPanel;
    [SerializeField] private Button firstOption;
    [SerializeField] private Button controlButton;

    private Animator pauseAnim;
    private bool pauseState = false;
    
    private void Awake(){
        pauseAnim = pausePanel.GetComponent<Animator>();
    }

    private void Update(){
        if(InputManager.Instance.GetPauseButton()){
            pauseState = !pauseState;
            MenuManager.Instance.StartMenu = pauseState;
            
            if (!pauseState && optionsPanel.gameObject.activeSelf)
                optionsPanel.DesactivateThis();

            if (pauseState){
                firstOption.Select();
                pauseAnim.SetTrigger("In");
            }
            else{
                controlButton.Select();
                pauseAnim.SetTrigger("Out");
            }
        }
    }
}
