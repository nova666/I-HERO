using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shoot : MonoBehaviour {

 
    #region SHOOT HELPER
    public float timeToSpawnHelper = 2;
    float elapsedTime;
    float timeToDisactivate = 5;
    bool switchPosition;
    int previousNumberPos;
    int currentNumberPos = 0;
    float timeSinceTheLevelStarted;
    #endregion
    public GameObject shootRot;
   
    void Start() {
       
    }

    // Update is called once per frame
    void Update()
    {

        if (GameLevelManager.GetSceneName() == "World1_1")
        {
            ControlShootHelper();
        }   
        if (Input.GetButtonDown("Fire1"))
        {
            VFXManager.SpawnShootVRX(shootRot);
            SoundManager.PlaySFX("Shoot", shootRot.transform);
        }
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 100, Color.red);
    }

    private void ControlShootHelper()
    {
        elapsedTime += Time.deltaTime;
        timeSinceTheLevelStarted += Time.deltaTime;
        if(timeSinceTheLevelStarted <= 10)
        {
            if (elapsedTime >= timeToSpawnHelper && !switchPosition)
            {
                if (previousNumberPos == 0)
                {
                    currentNumberPos = 1;
                }
                else
                {
                    currentNumberPos = 0;
                }
                previousNumberPos = currentNumberPos;

                switchPosition = true;
            }

            if (elapsedTime >= timeToDisactivate)
            {
                UIController.HidHelper();
                elapsedTime = 0;
                switchPosition = false;
            }

            if (currentNumberPos == 0)
            {
                UIController.SetHitPoint(InteractionPoints.Headposition());
            }
            else if (currentNumberPos == 1)
            {
                UIController.SetHitPoint(InteractionPoints.Tailposition());
            }
        }
        if(timeSinceTheLevelStarted > 10)
        {
            UIController.HidHelper();
        }
    }


}
