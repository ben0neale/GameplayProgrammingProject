using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Camera PlayerCam;
    public Camera CutSceneCam;
    public Animator doorAnim;
    public Animator switchAnim;
    public Animator cutsceneCamAnim;

    float timer = 1f;
    float cutsceneTime = 4f;

    public enum Gamestate
    {
        play,
        cutscene
    }
    public Gamestate state = Gamestate.play;
    // Start is called before the first frame update
    void Start()
    {
        PlayerCam.enabled = true;
        CutSceneCam.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (state == Gamestate.cutscene)
        {
            CutScene();
        }
    }

    void CutScene()
    {
        CutSceneCam.enabled = true;
        PlayerCam.enabled = false;
        doorAnim.SetBool("open", true);
        switchAnim.SetBool("press", true);
        cutsceneCamAnim.SetBool("move", true);
        Time.timeScale = 0.5f;

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {         
            doorAnim.SetBool("open", false);
            switchAnim.SetBool("press", false);
            cutsceneCamAnim.SetBool("move", false);          
        }
        if (cutsceneTime > 0)
        {
            cutsceneTime -= Time.deltaTime;
        }
        else
        {
            state = Gamestate.play;
            PlayerCam.enabled = true;
            CutSceneCam.enabled = false;
            Time.timeScale = 1f;
        }
        
    }
}
