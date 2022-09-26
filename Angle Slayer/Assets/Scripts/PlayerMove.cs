using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameObject player;
    public float speed;

    public int damage;

    private bool touchStart = false;

    private Vector2 pointA;
    private Vector2 pointB;

    private Vector2 dashPointA;
    private Vector2 dashPointB;

    public GameObject circle;
    public GameObject outerCircle;
    private Vector2 dashDir;

    public int controlTouch;

    public GameObject[] enemies;

    public bool startCountDown;
    public float countSwipeTime;

    public bool dashing;
    public float dashTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixedUpdate()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.touches[i].phase == TouchPhase.Began && controlTouch == -1)
            {
                circle.SetActive(true);
                outerCircle.SetActive(true);
                pointA = new Vector3(Input.GetTouch(i).position.x, Input.GetTouch(i).position.y, 0);
                controlTouch = Input.touches[i].fingerId;
                circle.transform.position = pointA;
                outerCircle.transform.position = pointA;

                startCountDown = true;
                countSwipeTime = 0.15f;
                dashPointA = new Vector3(Input.GetTouch(i).position.x, Input.GetTouch(i).position.y, 0);
            }
            if (controlTouch >= 0 && Input.touches[i].phase == TouchPhase.Moved && Input.touches[i].fingerId == controlTouch)
            {
                touchStart = true;
                pointB = new Vector3(Input.GetTouch(i).position.x, Input.GetTouch(i).position.y, 0);
            }
            if (controlTouch >= 0 && Input.touches[i].phase == TouchPhase.Ended && Input.touches[i].fingerId == controlTouch)
            {
                if(countSwipeTime > 0 && dashing == false)
                {
                    dashPointB = new Vector3(Input.GetTouch(i).position.x, Input.GetTouch(i).position.y, 0);
                    dashing = true;
                    dashTime = 0.15f;
                    dashDir = dashPointB - dashPointA;
                    dashDir.Normalize();
                }
                startCountDown = false;

                touchStart = false;
                circle.transform.position = outerCircle.transform.position;
                controlTouch = -1;
                circle.SetActive(false);
                outerCircle.SetActive(false);

                Attack(false);
            }

        }

        if(dashing == true)
        {
            dashTime -= Time.fixedDeltaTime;
            if(dashTime < 0)
            {
                Attack(true);
                dashing = false;
            }
            player.transform.Translate(dashDir * 3 * speed * Time.fixedDeltaTime);

        }

        if(startCountDown == true)
        {
            if(countSwipeTime > 0)
            {
                countSwipeTime -= Time.fixedDeltaTime;
            }
        }

        if(touchStart == true)
        {
            Vector2 offset = pointB - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset, Screen.width * 220 / 2000);
            circle.transform.position = new Vector3(pointA.x + direction.x, pointA.y + direction.y, 0);

            player.transform.Translate(direction / (Screen.width * 220 / 2000) * speed * Time.fixedDeltaTime);
        }

    }

    public void Attack(bool dashAttack)
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            float dis = Vector2.Distance(gameObject.transform.position, enemy.transform.position);
            if (dis < enemy.GetComponent<EnemyStats>().attackRange)
            {
                Vector2 dir_1 = new Vector2(enemy.GetComponent<EnemyHealth>().dirPointer.position.x - enemy.transform.position.x, enemy.GetComponent<EnemyHealth>().dirPointer.position.y - enemy.transform.position.y);
                Vector2 dir_2 = new Vector2(transform.position.x - enemy.transform.position.x, transform.position.y - enemy.transform.position.y);

                float myAngle = Vector2.SignedAngle(dir_2, dir_1);

                if (myAngle > 0 && myAngle < enemy.GetComponent<EnemyStats>().attackRadius)
                {
                    if(enemy.GetComponent<EnemyHealth>().canGetHurt == true)
                    {
                        if (dashAttack == true)
                        {
                            enemy.GetComponent<EnemyHealth>().Hurt(damage * 1.5f, 1);
                        }
                        else
                        {
                            enemy.GetComponent<EnemyHealth>().Hurt(damage, 0);
                        }
                    }
                }
            }
        }
    }
}
