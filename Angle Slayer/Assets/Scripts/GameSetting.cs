using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    private float countTime = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

    }

    // Update is called once per frame
    void Update()
    {
        countTime -= Time.deltaTime;
        if(countTime < 0)
        {
            fpsText.text = "FPS: " + (int)(1f / Time.deltaTime);
            countTime = 0.2f;
        }
        
    }
}
