using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_00 : MonoBehaviour
{
    public float attackCoolDown = 1f;

    public bool isWarning;
    public bool isSlashing;

    public GameObject warningAnim;

    public GameObject slashAnim;

    public bool playerInRange;

    public GameObject player;
    public GameObject slashObj;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float dis = Vector2.Distance(gameObject.transform.position, player.transform.position);

        if (attackCoolDown > 0 && isWarning == false && isSlashing == false)
        {
            attackCoolDown -= Time.deltaTime;
        }

        if(attackCoolDown <= 0 && isWarning == false && isSlashing == false)
        {
            if (dis < gameObject.GetComponent<EnemyStats>().attackRange)
            {
                isWarning = true;
                warningAnim.GetComponent<FlagControl>().flag = true;
                warningAnim.GetComponent<Animator>().SetTrigger("playAnim");
            }
        }

        if(isWarning == true)
        {
            if (warningAnim.GetComponent<FlagControl>().flag == false)//finish Playing animation
            {
                slashAnim.GetComponent<Animator>().SetTrigger("playSlash");
                Vector2 player_to_enemy = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
                float rotateAngle = Vector2.SignedAngle(Vector2.right, player_to_enemy);
                slashObj.transform.rotation = Quaternion.Euler(0, 0, rotateAngle);
                isWarning = false;
                isSlashing = true;
            }
        }

        if (isSlashing == true)
        {
            if (slashAnim.GetComponent<FlagControl>().flag == false)//finish Playing animation
            {
                isSlashing = false;
                attackCoolDown = 1f;
            }
        }

    }
}
