using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControl : MonoBehaviour
{

    enum ScoreValue
    {
        Coin = 100,
    }  

    /// MovementOfCrystal 
    float speedRot = 50;
    int Index;
    int newIndex;
    int PreviousIndex;
    float ElapsedTime;
    float TimeToChangePosition = 2.0f;
    // Boss Health
    BossBehaviour Boss;
    List<Transform> ListPositionsCrystal = new List<Transform>();

    private void Awake()
    {
        AddPositionsToList();   //boss
        Boss = transform.root.gameObject.GetComponent<BossBehaviour>();
    }
    // Update is called once per frame
    void Update()
    {
        RotateObject();

        //boss
        if (gameObject.name == "Crystal") //boss
        {
          if(ElapsedTime >= TimeToChangePosition)
          {
             MoveCrystal();
          }
        }
    }

    private void RotateObject()
    {
        transform.Rotate(Vector3.up * speedRot * Time.deltaTime, Space.World);
        ElapsedTime += Time.deltaTime;
    }

    private void AddPositionsToList()   // For Crystal
    {
        foreach (GameObject crystal in GameObject.FindGameObjectsWithTag("Crystal"))
        {
            ListPositionsCrystal.Add((crystal).transform);
        }
    }

    private void MoveCrystal()   // For Crystal
    {
        ElapsedTime = 0;                            // get random position between available positions
        TimeToChangePosition = GetRandomNumber();   // Move Crystal to new position                   
        Index = GetNewIndex();                       // Set crystal to new position 
        transform.position = ListPositionsCrystal[Index].position;
        transform.parent = ListPositionsCrystal[Index];
    }

    private int GetNewIndex()    // For Crystal
    {
        do
        {
            newIndex = UnityEngine.Random.Range(0, ListPositionsCrystal.Count);

        } while (newIndex == PreviousIndex);

        PreviousIndex = newIndex;
        return newIndex;
    }

    private float GetRandomNumber()   // For Crystal
    {
        return UnityEngine.Random.Range(5.0f, 30.0f);
    }

    public void DamageBoss()    // For Crystal
    {
        Boss.DamageBoss();
        MoveCrystal();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Crystal")
        {
            return;
        }

        if (gameObject.tag == "InteractiveObject" && other.gameObject.tag == "VFX")
        {
            VFXManager.SpawnConfetti(transform);
            SoundManager.PlaySFX("Item", transform);
            ScoreMark.SpawnScoreMark((int)ScoreValue.Coin, gameObject);
            Destroy(gameObject);
        }

    }

}


