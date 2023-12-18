using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health Bar Settings")]
    private float HP;
    private float lerpTimer;
    public float maxHP = 100f;
    public float chipSpeed = 2f;
    public TextMeshProUGUI HP_Value;
    public Image frontHealthBar;
    public Image backHealthBar;

    [Header("Damage Overlay")]
    public Image dmgOverlay;
    public float overlayDuration = 0.5f;
    public float fadeSpeed = 1.5f;
    public float maxOverlayOpacity = 0.7f;
    public float lowHealthThreshold = 20;

    private float durationTimer;
    public float playerHP;

    private void Start()
    {
        HP = maxHP;
        dmgOverlay.color = new Color(dmgOverlay.color.r, dmgOverlay.color.g, dmgOverlay.color.b, 0);
    }

    private void Update()
    {
        HP = Mathf.Clamp(HP, 0, maxHP);
        UpdateHealthUI();
        TestDmg();
        if (dmgOverlay.color.a > 0)
        {
            if (HP <= lowHealthThreshold)
            {
                return;
            }
            durationTimer += Time.deltaTime;
            if (durationTimer > overlayDuration)
            {
                float tempAlpha = dmgOverlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                dmgOverlay.color = new Color(dmgOverlay.color.r, dmgOverlay.color.g, dmgOverlay.color.b, tempAlpha);
            }
        }
    }

    public bool isDeath()
    {
        if (HP == 0)
        {
            return true;
        }
        return false;
    }

    private void TestDmg()
    {
        if(true)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                TakeDamage(Random.Range(5, 20));
            } 
            if (Input.GetKeyDown(KeyCode.C))
            {
                RestoreHealth(Random.Range(5, 20));
            }
        }
    }

    public void UpdateHealthUI()
    {
        HP_Value.text = HP.ToString() + "/" + maxHP.ToString();
        float frontFill = frontHealthBar.fillAmount;
        float backFill = backHealthBar.fillAmount;
        float HP_ratio = HP / maxHP;
        if(backFill > HP_ratio)
        {
            frontHealthBar.fillAmount = HP_ratio;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(backFill, HP_ratio, percentComplete);
        }
        if (frontFill < HP_ratio)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = HP_ratio;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(frontFill, backHealthBar.fillAmount, percentComplete);
        }
    }

    public void TakeDamage(float dmg)
    {
        HP -= dmg;
        lerpTimer = 0f;
        durationTimer = 0;
        dmgOverlay.color = new Color(dmgOverlay.color.r, dmgOverlay.color.g, dmgOverlay.color.b, maxOverlayOpacity);
    }

    public void RestoreHealth(float amount)
    {
        HP += amount;
        lerpTimer = 0f;
    }
}
