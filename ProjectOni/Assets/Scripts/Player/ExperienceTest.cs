﻿using UnityEngine;
using UnityEngine.UI;
public class ExperienceTest : MonoBehaviour
{
    private ExperienceMarket expMrk;
    private PlayerStats plStats;
    [SerializeField]private bool isHit = false;

    private void Start() {
        expMrk = ExperienceMarket.Instance;
        plStats = GameManager.Instance.playerSts;
    }

    private void Update() {
        if(isHit)
            LevelUp();
    }

    private void LevelUp(){
        if(Input.GetKeyUp(KeyCode.Keypad1)){
            expMrk.LifeUp();
        }
        if(Input.GetKeyUp(KeyCode.Keypad2)){
            expMrk.AtkUp();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        isHit = true;
        UIManager.Instance.EnterMarket();
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        isHit = false;
        UIManager.Instance.ExitMarket();
    }
}
