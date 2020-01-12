using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WayPointSystem : MonoBehaviour {

    List<Transform> listWayPoints = new List<Transform>();    // List To sort the waypoint order depending on their number                                                       // Use this for initialization
    private int index = 0;
    int PreviousIndex;
    bool BossScene;

    float maxDistanceItem = 50;
    Transform itemTarget;


    void Start () {

        AddObjectsToList();

        if (GameLevelManager.GetSceneName() == "boss")
        {
            BossScene = true;
            PreviousIndex = 0;
        }
        else
        {
            BossScene = false;
        }

    }


    public Transform GetCoordinateWaypoint()         // used only for the pivotPoint
    {
        return listWayPoints[index];                    // In this instance i am trying to return the position, 
    }

    public void IncrementIndex()   // RENAME IT INDEX
    {
        if (BossScene)
        {
            index = GetNewIndex();
        }
        else
        {
            if (index < listWayPoints.Count - 1)   /// Increment the index only if the index is less than number of elements inside the list
            {
                index++;
            }
        }
    }


    private void AddObjectsToList()                       /// put objects(waypoints) in a list and reorder them in a numberical order
    {
        foreach (GameObject waypoint in GameObject.FindGameObjectsWithTag("Waypoint"))
        {
            listWayPoints.Add((waypoint).transform);
        }
        var newList = listWayPoints.OrderBy(x => x.name).ToList();
        listWayPoints = (List<Transform>)newList;
    }


    /// Boss Battle 
    private int GetNewIndex()
    {
        int newIndex;
        do
        {
            newIndex = UnityEngine.Random.Range(0, listWayPoints.Count);

        } while (newIndex == PreviousIndex);

        PreviousIndex = newIndex;
        return newIndex;
    }

    // NEW  USED TO FIND NEAREST OBJECT TOI SET THEM AS TARGET
    public Transform FindTarget()          
    {
        GameObject[] items_ToCollect = GameObject.FindGameObjectsWithTag("Collectable");
        float minDistance = Mathf.Infinity;
        Transform closest = null;
        for (int i = 0; i < items_ToCollect.Length; ++i)
        {
            float distance = (items_ToCollect[i].transform.position - transform.position).sqrMagnitude;
            Debug.Log(distance);
            if (distance < minDistance && distance <= maxDistanceItem)
            {
                closest = items_ToCollect[i].transform;
                minDistance = distance;
            }
        }
        return closest;
    }

}
