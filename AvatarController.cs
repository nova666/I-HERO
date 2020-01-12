using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AvatarController : MonoBehaviour {

    public Health CurrentHealth;
    GameObject characterPivotPoint;          
    Transform characterTransform;
    #region Waypoint System
    WayPointSystem wayPointSystem;
    public float Speed;           // controls movent towards the target object
    private int index = 0; //<== no reference
    #endregion

    TriggerAttackArea attackArea;
    Quaternion DestRot = Quaternion.identity;   // TRANSFERED TO aVATARcONTROLLER

    AnimationsController animations_Controller;              /// AvatarAnimationController
    Animator AnimatorInfo; // needs to check animations states

    GravityHandler AvatarGravity;
    float rotationDegreeSpeed = 1;
  
    float maxDistanceObject = 50;
    Transform itemTarget;
    Transform target;
    bool BossScene;
    int PreviousIndex;

    float ElapsedTime;
    float WakeUpTime = 3.0f;

    bool FightOn;
    bool StartGame = false;

    private void Awake()
    {
        AvatarGravity = GetComponent<GravityHandler>();
        wayPointSystem = GetComponent<WayPointSystem>();
    }
    private void Start()                      // NEW
    {
        FightOn = false;
        attackArea = GetComponentInChildren<TriggerAttackArea>();
        characterPivotPoint = GameObject.Find("Pivot");
        characterTransform = GetComponent<Transform>(); 
        animations_Controller = GetComponentInChildren<AnimationsController>();
        AnimatorInfo = GetComponentInChildren<Animator>();

        #region Boss
        //#Boss// battle need to manage State in game manager
        //if (GameLevelManager.GetSceneName() == "boss")
        //{
        //    BossScene = true;
        //    PreviousIndex = 0;
        //}
        //else
        //{
        //    BossScene = false;
        //}
        #endregion
    }

    void MoveTowardsTheTarget(Transform nextPosition)   // move character toward next position
    {
        Vector3 targetPos = new Vector3(nextPosition.position.x,
            characterTransform.position.y, nextPosition.position.z);      /// trying to ignore the Y position
        characterTransform.position = Vector3.MoveTowards(characterTransform.position,
            targetPos, Speed * Time.deltaTime);
    }

    private void Update()
    {
        CheckTimeForAction(Time.deltaTime);
        itemTarget = wayPointSystem.FindTarget();
        AvatarGravity.Gravity();
        AvatarGravity.Jump();
        AvatarGravity.FinalMove();
        AvatarGravity.GroundChecking();
        AvatarGravity.CollisionCheck();
        Move();
        HealthCheck();
        CheckState();
    }

    #region Waiting Avatar State
    private void CheckState()
    {
        if(FightOn && AnimatorInfo.GetCurrentAnimatorStateInfo(0).IsName("musketeer_idle"))
        {
            attackArea.CheckIfCollidingWithEnemies();
        }
    }

    private void CheckTimeForAction(float deltaTime)
    {
        if(!FightOn)
        {
            ElapsedTime += deltaTime;
            if(ElapsedTime >= WakeUpTime)
            {
                FightOn = true;
            }
        }
        else if(FightOn && !StartGame)
        {
            animations_Controller.SetMove = true;
            StartGame = true;
        }
    }

    #endregion
    private void HealthCheck()
    {
        if(CurrentHealth.GetHealth() <= 0)
        {
            animations_Controller.SetDie = true;
            if(AnimatorInfo.GetCurrentAnimatorStateInfo(0).IsName("GameOver"))
            {
                GameLevelManager.LoadGameOver();
            }
        }
    }

    private void SetTargetRotation(Transform target)
    {
        Vector3 targetPosition = new Vector3(target.position.x,
        characterTransform.position.y, target.position.z);
        Vector3 relativePos = targetPosition - characterTransform.position;
        DestRot = Quaternion.LookRotation(relativePos, target.up);
        characterTransform.rotation = Quaternion.RotateTowards(characterTransform.rotation, DestRot, rotationDegreeSpeed);
        MoveTowardsTheTarget(target);
    }

    private void Move()
    {
        if (CurrentHealth.GetHealth() != 0 && !attackArea.GetAttackAreaState() &&
            !AnimatorInfo.GetCurrentAnimatorStateInfo(0).IsName("musketeer_stun") &&
            !AnimatorInfo.GetCurrentAnimatorStateInfo(0).IsName("musketeer_idle"))
        {
            if (itemTarget == null)   /// if no collectable items has been set as the target
            {
                if(BossScene)
                {
                    target = wayPointSystem.GetCoordinateWaypoint();
                    SetTargetRotation(target);
                }
                else
                {
                    target = wayPointSystem.GetCoordinateWaypoint();
                    SetTargetRotation(target);
                }
            }
            else
            {
                target = wayPointSystem.FindTarget();                  //// Set Item As NextPosition
                SetTargetRotation(target);
            }

            if(AnimatorInfo.GetCurrentAnimatorStateInfo(0).IsName("musketeer_idle") && !attackArea.GetAttackAreaState())
            {
                animations_Controller.SetIdle = false;
                animations_Controller.SetMove = true;
            }
        } 
    }

    public void Stun()
    {
        animations_Controller.TriggerStun();
    }

    public void MakeAvatarJump()
    {
        animations_Controller.TriggerJump();
        AvatarGravity.TriggerJump();
    }


}


