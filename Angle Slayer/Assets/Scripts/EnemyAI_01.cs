using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_01 : MonoBehaviour
{
    public GameObject player;
    public GameObject spikeObj;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        spikeObj.transform.Rotate(0, 0, 60*Time.deltaTime);
    }
    


}

