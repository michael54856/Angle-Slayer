using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_04 : MonoBehaviour
{
    public float attackCoolDown = 1.5f;

    public GameObject player;

    public float walkTime;
    public bool isWalking;
    public bool isShooting;

    private GameObject top_right_cornorObj;
    private GameObject bottom_left_cornorObj;

    public Vector2 walkDir;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        top_right_cornorObj = GameObject.FindWithTag("top_right_Cornor");
        bottom_left_cornorObj = GameObject.FindWithTag("bottom_left_Cornor");
    }

    // Update is called once per frame
    void Update()
    {
        if (attackCoolDown > 0 && isWalking == false && isShooting == false)
        {
            attackCoolDown -= Time.deltaTime;
        }

        if (attackCoolDown <= 0 && isWalking == false && isShooting == false)
        {
            isWalking = true;
            float r = 0;
            if(transform.position.y > player.transform.position.y)
            {
                if (transform.position.x > player.transform.position.x)//top-right
                {
                    r = Random.Range(0, 1.57f);
                }
                if (transform.position.x <= player.transform.position.x)//top-left
                {
                    r = Random.Range(1.57f, 3.14f);
                }

            }
            if(transform.position.y <= player.transform.position.y)
            {
                if (transform.position.x > player.transform.position.x)//bottom-right
                {
                    r = Random.Range(4.71f, 6.28f);
                }
                if (transform.position.x <= player.transform.position.x)//bottom-left
                {
                    r = Random.Range(3.14f, 4.71f);
                }
            }

            walkDir = new Vector2(Mathf.Cos(r), Mathf.Sin(r));
            walkTime = 1f;
        }
    }

    private void FixedUpdate()
    {
        if(isWalking == true)
        {
            if (transform.position.x > top_right_cornorObj.transform.position.x - 0.25f)
            {
                walkDir.x = -(Mathf.Abs(walkDir.x));
            }
            if (transform.position.y > top_right_cornorObj.transform.position.y - 0.25f)
            {
                walkDir.y = -(Mathf.Abs(walkDir.y));
            }

            if (transform.position.x < bottom_left_cornorObj.transform.position.x + 0.25f)
            {
                walkDir.x = Mathf.Abs(walkDir.x);
            }
            if (transform.position.y < bottom_left_cornorObj.transform.position.y + 0.25f)
            {
                walkDir.y = (Mathf.Abs(walkDir.y));
            }
            transform.Translate(walkDir * Time.fixedDeltaTime * 3);
            walkTime -= Time.fixedDeltaTime;
            if(walkTime <= 0)
            {
                isWalking = false;
                attackCoolDown = 1.5f;
            }
        }
    }
}
