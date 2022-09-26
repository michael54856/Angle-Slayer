using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectAttackRange : MonoBehaviour
{
    public bool playerInRange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D aaa) 
    {
        if (aaa.gameObject.tag == "Player") 
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D aaa) 
    {
        if (aaa.gameObject.tag == "Player") 
        {
            playerInRange = false;
        }
    }
}
