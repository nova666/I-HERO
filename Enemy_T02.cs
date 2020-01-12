using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Need to rename the script to Enemy_T02
/// </summary>

public class Enemy_T02 : MonoBehaviour {

    Enemy enemy = new Enemy();
    string name = "Enemy_T02";
    int scoreValue = 200;
    public float elapsedTimeToAttack;
    public float timeToAttack;

    void Start() {
        enemy.Init(gameObject, name);
        enemy.Idle = true;
        enemy.Speed = 3;
    }

    void Update() {
        /// Function To Rotate Character Towards The Enemy
        if (enemy.Idle)
        {
            enemy.LookAtTarget(transform,enemy.TargetPosition);
            elapsedTimeToAttack += Time.deltaTime;    // code Slowing Down
            if (elapsedTimeToAttack >= timeToAttack)
            {
                enemy.Idle = false;
                enemy.Chase = true;
            }
        }
        else if (enemy.Chase)
        {
            enemy.LookAtTarget(transform, enemy.TargetPosition);
            enemy.MoveTowardsTarget(transform, enemy.TargetPosition.position, enemy.Speed);
            enemy.Animation_Controller.SetMove = true;
        }
        else if (enemy.Escape)
        {
            enemy.LookAtTarget(transform, enemy.SelectedEscapeRoute);
            enemy.MoveTowardsTarget(transform, enemy.SelectedEscapeRoute.position, enemy.Speed);
        }

        if (!enemy.Chase && !enemy.Idle && !enemy.Escape)
        {
            enemy.LookAtTarget(transform, enemy.SelectedEscapeRoute);
            enemy.Escape = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Attack_Area")
        {
            enemy.Chase = false;
            enemy.Animation_Controller.TriggerAttack();
        }

        if (other.gameObject.tag == "Enemy_T02" || other.gameObject.tag == "VFX")
        {
            if (other.gameObject.tag == "VFX")
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

