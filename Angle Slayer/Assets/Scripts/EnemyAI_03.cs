using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_03 : MonoBehaviour
{
    public GameObject player;
    public GameObject spikeObj;

    public float attackCoolDown = 1.5f;

    public bool isWarning;
    public bool isDashing;

    public GameObject warningAnim;

    private float dashTime;
    private Vector2 dashDir;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        spikeObj.transform.Rotate(0, 0, 60 * Time.deltaTime);

        if(attackCoolDown > 0 && isWarning == false && isDashing == false)
        {
            attackCoolDown -= Time.deltaTime;
        }

        if (attackCoolDown <= 0 && isWarning == false && isDashing == false)
        {
            isWarning = true;
            warningAnim.GetComponent<FlagControl>().flag = true;
            warningAnim.GetComponent<Animator>().SetTrigger("playAnim");
        }

        if (isWarning == true)
        {
            if (warningAnim.GetComponent<FlagControl>().flag == false)//finish Playing animation
            {
                dashTime = 0.2f;
                dashDir = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
                dashDir.Normalize();
                isWarning = false;
                isDashing = true;
            }
        }

    }
    public void FixedUpdate()
    {
        if (isDashing == true)
        {
            dashTime -= Time.fixedDeltaTime;
            transform.Translate(dashDir * 10 * Time.fixedDeltaTime);

            if(dashTime <= 0)
            {
                attackCoolDown = 1.5f;
                isDashing = false;
            }
        }
    }


}

