﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsBehaviour : MonoBehaviour{

    [SerializeField]private int enemiesCant = 5;
    [SerializeField]private bool haveMarket = false;
    [SerializeField]private bool haveBoss = false;
    [SerializeField]private GameObject marketPrefab;
    [SerializeField]private GameObject bossPrefab;
    [SerializeField]private GameObject[] enemyPrefab;
    [SerializeField]private Transform[] enemySpawns;

    private Node node;
    private SpriteRenderer mapNode;
    private Vector3 pos;

    private List<GameObject> enemies = null;
    private GameObject market = null;   
    private int enemiesLeft;

    private bool isComplete = false;

    public void SetMapNode(SpriteRenderer spr, Color color){
        mapNode = spr;
        mapNode.color = color;
    }

    public void SetColorNode(Color c){
        if(mapNode != null)
            mapNode.color = c;
    }

    public bool Complete{
        get{return isComplete;}
    }

    public Node Node{
        set{
            node = value;
            setRoom();
        }
    }

    public bool HaveMarket{
        get{return haveMarket;}
    }

    public bool HaveBoss{
        get{return haveBoss;}
    }

    public NodeBehaviour NodeBehaviour{
        get{return node.Behaviour;}
    }

    public void setEnemiesRoom(){
        if(node.Behaviour == NodeBehaviour.Normal){
            enemiesCant = Random.Range(EnemyDirector.Instance.getMinDifficultValue(), 
                                        EnemyDirector.Instance.getMaxDifficultValue());
            enemiesLeft = enemiesCant;
            for(int i = 0; i < enemiesCant; i++){
                pos = enemySpawns[Random.Range(0, enemySpawns.Length)].position;
                pos.z = GameManager.Instance.PlayerPos.z;
                enemies.Add(Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)], pos, transform.rotation));
                enemies[i].GetComponent<EnemyStats>().MyRoom = gameObject;
                enemies[i].SetActive(false);
            }
            if(EnemyDirector.Instance.roomIndex == 0){
                EnemyDirector.Instance.startFirstTime();
            }else{
                EnemyDirector.Instance.startControlTime(enemiesCant);
            }
        }
    }

    public void setRoom() {
        enemies = new List<GameObject>();
        switch(node.Behaviour){
            case NodeBehaviour.Normal:
               /* enemiesCant = Random.Range(1, enemiesCant);
                enemiesLeft = enemiesCant;
                for(int i = 0; i < enemiesCant; i++){
                    pos = enemySpawns[Random.Range(0, enemySpawns.Length)].position;
                    pos.z = GameManager.Instance.PlayerPos.z;
                    enemies.Add(Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)], pos, transform.rotation));
                    enemies[i].GetComponent<EnemyStats>().MyRoom = gameObject;
                    enemies[i].SetActive(false);
                }*/
            break;
            case NodeBehaviour.Market:
                haveMarket = true;
                pos = transform.position;
                pos.z = GameManager.Instance.PlayerPos.z;
                market = Instantiate(marketPrefab, pos, transform.rotation);
                if (!GameManager.Instance.isTutorial){
                    isComplete = true;
                    market.layer = 11;
                }
            break;
            case NodeBehaviour.MediumBoss:
            break;
            case NodeBehaviour.Boss:
                enemiesLeft = 1;
                haveBoss = true;
                pos = enemySpawns[Random.Range(0, enemySpawns.Length)].position;
                pos.z = GameManager.Instance.PlayerPos.z;
                enemies.Add(Instantiate(bossPrefab, pos, transform.rotation));
                enemies[0].GetComponent<EnemyStats>().MyRoom = gameObject;
                enemies[0].SetActive(false);
                GameManager.Instance.SetBoss = enemies[0];
            break;
            case NodeBehaviour.Tutorial:
                //isComplete = true;
            break;
        }
    }

    public void ActiveEnemies(){
        if(!haveMarket){
            foreach (GameObject enemy in enemies)
                enemy.SetActive(true);
            
            EnemyBehaviour.Instance.FillEnemyList();

            if(enemies[0].GetComponent<EnemyStats>().enemyType == EnemyType.Boss)
                UIManager.Instance.InitBoss();
        }else{
            SwitchMarket();
        }
    }

    public void SwitchMarket(){
        if(GameManager.Instance.isTutorial){
            market.layer = 11;
        }else{
            market.layer = 11;
        }
    }

    public void EnemyDeath(EnemyBase thisEnemy){
        enemiesCant--;
        EnemyBehaviour.Instance.onEnemyDeath(thisEnemy);
        if(enemiesCant <= 0){
            GetComponent<NodeExits>().OpenDoors();
            isComplete = true;
            //AudioManager.Instance.RoomFinished();
            SoundManager.Instance.RoomClear();
            if(EnemyDirector.Instance.roomIndex == 0){
                EnemyDirector.Instance.stopFirstTime();
            }else{
                EnemyDirector.Instance.stopControlTime();
            }
        }
    }

    public void RoomFinished(){
        isComplete = true;
    }

    public List<EnemyBase> GetEnemies(){
        List<EnemyBase> enemyList = new List<EnemyBase>();
        foreach(GameObject enemy in enemies){
            if(enemy != null){
                enemyList.Add(enemy.GetComponent<EnemyBase>());
            }
        }
        return enemyList;
    }
}
