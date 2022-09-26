using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlash : MonoBehaviour
{

    public int damage;
    private GameObject player;
    public GameObject rangeChecker;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        if(rangeChecker.GetComponent<DetectAttackRange>().playerInRange == true)
        {
            player.GetComponent<PlayerHealth>().GetDamage(damage);
        }
       
    }
}
