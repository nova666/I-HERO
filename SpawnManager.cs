using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {


    string enemyType;  // specify type of enemy
    Collider[] colliders;
    float areaSize = 20;
    int layerMask = 1 << 10;      // working  for specific layer

    // Enemies Prefab
    public GameObject Enemy_T01;    /// Ground enemy 01  bunny
    public GameObject Enemy_T02;    /// Flying Enemy 02  bat
    public GameObject Enemy_T03;    /// Ground Enemy 03
    public GameObject Enemy_T04;    /// Rushing Enemy 04
 
    //public int maxNumOfEnemT01;
    public float offSetPawn;
    int numberOfEnemies = 0;
    float timeCounterEnemyT1;
    float elapsedTime;
    float timeToSpawn = 2.0f;
    int indexPositions;

    public void SpawnWave(Vector3 SpawnArea, String Wave) /// Spawn method
    {
        List<GameObject> monsterList = new List<GameObject>();
        SpawnArea = new Vector3(SpawnArea.x,
            SpawnArea.y, SpawnArea.z);
        colliders = Physics.OverlapSphere(SpawnArea, areaSize, layerMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Spawn Enemy")
            {
                monsterList.Add(colliders[i].gameObject);   
            }
        }
        DetectWaveType(Wave, monsterList);
    }

    private void DetectWaveType(string wave, List<GameObject> monsterList)
    {
        switch (wave)
        {
            case "Wave01":
                SpawnWaveArea01(monsterList);
                break;
            case "Wave02":
                SpawnWaveArea02(monsterList);
                break;
            case "Wave03":
                SpawnWaveArea03(monsterList);
                break;
            case "Single01":
                SpawnSingle01(monsterList);
                break;
            case "Single02":
                SpawnSingle02(monsterList);
                break;
        }
    }

    private void SpawnSingle01(List<GameObject> monsterList)
    {
        
        foreach (GameObject position in monsterList)
        {
            VFXManager.SpawningEnemy(position.transform);
            SoundManager.PlaySFX("Spawn", transform);
            StartCoroutine(SpawnEnemySingle01(position.transform));
            
        }
        ResetList(monsterList);
    }

    IEnumerator SpawnEnemySingle01(Transform transform)   // spawning delayed
    {
        yield return new WaitForSeconds(0.5f); /// Start Wait 
        SoundManager.PlaySFX("SpawnCry", transform);
        GameObject enemy = Instantiate(Enemy_T01, transform.position, Quaternion.identity) as GameObject; //single  //bunny
        
    }


    private void SpawnSingle02(List<GameObject> monsterList)
    {
        foreach (GameObject position in monsterList)
        {
            VFXManager.SpawningEnemy(position.transform);
            SoundManager.PlaySFX("Spawn",transform);
            SoundManager.PlaySFX("SpawnCry", transform);
            StartCoroutine(SpawnEnemySingle02(position.transform));
        }
        ResetList(monsterList);
    }

    IEnumerator SpawnEnemySingle02(Transform transform)
    {
        yield return new WaitForSeconds(0.5f); /// Start Wait 
        SoundManager.PlaySFX("SpawnCry", transform);
        GameObject enemy = Instantiate(Enemy_T02, transform.position, Quaternion.identity) as GameObject;   // single bat
    }

    private void ResetList(List<GameObject> monsterList)
    {
        monsterList.Clear();
    }

    public void SpawnWaveArea01(List<GameObject> monsterList) // need Change
    {
        foreach( GameObject position in monsterList)
        {
            VFXManager.SpawningEnemy(position.transform);
            SoundManager.PlaySFX("Spawn", transform);
            SoundManager.PlaySFX("SpawnCry", transform);
            StartCoroutine(SpawnEnemyWaveArea01(position.transform)); 
        } 
        ResetList(monsterList);
    }

    IEnumerator SpawnEnemyWaveArea01(Transform transform)
    {
        yield return new WaitForSeconds(0.5f); /// Start Wait 
        SoundManager.PlaySFX("SpawnCry", transform);
        GameObject enemy = Instantiate(Enemy_T01, transform.position, Quaternion.identity) as GameObject;   // wave rabbit
    }

    public void SpawnWaveArea02(List<GameObject> monsterList)
    {
        StartCoroutine(SpawnEnemyT02(monsterList));
    }

    public void SpawnWaveArea03(List<GameObject> monsterList)
    {
        StartCoroutine(SpawnEnemyT01(monsterList));
    }

    public void SpawnWaveArea04(List<GameObject> monsterList)
    {
        foreach (GameObject position in monsterList)
        {
            VFXManager.SpawningEnemy(position.transform);
            SoundManager.PlaySFX("Spawn",transform);
            SoundManager.PlaySFX("SpawnCry", transform);
            GameObject enemy = Instantiate(Enemy_T04, position.transform.position, Quaternion.Euler(0,-90,0)) as GameObject;
        }
        ResetList(monsterList);
    }
    
    IEnumerator SpawnEnemyT02(List<GameObject> monsterList)
    {
        yield return new WaitForSeconds(1.0f); /// Start Wait 
        while (true)
        {
            for (int i = 0; i < monsterList.Count; i++)
            {
                VFXManager.SpawningEnemy(monsterList[i].transform);
                SoundManager.PlaySFX("Spawn",transform);
                yield return new WaitForSeconds(0.5f);  /// Spawn wait
                SoundManager.PlaySFX("SpawnCry", transform);
                GameObject enemy = Instantiate(Enemy_T02, monsterList[i].transform.position, Quaternion.identity) as GameObject;
                yield return new WaitForSeconds(0.5f);  /// Spawn wait
            }
            ResetList(monsterList);
            yield break;
        }

    }

    IEnumerator SpawnEnemyT01(List<GameObject> monsterList)
    {
        yield return new WaitForSeconds(2.0f); /// Start Wait 
        while(true)
        {
            for (int i = 0; i < monsterList.Count; i++)
            {
                VFXManager.SpawningEnemy(monsterList[i].transform);
                SoundManager.PlaySFX("Spawn", transform);
                yield return new WaitForSeconds(0.5f);  /// Spawn wait
                SoundManager.PlaySFX("SpawnCry", transform);
                GameObject enemy = Instantiate(Enemy_T01, monsterList[i].transform.position, Quaternion.identity) as GameObject;
                yield return new WaitForSeconds(0.5f);  /// Spawn wait
            }
            ResetList(monsterList);
            yield break;  
        }
        
    }

    
}


