using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_T03 : MonoBehaviour {

    Enemy enemy = new Enemy();
    string name = "Enemy_T03";
    int scoreValue = 100;
    float moveSpeed = 4.0f;
    TestRoate rotateController;
    Transform closestPositionForRotation;
    Collider[] positionsRot;
    bool targetReached;
   
    
    void Start()
    {
        enemy.Init(gameObject, name);
        rotateController = GetComponent<TestRoate>();
        enemy.Speed = 3;
        closestPositionForRotation = FindClosestRotationPos();
    }

    Transform FindClosestRotationPos()
    {
        float minDistance = Mathf.Infinity;
        positionsRot = Physics.OverlapSphere(transform.position, 100);   // still need mask to highlight the current position
        Transform closest = null;
        for(int i = 0; i < positionsRot.Length; i++)
        {
            if (positionsRot[i].gameObject.name == "RotationPos")
            {
                float distance = (positionsRot[i].transform.position - transform.position).sqrMagnitude;
                if(distance < minDistance)
                {
                    closest = positionsRot[i].transform;
                    minDistance = distance;
                }
            }  
        }
        return closest;          
    }

    float CalculateDistance(Vector3 position, Vector3 target)
    {
        float minDistance = Mathf.Infinity;
        float distance = (target - position).sqrMagnitude;
        if (distance < minDistance)
        {
           minDistance = distance;
        }  
        return minDistance;
    }

    // Update is called once per frame
    void Update()
    {
       if(closestPositionForRotation != null)  /// Need this only to make enemy get to the target position  
                                               /// once has been set in the position 
        {
            if (!targetReached)
            {
                enemy.Animation_Controller.SetMove = true;
                enemy.MoveTowardsTarget(transform,closestPositionForRotation.position, enemy.Speed);
                enemy.LookAtTarget(transform,closestPositionForRotation);
            }
           
            if(transform.position == closestPositionForRotation.position && !targetReached)
            {
                targetReached = true;
                gameObject.transform.eulerAngles = Vector3.zero;
                transform.Rotate(new Vector3(0, -90, 0));
                if (!rotateController.enabled)
                {
                    rotateController.enabled = true;
                }
            }
            
            if(targetReached && rotateController.enabled)
            {
                float distance = CalculateDistance(transform.position, enemy.TargetPosition.position);
                
                if(distance < 200)
                {
                    rotateController.enabled = false;
                    enemy.Chase = true;
                }
            }

        }

        if (enemy.Chase)                                              /// Initialising state of chasing the character
        {
            transform.parent = null;
            enemy.LookAtTarget(transform,enemy.TargetPosition);
            enemy.MoveTowardsTarget(transform, enemy.TargetPosition.position, enemy.Speed);
        }
        else if (enemy.Escape)
        {
            enemy.LookAtTarget(transform, enemy.SelectedEscapeRoute);
            float distance = CalculateDistance(transform.position, enemy.TargetPosition.position);

            if (distance > 1000)
            {
                Despawn();
            }

            enemy.MoveTowardsTarget(transform, enemy.SelectedEscapeRoute.position, enemy.Speed);
        }
    }

    private void Despawn()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "RotationPos")
        {
            transform.parent = other.gameObject.transform;
            other.gameObject.GetComponent<Collider>().enabled = false;
        }
        else if(other.gameObject.name == "Attack_Area")
        {
            if (enemy.Chase)
            {
                enemy.Chase = false;
                enemy.Escape = true;
            }
        }

        if (other.gameObject.tag == "Enemy_T03" || other.gameObject.tag == "VFX")
        {
            if(other.gameObject.tag == "VFX")
            {
                UIController.IncreaseScore(scoreValue);
                ScoreMark.SpawnScoreMark(scoreValue, gameObject);
            }

            VFXManager.SpawnConfetti(transform);
            SoundManager.PlaySFX("Die", transform);
            Destroy(gameObject);
        }
    }

}
