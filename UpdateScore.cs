using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScore : MonoBehaviour {

    private Text score;
    private Image lifePoints;

    bool displaying = true;
    float timeToDisplay = 2.0f;
    float timeElapsed;
    int scorevalue;
	// Use this for initialization
	void Start () {
        score = GetComponentInChildren<Text>();

        lifePoints = GetComponentInChildren<Image>();
        score.text = "000";
        
	}
	
	// Update is called once per frame
	void Update () {

        if(displaying)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= timeToDisplay)
            {

                score.gameObject.SetActive(false);
                lifePoints.gameObject.SetActive(false);


                displaying = false;
                timeElapsed = 0;

            }
            else
            {
                score.gameObject.SetActive(true);
                lifePoints.gameObject.SetActive(true);
            }
        }

        
       
	}

    public void activateHUD()
    {
        displaying = true;
    }

    public void incrementScore(int value)
    {
        scorevalue += value;
        score.text = scorevalue.ToString();
    }
}
