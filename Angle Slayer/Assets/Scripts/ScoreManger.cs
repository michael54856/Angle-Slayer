using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManger : MonoBehaviour
{
    public float showScore = 0;
    public float nowRealScore = 0;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(nowRealScore > showScore)
        {
            showScore += 2000 * Time.deltaTime;
            scoreText.text = ((int)showScore).ToString();
        }
        if (showScore > nowRealScore)
        {
            showScore = nowRealScore;
            scoreText.text = ((int)showScore).ToString();
        }

    }
}
