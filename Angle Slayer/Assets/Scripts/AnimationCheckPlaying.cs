using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCheckPlaying : MonoBehaviour
{
    public Animator anim;
    public string animName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(animName))
        {
            // Avoid any reload.
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
