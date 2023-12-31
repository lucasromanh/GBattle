﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardController : MonoBehaviour
{
    private float waitTime;
    private Animator anim;

    void Start(){
        int gameLevel = GameManager.Instance.GetLevelNumber();
        anim = GetComponent<Animator>();
        switch(gameLevel){
            case 1:
                anim.SetFloat("level", 0);
                waitTime = 58;
                break;
            case 2:
                anim.SetFloat("level", 0.5f);
                waitTime = 83.1f;
                break;
            case 3:
                anim.SetFloat("level", 1);
                waitTime = 63;
                break;
        }
        StartCoroutine(WaitForCard());
    }

    IEnumerator WaitForCard(){
        int sceneNumber = SceneManager.GetActiveScene().buildIndex;
        yield return new WaitForSeconds(waitTime);
        GameManager.Instance.ChangeState(GameManager.GameState.Playing);
        SceneManager.LoadScene(sceneNumber+1);
    }
}
