using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour {

    SpawnManager SpawnerController;     // REFERNCE TO SPAWNER SCRIPT TO SPAWN ENEMIES WHEN PROPER COLLIDER IS COLLIDED WITH
    SwitchCamera cameraManager;        // REFERNCE TO CAMERA SCRIPT TO SWITCH CAMERA
    GameObject LeaderBoard;           // LeaderBoard Main Menu
    AvatarController Avatar;

    private void Awake()
    {
        Avatar = GetComponentInParent<AvatarController>();
    }
    private void Start()
    {
        if(gameObject.name == "HighScores" )
        {
            Debug.Log("I am in main menu");
            LeaderBoard = FindObjectOfType<HighScoreManager>().gameObject;
            LeaderBoard.SetActive(false);
        }

        cameraManager = FindObjectOfType<SwitchCamera>();
        SpawnerController = FindObjectOfType<SpawnManager>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pivot" && this.gameObject.tag == "Camera Switch")               // CHANGE CAMERA POSITION
        {
            cameraManager.ChangeCamera();
            Destroy(this.gameObject);
        }
        if(other.gameObject.tag == "Pivot" && this.gameObject.tag == "TriggerSpawn")                   // SPAWN ENEMIES
        {
            SpawnerController.SpawnWave(this.transform.position, this.gameObject.name);
            Destroy(this.gameObject);
        }
        if (gameObject.tag == "End_Level" && other.gameObject.tag == "Pivot")
        {
            GameLevelManager.HighScore();
        }
        if (gameObject.tag == "End_Level" && other.gameObject.tag == "VFX")
        {
            if(GameLevelManager.GetSceneName() == "World1_1")
            {
                return;
            }
            else
            {
                GameLevelManager.LoadNextLevel();
            }
            
        }

        if(gameObject.name == "HighScores")
        {
            ToggleScoreONOFF();
        }

        if(gameObject.name == "Head" && other.gameObject.tag == "VFX")
        {
            Avatar.Stun();
        }
        if (gameObject.name == "tail" && other.gameObject.tag == "VFX")
        {
            Avatar.MakeAvatarJump();
        }


        if (gameObject.name == "Continue" && other.gameObject.tag == "VFX")
        {
            GameLevelManager.Continue();
            UIController.ResetScore();
        }
        if (gameObject.name == "Quit" && other.gameObject.tag == "VFX")
        {
            GameLevelManager.ReturnToMenu();
            UIController.ResetScore();
        }

        if(gameObject.name == "Crystal")
        {
            gameObject.GetComponent<ItemControl>().DamageBoss();
        }
    }

    private void ToggleScoreONOFF()
    {
        
        if (LeaderBoard.activeSelf == true)
        {
            LeaderBoard.SetActive(false);
        }
        else
        {
            LeaderBoard.SetActive(true);
        }
    }
}
