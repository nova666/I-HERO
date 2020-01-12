using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour {

    public GameObject[] CollectionGO;
    static GameObject VFX;
    static GameObject ConfettiVFX;
    static GameObject SpawningVFX;
    static GameObject effectTospawn;


    private void Awake()
    {
        AssignVFX();

    }

    void AssignVFX()
    {
        VFX = CollectionGO[0];
        ConfettiVFX = CollectionGO[1];
        SpawningVFX = CollectionGO[2];
    }

    public static void SpawnShootVRX(GameObject shoot)
    {
        if (shoot != null)
        {
            GameObject shootVFX;
            effectTospawn = VFX;
            shootVFX = Instantiate(effectTospawn, shoot.transform.position, shoot.transform.rotation);
            shootVFX.gameObject.GetComponent<projectileMove>().speed = 10;
            shootVFX.gameObject.GetComponent<projectileMove>().CreateSpawnPoint(shoot.transform);
        }
    }

    public static void SpawnConfetti(Transform enemyPosition)
    {
        if(enemyPosition != null)
        {
            GameObject VFX;
            effectTospawn = ConfettiVFX;
            VFX = Instantiate(effectTospawn, enemyPosition.position, enemyPosition.rotation);
        }
    }

    public static void SpawningEnemy(Transform enemyPosition)
    {
        if (enemyPosition != null)
        {
            GameObject VFX;
            effectTospawn = SpawningVFX;
            VFX = Instantiate(effectTospawn, enemyPosition.position, enemyPosition.rotation);
        }
    }

}
