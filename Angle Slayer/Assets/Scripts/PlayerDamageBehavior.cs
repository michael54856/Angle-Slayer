using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDamageBehavior : MonoBehaviour
{
    private GameObject textObj;

    public Color textColor;

    public float X_speed;
    public float Y_speed;

    private float addSpeed;

    private float bigTime = 0.4f;

    private float countTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        textObj = gameObject.GetComponent<FindChildObject>().myChild;
        textObj.GetComponent<TextMeshProUGUI>().fontSize = 0.1f;

        X_speed = Random.Range(0.5f,0.75f);
        Y_speed = Random.Range(2.8f, 3.2f);
        addSpeed = Y_speed * 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(textColor.a < 0.2f)
        {
            Destroy(gameObject);
        }

        Y_speed -= Time.deltaTime * addSpeed;

        Vector2 dir = new Vector2(X_speed,Y_speed);
        transform.Translate(dir * Time.deltaTime);

        countTimer += Time.deltaTime;
        if(countTimer > 0.8f)
        {
            textColor.a -= Time.deltaTime * 1.1f;
            textObj.GetComponent<TextMeshProUGUI>().color = textColor;
        }

        if(bigTime > 0)
        {
            bigTime -= Time.deltaTime;
            textObj.GetComponent<TextMeshProUGUI>().fontSize += Time.deltaTime;
        }


        
    }
}
