using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health Bar Settings")]
    public float HP;
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
    public GameObject HUD;

    private void Start()
    {
        HP = maxHP;
        dmgOverlay.color = new Color(dmgOverlay.color.r, dmgOverlay.color.g, dmgOverlay.color.b, 0);
        InitPlayerStatsForServer();
    }

    private void Update()
    {
        UpdateHPForServer();
        HP = Mathf.Clamp(HP, 0, maxHP);
        UpdateHealthUI();
        //TestDmg();
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

    public void RestoreFullHealth()
    {
        if(ICommon.GetPlayerController().gameMode == GameMode.Multiplayer)
        {
            Hashtable hash = new Hashtable();
            hash.Add(ICommon.GetPlayerGameObject().name, maxHP);
            PhotonNetwork.CurrentRoom.SetCustomProperties(hash);   
        }else
        {
            RestoreHealth(maxHP);

        }
    }

    
    [Photon.Pun.PunRPC]
    public float TakeDamage(float dmg)
    {
        HP = Mathf.Clamp(HP - dmg, 0, maxHP);
        if(HP == 0)
        {
            if(ICommon.GetPlayerController().gameMode == GameMode.Multiplayer)
            {
                PlayerSpawner.Instance.Die();
                //this.gameObject.SetActive(false);
            }else
            {
                //TO DO: for singleplayer mode, switch to Extraction Unsuccessful screen
            }
        }
        lerpTimer = 0f;
        durationTimer = 0;
        dmgOverlay.color = new Color(dmgOverlay.color.r, dmgOverlay.color.g, dmgOverlay.color.b, maxOverlayOpacity);
        return HP;
    }

    public void RestoreHealth(float amount)
    {
        HP = Mathf.Clamp(HP + amount, 0, maxHP);
        lerpTimer = 0f;
    }

    public void UpdateHPForServer()
    {
        if(ICommon.GetPlayerController().gameMode == GameMode.SinglePlayer){return;}


        float HPfromServer = (float)PhotonNetwork.CurrentRoom.CustomProperties[ICommon.GetPlayerGameObject().name];
        if (HPfromServer > HP)
        {
            RestoreHealth(HPfromServer - HP);
        }else if(HPfromServer < HP)
        {
            TakeDamage(HP-HPfromServer);
        }
        //HP = HPfromServer;
        
        // Hashtable hash = new Hashtable();
        // hash.Add(this.gameObject.name, HP);
        // PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }


    private void InitPlayerStatsForServer()
    {
        //TODO: this should moved into PlayerController
        {
            Hashtable hash = new Hashtable();
            hash.Add(this.gameObject.name, HP);
            PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
        }
        {
            Hashtable hash = new Hashtable();
            hash.Add(this.gameObject.name+ ICommon.CustomProperties_Key_KillsCount(), 2);
            PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
        }
        {
            Hashtable hash = new Hashtable();
            hash.Add(this.gameObject.name+ ICommon.CustomProperties_Key_DeathsCount(), 3);
            PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
        }

    }
}
