using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    public int myPoint;
    private int[] pointType = new int[4];
    public GameObject[] pointsObj = new GameObject[4];

    public float nowHealth;
    public float maxHealth;

    public Color hurtColor;
    public Color normalColor;
    public Color HpBarNormalColor;
    public float hurtTime;

    public Transform dirPointer;

    public GameObject yellowRange;

    public float noHurtTime = 6.5f;
    public Image orangeRange;

    public GameObject hitEffect;

    private GameObject player;

    public GameObject pokeEffect;

    public GameObject damageText;

    public Transform damageSpawn;

    public Image myHealthBar;

    public Color critColor;

    public bool canGetHurt = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
       if(hurtTime > 0)
       {
            hurtTime -= Time.deltaTime;
            if(hurtTime <= 0)
            {
                myHealthBar.color = HpBarNormalColor;
                gameObject.GetComponent<SpriteRenderer>().color = normalColor;
            }

       }
       if(noHurtTime > 0)
       {
            noHurtTime -= Time.deltaTime;

            orangeRange.fillAmount =  ((6.5f - noHurtTime)/ 6.5f) * (gameObject.GetComponent<EnemyStats>().attackRadius / 360f);
            if(noHurtTime <= 0)
            {
                noHurtTime = 6.5f;
                //change Range
                float switchAngle = Random.Range(yellowRange.transform.rotation.eulerAngles.z + gameObject.GetComponent<EnemyStats>().attackRadius, yellowRange.transform.rotation.eulerAngles.z + 360 - gameObject.GetComponent<EnemyStats>().attackRadius);
                if (switchAngle > 360)
                {
                    switchAngle -= 360;
                }
                yellowRange.transform.rotation = Quaternion.Euler(0, 0, switchAngle);
            }
       }
        
    }

    public void Hurt(float dmg,int attackMode)
    {

        nowHealth -= (int)dmg;

        myHealthBar.fillAmount = nowHealth / maxHealth;
        hurtTime = 0.25f;
        myHealthBar.color = hurtColor;
        gameObject.GetComponent<SpriteRenderer>().color = hurtColor;

        GameObject myDmgText = Instantiate(damageText, damageSpawn.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        myDmgText.GetComponent<FindChildObject>().myChild.GetComponent<TextMeshProUGUI>().text = ((int)dmg).ToString();

        Vector2 player_to_enemy = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
       

        //hitParticle
        float hitEffectAngle = Vector2.SignedAngle(player_to_enemy, Vector2.right) + 180;
        GameObject hitEff = Instantiate(hitEffect, transform.position, Quaternion.Euler(new Vector3(hitEffectAngle, 90, 0)));
        ParticleSystem.MainModule col = hitEff.GetComponent<ParticleSystem>().main;
        col.startColor = normalColor;

        //pokeAnim
        float pokeEffectAngle = Vector2.SignedAngle(Vector2.up, player_to_enemy);
        GameObject pokeEff = Instantiate(pokeEffect, transform.position, Quaternion.Euler(new Vector3(0, 0, pokeEffectAngle+180)));
        if(attackMode == 1)
        {
            pokeEff.GetComponent<FindChildObject>().myChild.GetComponent<SpriteRenderer>().color = critColor;
        }

        if(nowHealth <= 0)
        {
            GeneratePoints();
        }

        //changeAttackRange
        noHurtTime = 6.5f;
        float switchAngle = Random.Range(yellowRange.transform.rotation.eulerAngles.z + gameObject.GetComponent<EnemyStats>().attackRadius, yellowRange.transform.rotation.eulerAngles.z + 360 - gameObject.GetComponent<EnemyStats>().attackRadius);
        if (switchAngle > 360)
        {
            switchAngle -= 360;
        }
        yellowRange.transform.rotation = Quaternion.Euler(0, 0, switchAngle);

    }

    public void GeneratePoints()
    {
        //10,50,250,1250
        if(myPoint >= 1250)
        {
            pointType[3] = myPoint / 1250;
            myPoint -= 1250 * (myPoint / 1250);
        }
        if (myPoint >= 250)
        {
            pointType[2] = myPoint / 250;
            myPoint -= 250 * (myPoint / 250);
        }
        if (myPoint >= 50)
        {
            pointType[1] = myPoint / 50;
            myPoint -= 50 * (myPoint / 50);
        }
        if (myPoint >= 10)
        {
            pointType[0] = myPoint / 10;
            myPoint -= 10 * (myPoint / 10);
        }

        for(int i = 0; i <= 3; i++)
        {
            for (int j = 0; j < pointType[i]; j++)
            {
                Instantiate(pointsObj[i], transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            }         
        }

        Destroy(gameObject);
    }
}
