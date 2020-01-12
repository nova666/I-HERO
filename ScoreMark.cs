using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMark : MonoBehaviour {

    static int scorePoints;
    [SerializeField]
    static public GameObject hitMarkerPrefab;
    [SerializeField]
    static public Color[] markerColours = {Color.yellow, Color.red};
    [SerializeField]
    static private float markerKillTime = 1.5f;

    private void Awake()
    {
        hitMarkerPrefab = GameObject.Find("DamageText");
    }

    public static void SpawnScoreMark(int score, GameObject enemyPosition)
    {
        scorePoints = score;
        GameObject newmMarker = Instantiate(hitMarkerPrefab, enemyPosition.transform.position, Quaternion.identity);   // create new score text in the object position
        newmMarker.SetActive(true);  /// set the text display to true
        newmMarker.GetComponent<TextMarkerController>().SetText(scorePoints.ToString(), markerColours[Random.Range(0, markerColours.Length)]);
        Destroy(newmMarker.gameObject, markerKillTime);   /// destroy the object after a certain amount of time
    }

    //public void hitNowBonus(int score)
    //{
    //    scorePoints = score;
    //    GameObject cloudEffect = Instantiate(pariclesEffect, transform.position, Quaternion.identity);
    //    GameObject newmMarker = Instantiate(hitMarkerPrefab, transform.position, Quaternion.identity);   // create new score text in the object position
    //    newmMarker.SetActive(true);  /// set the text display to true
    //    newmMarker.transform.localScale = new Vector3(2, 2, 0);
    //    newmMarker.GetComponent<textMarkerController>().SetText(scorePoints.ToString(), markerColours[Random.Range(0, markerColours.Length)]);
    //    Destroy(newmMarker.gameObject, markerKillTime);   /// destroy the object after a certain amount of time
    //    Destroy(cloudEffect.gameObject, markerKillTime);
    //}
}


