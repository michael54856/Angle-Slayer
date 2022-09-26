using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsBehaviour : MonoBehaviour
{
    private GameObject goal;

    public GameObject playerScore;

    private Vector2 goalPos;

    public float restTimer = 0.5f; 

    public float speed;

    public Vector2 moveDir;

    public int myValue;

    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        goal = GameObject.FindWithTag("PointIcon");
        playerScore = GameObject.FindWithTag("PointText");
        cam = Camera.main;
        moveDir = new Vector2(Random.Range(-1.0f,1.0f), Random.Range(-1.0f, 1.0f));
        moveDir.Normalize();
        speed = Random.Range(0.85f,1.5f);

    }

    // Update is called once per frame
    void Update()
    {
        if(speed > 0)
        {
            transform.Translate(moveDir * speed * Time.deltaTime);
            speed -= Time.deltaTime;
        }
        if(speed <= 0 && restTimer > 0)
        {
            restTimer -= Time.deltaTime;
            if (restTimer <= 0)
            {
                goalPos = new Vector3(goal.transform.position.x, goal.transform.position.y,0);
            }
        }
        if (restTimer <= 0)
        {
            transform.position = Vector3.Lerp(transform.position, goalPos, 0.1f);
            if(Vector2.Distance(transform.position, goalPos) < 0.1f)
            {
                playerScore.GetComponent<ScoreManger>().nowRealScore += myValue;
                Destroy(gameObject);
            }
            
        }


    }

}
