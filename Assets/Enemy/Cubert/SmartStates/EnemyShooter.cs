using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [Header("General")]

    public Transform shootPoint;    // Where the raycast starts from
    public Transform gunPoint;      // Where the visual trail starts from
    public LayerMask layerMask;     // The ... layerMask ... to mask

    [Header("Gun")]
    public Vector3 spread = new Vector3(0.06f, 0.06f, 0.06f);

    public TrailRenderer bulletTrail;

    public int ammo = 30;

    private EnemyReferences enemyReferences;
    private int currentAmmo;    
    
    void Awake() {
        enemyReferences = GetComponent<EnemyReferences>();
        Reload();
    }

    public void Shoot() {

        if (ShouldReload()) return;         // for Smart one

        Vector3 direction = GetDirection();
        if (Physics.Raycast(shootPoint.position, direction, out RaycastHit hit, float.MaxValue, layerMask)) {
            Debug.DrawLine(shootPoint.position, shootPoint.position + direction * 10f, Color.red, 1f);

            //TODO: Bad Performance. Replace with Object Pooling.
            TrailRender trail = Instantiate(bulletTrail, gunPoint.position, Quaternion.identity);
            StartCoroutine(SpawnTrail(trail, hit));

            currentAmmo -= 1;
        }
    }

    public bool ShouldReload() {
        return currentAmmo <= 0;
    }

    public void Reload() {
        Debug.Log("Reload");
        currentAmmo = ammo;
    }
}
