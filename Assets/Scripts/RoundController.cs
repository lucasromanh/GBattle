﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundController : MonoBehaviour
{
    public HealthController player1H;
    private HealthController player2H;

    public HUDController hud;

    private bool theresAWinner;

    public GameObject[] enemies;

    private GameManager gm;
    
    private bool player1Win;
    private bool player2Win;

    void Start(){
        gm = GameManager.Instance;
        EnemyInRoundControl();
        RoundControl(gm.GetRoundNumber());
        theresAWinner = false;
        player1Win = false;
        player2Win = false;
    }

    private void EnemyInRoundControl(){
        int levelNumber = gm.GetLevelNumber();
        switch(levelNumber){
            case 1:
                enemies[1].SetActive(true);
                SetPlayer2HealthController(enemies[1]);
                break;
            case 2:
                enemies[2].SetActive(true);
                SetPlayer2HealthController(enemies[2]);
                break;
            case 3:
                enemies[0].SetActive(true);
                SetPlayer2HealthController(enemies[0]);
                break;
        }
    }
    private void RoundControl(int message){
        switch(message){
            case 0:
                hud.messages[0].SetActive(true);
                break;
            case 1:
                hud.messages[1].SetActive(true);
                break;
            case 2:
                hud.messages[2].SetActive(true);
                break;
            case 3:
                hud.messages[3].SetActive(true);
                break; 
            case 4:
                hud.messages[4].SetActive(true);
                break;
        }
    }

    void Update(){
        if(hud.GetCurrentNumber() == 0 || theresAWinner)
            StartCoroutine(LoadRound());
        else
            LifeControl();
    }

    IEnumerator LoadRound(){
        if(player1H.GetCurrentHealth()>player2H.GetCurrentHealth()){
            RoundControl(3);
            player1Win = true;
        }
        else{ 
            RoundControl(4);          
            player2Win = true;
        }
        
        yield return new WaitForSeconds(3);
        if(player1Win)
            gm.IncreaseRoundWonPlayer1();
        else
            gm.IncreaseRoundWonPlayer2();        

        if(gm.GetRoundNumber() < 1 || (gm.GetRoundWonPlayer1()==1 && gm.GetRoundWonPlayer2()==1)){
            gm.IncreaseNumberOfRounds();
            SceneManager.LoadScene(3);
        }
        else{
            if(gm.GetRoundWonPlayer1()>1){
                if (gm.GetLevelNumber()>2){
                    gm.ResetRoundNumber();
                    gm.resetRoundWonPlayer1();
                    gm.resetRoundWonPlayer2();
                    gm.ResetLevelNumber();
                    SceneManager.LoadScene(7);
                }
                else{
                    gm.ResetRoundNumber();
                    gm.resetRoundWonPlayer1();
                    gm.resetRoundWonPlayer2();            
                    gm.IncreaseLevelNumber();            
                    SceneManager.LoadScene(1);
                } 
            }
            else{
                gm.ResetRoundNumber();
                gm.resetRoundWonPlayer1();
                gm.resetRoundWonPlayer2();
                SceneManager.LoadScene(6);
            }
            
        }

    }
    private void LifeControl(){
        if (player1H.GetCurrentHealth() == 0 || player2H.GetCurrentHealth() == 0){
            theresAWinner = true;
             
        }    
    }

    private void SetPlayer2HealthController(GameObject player2){
        player2H = player2.GetComponent<HealthController>();
    }
}
