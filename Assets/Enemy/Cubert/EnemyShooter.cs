using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

//using System;
using UnityEngine;

public class EnemyShooter : MonoBehaviour, ICombat
{
    
    [Header("General")]

    public float ClipLength = 1f;
    
    public GameObject AudioClip;

    public Transform shootPoint;    // Where the raycast starts from

    public Transform gunPoint;      // Where the visual trail starts from

    public LayerMask layerMask;     //The ... layerMask ... to mask

    [Header("Gun")]

    public Vector3 spread = new Vector3(0.06f, 0.06f, 0.06f);

    public TrailRenderer bulletTrail;

    private EnemyReferences enemyReferences;

    public int ammo = 30;

    public float baseDamage = 20;
    public float baseHP = 100;

    private int currentAmmo;
    
    void Start() {
        AudioClip.SetActive(false);
    }
    
    void Awake() {
        enemyReferences = GetComponent<EnemyReferences>();
        Reload();    
    }

    public void Shoot() {

        if ( ShouldReload() ) return;   // for Smart one

        Vector3 direction = GetDirection();
        if (Physics.Raycast(shootPoint.position, direction, out RaycastHit hit, float.MaxValue, layerMask)) {
            Debug.DrawLine(shootPoint.position, shootPoint.position + direction * 10f, Color.red, 1f);

            TrailRenderer trail = Instantiate(bulletTrail, gunPoint.position, Quaternion.identity);
            StartCoroutine(SpawnTrail(trail, hit));
            currentAmmo -= 1;
            ICommon.CheckForHits(hit, baseDamage);

            //Sound FX
            StartCoroutine(PlayAudioForDuration());


        }
    }

    private IEnumerator PlayAudioForDuration() {
        AudioClip.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        AudioClip.SetActive(false);

    }
    
    private Vector3 GetDirection() {
        Vector3 direction = transform.forward;
        direction += new Vector3(
            UnityEngine.Random.Range(-spread.x, spread.x),
            UnityEngine.Random.Range(-spread.y, spread.y),
            UnityEngine.Random.Range(-spread.z, spread.z)
        );
        direction.Normalize();
        return direction;
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit) {
        float time = 0f;
        Vector3 startPosition = trail.transform.position;

        while (time < 1f) {
            trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += Time.deltaTime / trail.time;

            yield return null;
        }

        trail.transform.position = hit.point;

        Destroy(trail.gameObject, trail.time);
    }

    public bool ShouldReload() {
        return currentAmmo <= 0;
    }

    public void Reload() {
        Debug.Log("Reloaded");
        currentAmmo = ammo;
    }

    #region Combat
    // TODO: Do something when bot takes damages
    public void TakeDamage(float dmg) 
    {
        baseHP -= dmg;
        if (baseHP <= 0)
        {
            //Destroy(this.gameObject); // TODO: Do something when bot dies
            Dies();
        }
        //Debug.LogWarning(baseHP);

    }

    public void RestoreHealth(float hp)
    {

    }
    #endregion

    #region Combat Others
    private void Dies()
    {
        try
        {
            CharacterJoint anyJoint = GetComponentInChildren<CharacterJoint>();
            if (anyJoint)
            {
                enemyReferences.navMeshAgent.enabled = false;
                enemyReferences.animator.enabled = false;
                enemyReferences.shooter.enabled = false;
                enemyReferences.brain.enabled = false;
                CapsuleCollider capsuleCollider = GetComponent<CapsuleCollider>();
                capsuleCollider.enabled = false;

            }else // Just destroy the object if ragdoll is not implemented
            {
                Debug.LogWarning("Ragdoll is not implemented");
                Destroy(gameObject);
            }
        }
        catch(Exception err)
        {
            Debug.LogWarning(err + " - Ragdoll is not implemented");
        }
    }

    #endregion


}
