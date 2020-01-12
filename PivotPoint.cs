using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotPoint : MonoBehaviour {

    BossBehaviour boss;
    WayPointSystem waypointSystem;
    Vector3 characterPosition;   // position similar to the character
    float coordYofTarget;

	void Start () {
        if(gameObject.tag == "Boss")
        {
            boss = FindObjectOfType<BossBehaviour>();
        }
        else
        {
            waypointSystem = FindObjectOfType<WayPointSystem>();
        }
	}


	void Update () {
        if (gameObject.tag == "Boss")
        {
            coordYofTarget = boss.GetCoordinateWaypoint().position.y;
            SetPositionRelativeToBossXZ();
        }
        else
        {  
            coordYofTarget = waypointSystem.GetCoordinateWaypoint().position.y;
            SetPositionRelativeToCharacterXZ();
        }
	}

    private void SetPositionRelativeToCharacterXZ()
    {
        characterPosition.y = coordYofTarget;
        characterPosition.x = waypointSystem.gameObject.transform.position.x;
        characterPosition.z = waypointSystem.gameObject.transform.position.z;
        transform.position = characterPosition;
    }

    private void SetPositionRelativeToBossXZ()
    {
        characterPosition.y = coordYofTarget;
        characterPosition.x = boss.gameObject.transform.position.x;
        characterPosition.z = boss.gameObject.transform.position.z;
        transform.position = characterPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Waypoint")
        {
            if(gameObject.tag == "Boss")
            {
                boss.ChangetIndex();
            }
            else
            {
                waypointSystem.IncrementIndex();
            }
        }
    }
}

#region
//AvatarController waypoint;
///waypoint = FindObjectOfType<AvatarController>();
#endregion