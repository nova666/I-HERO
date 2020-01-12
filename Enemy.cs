using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy{

    ///HealthController avatarHealth;   // refernce to avatar health
    AnimationsController animation_Controller; // Refernce To animation controller

    #region Enemy Properties
    string name;
    int enemyHealth; 
    bool chase;
    bool attack;
    bool escape;
    bool idle;
    float speed;
    bool escapePositionSet;
    Vector3 origin;
    Quaternion targetRot = Quaternion.identity;
    #endregion
    #region Enemy Escape Logic
    Transform targetPosition;
    Transform selectedEscapeRoute;
    GameObject[] escapePositions;
    #endregion

    //Score
    ///UIController scoreTextDisplay;

    public void Init(GameObject enemyGameObject, string objectName)
    {
        name = objectName;
        escapePositions = GameObject.FindGameObjectsWithTag(name);
        targetPosition = GameObject.FindObjectOfType<AvatarController>().transform;
        animation_Controller = enemyGameObject.GetComponent<AnimationsController>();
        selectedEscapeRoute = GetEscapePosition;
    }

    #region Enemy Properties Functions
    public string EnemyName
    {
        get { return name; }
        set { name = value; }
    }

   public bool Chase
    {
        get { return chase; }
        set { chase = value; }
    }

    public bool Attack
    {
        get { return attack; }
        set { attack = value; }
    }

    public bool Escape
    {
        get { return escape; }
        set { escape = value; }
    }

    public bool Name
    {
        get { return Name; }
        set { Name = value; }
    }

    public bool Idle
    {
        get { return idle; }
        set { idle = value;  }
    }

    public int Health                   ////get set enemy health 
    {
        get { return enemyHealth; }
        set { enemyHealth = value; }
    }

    public Transform TargetPosition
    {
        get { return targetPosition; }

        set { targetPosition = value;}
    }

    public GameObject[] EscapePositions
    {
        get { return escapePositions;}

        set { escapePositions = value;}
    }

    public float Speed
    {
        get{ return speed;}

        set{ speed = value;}
    }

    public AnimationsController Animation_Controller
    {
        get{ return animation_Controller;}
    }

    public Transform SelectedEscapeRoute
    {
        get { return selectedEscapeRoute;}

        set { selectedEscapeRoute = value;}
    }

    public Transform GetEscapePosition
    {
         get { return SelectedEscapeRoute = EscapePositions[UnityEngine.Random.Range(0, escapePositions.Length)].transform; }
    }

    public bool EscapePositionSet
    {
        get { return escapePositionSet; }

        set { escapePositionSet = value; }
    }

    public Vector3 Origin
    {
        get { return origin; }

        set { origin = value; }
    }

    #endregion

    public void LookAtTarget(Transform enemy,Transform target)
    {
        Vector3 relativePos = target.position - enemy.position;
        targetRot = Quaternion.LookRotation(relativePos, target.up);
        enemy.rotation = Quaternion.RotateTowards(enemy.rotation, targetRot, 10);
    }

    public void MoveTowardsTarget(Transform enemyPosition,Vector3 nextPosition, float speed)
    {
        Vector3 targetPos = new Vector3(nextPosition.x, enemyPosition.position.y, nextPosition.z);
        enemyPosition.position = Vector3.MoveTowards(enemyPosition.position, nextPosition, speed * Time.deltaTime);
    }


    //// when animation of attack finishes change bool to false 

}
