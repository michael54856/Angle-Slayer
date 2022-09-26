using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpikeBehaviour : MonoBehaviour
{
    public float damage;

    public Image spikeEdge; 

    private bool startCoolDown = false;
    private float coolDownTime;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(startCoolDown == true)
        {
            coolDownTime -= Time.deltaTime;
            spikeEdge.fillAmount = (5f - coolDownTime) / 5f;
            if (coolDownTime <= 0)
            {
                startCoolDown = false;
                spikeEdge.fillAmount = 0;
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                gameObject.GetComponent<PolygonCollider2D>().enabled = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D playerBall)
    {
        if(playerBall.tag == "Player")
        {
            playerBall.gameObject.GetComponent<PlayerHealth>().GetDamage(damage);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;

            startCoolDown = true;
            coolDownTime = 5f;
        }
    }
}
