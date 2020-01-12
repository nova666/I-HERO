using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_T01 : MonoBehaviour  
{

    Enemy enemy = new Enemy();
    string name = "Enemy_T01";
    string escapePosition;
    int scoreValue = 250;
    #region Only for Enemy_T01
    NavMeshAgent navigation;
    float time;
    float targetTime = 1.0f;
    float distance;
    #endregion

    // Use this for initialization
    void Start () {

        enemy.Init(gameObject,name);   /// send this game gameobject to the custom class to create enemy 
        navigation = GetComponent<NavMeshAgent>();
        enemy.Speed = 3;
        enemy.Chase = true;
        enemy.Origin = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        if(enemy.Chase)
        {
            enemy.Animation_Controller.SetMove = true;
            navigation.speed = enemy.Speed;
            navigation.SetDestination(enemy.TargetPosition.position);
        }
       
        if(enemy.Attack)
        {
            enemy.Animation_Controller.TriggerAttack();
        }

        if (enemy.Escape)
        {
            navigation.SetDestination(enemy.SelectedEscapeRoute.position);
            
        }

        if (!enemy.Chase)
        {
            time += Time.deltaTime;
            if (time >= targetTime)
            {
                enemy.Escape = true;
            }
        }


        distance = Vector3.Distance(enemy.Origin, transform.position);
        if (distance >= 40)
        {
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

        if(other.gameObject.tag == "Enemy_T01" || other.gameObject.tag == "VFX")
        {
            if (other.gameObject.tag == "VFX")
            {
                UIController.IncreaseScore(scoreValue);
                ScoreMark.SpawnScoreMark(scoreValue, gameObject);
                VFXManager.SpawnConfetti(transform);
                SoundManager.PlaySFX("Die", transform);
            }
            if(other.gameObject.tag == "Enemy_T01")
            {
                VFXManager.SpawnConfetti(transform);
                SoundManager.PlaySFX("Despawn", transform);
            }


                Destroy(gameObject);
        }
    }

}
