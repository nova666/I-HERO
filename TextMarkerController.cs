using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextMarkerController : MonoBehaviour {

    private Text myText;
    [SerializeField]
    private float moveAmt;
    [SerializeField]
    private float moveSpeed;
    Camera camera;
    private Vector3[] moveDir;
    private Vector3 myMoveDir;
    private bool canMove = false;

    int colorIndex;

    float time;

    private void Awake()
    {
        myText = GetComponentInChildren<Text>();
    }
    private void Start()
    {

        camera = Camera.main;
        moveDir = new Vector3[]
        {
            transform.up,
            (transform.up + transform.right),
            (transform.up + -transform.right)
        };
       
        myMoveDir = moveDir[Random.Range(0, moveDir.Length)];
    }

    private void Update()
    {
        Vector3 v = camera.transform.position - transform.position;
        v.x = v.z = 0.0f;
        transform.LookAt(camera.transform.position - v);
        transform.Rotate(0, 180, 0);

        time += Time.deltaTime;

        if (canMove)
            transform.position = Vector3.MoveTowards(transform.position, transform.position + myMoveDir, moveAmt * (moveSpeed * Time.deltaTime));

        if (colorIndex > 1)
        {
            colorIndex = 0;
        }

        if (time >= 0.05f)
        {
            colorIndex++;
            time = 0;
        }

        if (colorIndex == 0)
        {
            myText.color = Color.red;
        }
        else if (colorIndex == 1)
        {
            myText.color = Color.yellow;
        }
    }

    public void SetText(string textStr, Color textColor)
    {
        myText.text = textStr;
        canMove = true;
    }
}
