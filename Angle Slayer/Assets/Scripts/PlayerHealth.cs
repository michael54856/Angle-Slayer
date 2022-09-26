using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public float nowHealth;
    public float yellowHealth;
    public float maxHealth;


    public GameObject hitParticle;

    public Animator cameraANIM;

    public TextMeshProUGUI hpText;
    public Image hpBar;
    public Image yellowBar;

    public GameObject damageCanvas;

    public GameObject hpBarEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(yellowHealth > nowHealth)
        {
            float adj = (yellowHealth - nowHealth) * 1.5f;
            if(adj < 20)
            {
                adj = 20;
            }
            yellowHealth -= Time.deltaTime * adj;
            if(yellowHealth <= nowHealth)
            {
                yellowHealth = nowHealth;
            }
        }
        if (nowHealth > yellowHealth)
        {
            yellowHealth = nowHealth;
        }

        yellowBar.fillAmount = yellowHealth / maxHealth;

    }

    public void GetDamage(float dmg)
    {
        hpBarEffect.GetComponent<RectTransform>().anchoredPosition = new Vector3(80 + (nowHealth / maxHealth) * 330, -80, 0);

        nowHealth -= dmg;
        hpBar.fillAmount = nowHealth / maxHealth;
        hpText.text = nowHealth.ToString() + "/" + maxHealth.ToString();

        GameObject myDmgText = Instantiate(damageCanvas, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        myDmgText.GetComponent<FindChildObject>().myChild.GetComponent<TextMeshProUGUI>().text = ((int)dmg).ToString();

       
        Instantiate(hitParticle, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        hpBarEffect.GetComponent<ParticleSystem>().Play();

        hpBarEffect.GetComponent<RectTransform>().anchoredPosition = new Vector3(80 + (nowHealth / maxHealth) * 330, -80, 0);
        cameraANIM.SetTrigger("shake");
    }

 
}
