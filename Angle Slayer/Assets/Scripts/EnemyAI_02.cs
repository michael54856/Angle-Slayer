using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI_02 : MonoBehaviour
{
    public float attackCoolDown = 1f;

    public bool isWarning;
    public bool isSlashing;

    public GameObject warningAnim;

    public GameObject slashAnim;

    public bool playerInRange;

    public GameObject player;
    public GameObject slashObj;

    public Image orangeRange;

    private GameObject top_right_cornorObj;
    private GameObject bottom_left_cornorObj;
    public bool isDisappearing = false;
    public ParticleSystem smokePar;
    public Image hpBar;
    public Image attackRange;
    private float disappearTime;

    public GameObject showDangerObj;

    private GameObject dangerMark;
    private float randomRadian;

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
        float dis = Vector2.Distance(gameObject.transform.position, player.transform.position);

        if (attackCoolDown > 0 && isWarning == false && isSlashing == false && isDisappearing == false)
        {
            attackCoolDown -= Time.deltaTime;
        }

        if (attackCoolDown <= 0 && isWarning == false && isSlashing == false && isDisappearing == false)
        {
            if (dis < gameObject.GetComponent<EnemyStats>().attackRange)
            {
                int mode = Random.Range(0,2);
                if(mode == 0)
                {
                    isWarning = true;
                    warningAnim.GetComponent<FlagControl>().flag = true;
                    warningAnim.GetComponent<Animator>().SetTrigger("playAnim");
                }
                if(mode == 1)
                {
                    isDisappearing = true;
                    randomRadian = Random.Range(0, 6.28f);
                    
                    dangerMark = Instantiate(showDangerObj, player.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                    gameObject.GetComponent<EnemyHealth>().canGetHurt = false;
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    gameObject.GetComponent<CircleCollider2D>().enabled = false;
                    hpBar.enabled = false;
                    attackRange.enabled = false;
                    orangeRange.enabled = false;
                    disappearTime = 3f;
                    smokePar.Play();
                }
                
            }
        }

        if (isWarning == true)
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

        if(isDisappearing == true)
        {
            disappearTime -= Time.deltaTime;

            
            Vector3 myPos = new Vector3(player.transform.position.x + Mathf.Cos(randomRadian), player.transform.position.y + Mathf.Sin(randomRadian), 0);
            if (myPos.x > top_right_cornorObj.transform.position.x - 0.7f)
            {
                myPos.x = top_right_cornorObj.transform.position.x - 0.7f;
            }
            if (myPos.y > top_right_cornorObj.transform.position.y - 0.7f)
            {
                myPos.y = top_right_cornorObj.transform.position.y - 0.7f;
            }

            if (myPos.x < bottom_left_cornorObj.transform.position.x + 0.7f)
            {
                myPos.x = bottom_left_cornorObj.transform.position.x + 0.7f;
            }
            if (myPos.y < bottom_left_cornorObj.transform.position.y + 0.7f)
            {
                myPos.y = bottom_left_cornorObj.transform.position.y + 0.7f;
            }
            Vector3 markDir = myPos - player.transform.position;
            markDir.Normalize();
            dangerMark.transform.position = new Vector3(player.transform.position.x + markDir.x * 0.25f, player.transform.position.y + markDir.y * 0.25f, 0);
           
            dangerMark.GetComponent<FindChildObject>().myChild.GetComponent<Image>().fillAmount = (3 - disappearTime) / 3f;
            
            if (disappearTime <= 0)
            {
                isDisappearing = false;
                smokePar.Play();
                attackCoolDown = 1f;
                gameObject.GetComponent<EnemyHealth>().canGetHurt = true;
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                gameObject.GetComponent<CircleCollider2D>().enabled = true;

                orangeRange.fillAmount = 0;
                gameObject.GetComponent<EnemyHealth>().noHurtTime = 6.5f;
                orangeRange.enabled = true;
                Destroy(dangerMark);

                hpBar.enabled = true;
                attackRange.enabled = true;

                Vector3 newPos = new Vector3(player.transform.position.x + Mathf.Cos(randomRadian), player.transform.position.y + Mathf.Sin(randomRadian), 0);
                if (newPos.x > top_right_cornorObj.transform.position.x - 0.7f)
                {
                    newPos.x = top_right_cornorObj.transform.position.x - 0.7f;
                }
                if (newPos.y > top_right_cornorObj.transform.position.y - 0.7f)
                {
                    newPos.y = top_right_cornorObj.transform.position.y - 0.7f;
                }

                if (newPos.x < bottom_left_cornorObj.transform.position.x + 0.7f)
                {
                    newPos.x = bottom_left_cornorObj.transform.position.x + 0.7f;
                }
                if (newPos.y < bottom_left_cornorObj.transform.position.y + 0.7f)
                {
                    newPos.y = bottom_left_cornorObj.transform.position.y + 0.7f;
                }

                gameObject.transform.position = newPos;

                slashAnim.GetComponent<Animator>().SetTrigger("playSlash");
                Vector2 player_to_enemy = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
                float rotateAngle = Vector2.SignedAngle(Vector2.right, player_to_enemy);
                slashObj.transform.rotation = Quaternion.Euler(0, 0, rotateAngle);
                isSlashing = true;
            }
        }

    }
}
