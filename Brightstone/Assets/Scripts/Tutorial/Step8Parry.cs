﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step8Parry : Step{
    [SerializeField] private ComboManager comboManager;
    [SerializeField] private EnemyStats enemyStats;
    [SerializeField] private string[] initialTexts;
    [SerializeField] private string[] middleTexts;
    
    private PlayerStats playerStats;
    private int textIndex = 0;
    private int parriedTimes = 0;
    private int parryTotal = 3;
    private bool firstDialogueFinished = false;

    public override void StepInitialize(){
        playerStats = GameManager.Instance.playerSts;
        playerStats.OnHit.AddListener(PlayerHit);
        enemyStats.OnParried.AddListener(EnemyParried);
        enemyStats.GetComponent<EnemyMovement>().SetSpeed(10.0f);

        TextGenerator.Instance.Appear();
        TextGenerator.Instance.Show(initialTexts[textIndex]);
        GameManager.Instance.DisablePlayer();
    }

    public override void StepFinished(){
        
    }

    public override void StepUpdate(){
        if (!firstDialogueFinished){
            if (InputManager.Instance.GetPassButton()){
                textIndex++;
                if (textIndex < initialTexts.Length){
                    TextGenerator.Instance.Show(initialTexts[textIndex]);
                }
                else{
                    TextGenerator.Instance.Hide();
                    GameManager.Instance.EnablePlayer();
                    textIndex = 0;
                    firstDialogueFinished = true;
                }
            }
            return;
        }

        float hpDiff = playerStats.MaxLife() - playerStats.Life;
        if (hpDiff > 0){
            playerStats.Life = -hpDiff;
        }

        if (enemyStats){
            float eHpDiff = enemyStats.MaxLife() - enemyStats.Life;
            if (comboManager.ActualComboIndex() == 0 && eHpDiff != 0)
                enemyStats.Life = -eHpDiff;
        }

        if (parriedTimes < parryTotal) return;

        if (textIndex < middleTexts.Length){
            if (InputManager.Instance.GetPassButton()){
                textIndex++;
                if (textIndex < middleTexts.Length){
                    TextGenerator.Instance.Show(middleTexts[textIndex]);
                }
                else{
                    TextGenerator.Instance.Hide();
                    GameManager.Instance.EnablePlayer();
                }
            }
            return;
        }

        finished = true;
    }

    private void PlayerHit(){
        parriedTimes = 0;
    }

    private void EnemyParried(){
        parriedTimes++;
        
        if (parriedTimes >= parryTotal){
            Invoke("DestroyEnemy", 1.5f);
            GameManager.Instance.DisablePlayer();
            TextGenerator.Instance.Appear();
            TextGenerator.Instance.Show(middleTexts[textIndex]);
        }
    }

    private void DestroyEnemy(){
        Destroy(enemyStats.gameObject);
    }
}