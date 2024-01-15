using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

namespace Player.WeaponHandler
{
    public class BaseGunController : MonoBehaviour
    {
        #region Initialize Variables
        public Camera _cam;
        public GameObject bodySpine;
        [Header("Gun Settings")]
        
        [HideInInspector] public bool isEquipped = false;
        [HideInInspector] public bool isActive = false;
        public GunID GunID = GunID.DEFAULT; //To identify different guns
        public GunDatabase gunDB;
        private Animator animator;

        bool _canShoot;
        public bool _isReloading; //Yes
        private GunScript activeGun;

        //Muzzle
        public Sprite[] muzzleSprites;

        //Aiming
        public Vector3 normalLocalPosition;
        public Vector3 aimingLocalPosition;
        public float weaponSwayIntensity = -2f;

        //Recoil

        public GameObject bulletHole;


        public float aimSmoothing = 10f;
        #endregion

        private void Awake()
        {
            //ICommon.LoadGunController(this);
            ICommon.LoadBulletHolePrefab(bulletHole);
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void ManualStart()
        {
            //_currentAmmoInClip = activeGun._currentAmmoInClip;
            //_ammoInReserve = activeGun._ammoInReserve;
            _canShoot = true;
            GetActiveGun();
            activeGun.muzzleFlash.color = new Color(0, 0, 0, 0);
        }

        #region Gun Actions
        public void ShootGun()
        {
            GetActiveGun();
            if ( _isReloading || !_canShoot || activeGun._currentAmmoInClip <= 0) { return; }
            _canShoot = false;
            activeGun._currentAmmoInClip -= 1;
            StartCoroutine(FireBullets());
        }

        public void Reload()
        {
            if (_isReloading || activeGun._currentAmmoInClip >= activeGun.clipSize || activeGun._ammoInReserve <= 0) { return; }
            _isReloading = true;
            GetActiveGun();
            GunReloadAnimation();
        }

        private void ReloadComplete()
        {
            int _ammoNeeded = activeGun.clipSize - activeGun._currentAmmoInClip;
            if (_ammoNeeded >= activeGun._ammoInReserve)
            {
                activeGun._currentAmmoInClip += activeGun._ammoInReserve;
                activeGun._ammoInReserve -= _ammoNeeded;
            }
            else
            {
                activeGun._currentAmmoInClip = activeGun.clipSize;
                activeGun._ammoInReserve -= _ammoNeeded;
            }
            _isReloading = false;
        }
        #endregion

        #region  Gun Sub Actions

        IEnumerator FireBullets()
        {
            activeGun.PlayShootingSFX();
            ShootRayCast();
            Recoil();
            StartCoroutine(MuzzleFlash());
            yield return new WaitForSeconds(activeGun.fireRate);
            _canShoot = true;
        }
        IEnumerator MuzzleFlash()
        {
            activeGun.muzzleFlash.sprite = muzzleSprites[Random.Range(0, muzzleSprites.Length)];
            activeGun.muzzleFlash.color = Color.white;
            yield return new WaitForSeconds(0.05f);
            activeGun.muzzleFlash.sprite = null;
            activeGun.muzzleFlash.color = new Color(0, 0, 0, 0);
        }

        private void ShootRayCast()
        {
            if (activeGun.GunType == GunType.SHOTGUN)
            {
                ShootMultipleRayCasts();
            }else
            {
                ShootSingleRayCast();
            }
        }

        private void ShootSingleRayCast()
        {
            RaycastHit hit;
            //Debug.DrawRay(transform.parent.position, transform.parent.forward * 100);
            if (Physics.Raycast(_cam.transform.position + _cam.transform.forward*1, _cam.transform.forward, out hit))
            {
                float dirSign = Mathf.Sign(Vector3.Dot(_cam.transform.position, hit.point));
                //Debug.Log(hit.normal);
                //Debug.LogWarning(hit.transform.gameObject.name);
                ICommon.CheckForHits(hit, activeGun.baseDamage);

                //Instantiate(bulletHole, hit.point + new Vector3(hit.normal.x * 0.01f, hit.normal.y * 0.01f, hit.normal.z * 0.01f), Quaternion.LookRotation(-hit.normal));
            }

        }

        private void ShootMultipleRayCasts()
        {
            int spread = Random.Range(5,8);
            for (int i = 0; i< spread; i++)
            {
                Vector3 rand = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
                Vector3 random_direction = Quaternion.Euler(rand) * _cam.transform.forward;
                RaycastHit hit;
                if (Physics.Raycast(_cam.transform.position + _cam.transform.forward*1, random_direction, out hit))
                {
                    float dirSign = Mathf.Sign(Vector3.Dot(_cam.transform.position, hit.point));
                    //Debug.Log(hit.normal);
                    //Debug.LogWarning(hit.transform.gameObject.name);
                    ICommon.CheckForHits(hit, activeGun.baseDamage);

                    //Instantiate(bulletHole, hit.point + new Vector3(hit.normal.x * 0.01f, hit.normal.y * 0.01f, hit.normal.z * 0.01f), Quaternion.LookRotation(-hit.normal));
                }
            }

        }

        public void DetermineAim(bool isAiming)
        {
            Vector3 target_pos = normalLocalPosition;
            if (isAiming)
            {
                target_pos = aimingLocalPosition;
            }
            Vector3 transitionPos = Vector3.Lerp(transform.localPosition, target_pos, Time.deltaTime * aimSmoothing);
            transform.localPosition = transitionPos;
        }

        private float xRotation = 0f;
        public void HandleRotation(Vector2 input)
        {
            
            //Transform parent_rotation = transform.parent;
            //Quaternion cam_rotation = _cam.transform.localRotation;
            ////Quaternion target_rotation = Quaternion.Euler(cam_rotation.x, parent_rotation.transform.localRotation.y, parent_rotation.transform.localRotation.z);
            //parent_rotation.localRotation = cam_rotation;

            if(!_isReloading)
            {
                transform.localPosition += (Vector3)input * weaponSwayIntensity / 1000;
            }

            //Debug.Log(input);
            //float mouseX = input.x;
            float mouseY = input.y;

            xRotation -= (mouseY * Time.deltaTime) * PlayerController.ySensitivity;
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);

            //_cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            transform.parent.localRotation = Quaternion.Euler(xRotation, 0, 0);
            Components.PlayerBodyAnimation.SyncCharacterHeadRotation(xRotation);

            //PlayerController.playerTransform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * PlayerController.xSensitivity);
        }

        private void Recoil()
        {
            transform.localPosition -= Vector3.forward * activeGun.recoil/10f; //Pure gun recoiling visual effect

            float xRecoil = Random.Range(-activeGun.randomRecoilConstraints.x, activeGun.randomRecoilConstraints.x);
            float yRecoil = Random.Range(-activeGun.randomRecoilConstraints.y, activeGun.randomRecoilConstraints.y);

            Vector2 recoil = new Vector2(xRecoil, yRecoil);
            xRotation -= Mathf.Abs(xRecoil);

            PlayerController.playerTransform.Rotate(Vector3.up * yRecoil);

        }

        private void GetActiveGun()
        {
            activeGun = PlayerWeapons.Instance.currentActiveGun;
        }
        #endregion

        #region Gun Animations

        public void GunSwitchAnimation()
        {
           StartCoroutine(CoGunSwitchAnimation());
        }
        IEnumerator CoGunSwitchAnimation()
        {
            animator.SetBool("Reloading", false);
            animator.enabled = false;
            animator.enabled = true;
            animator.SetBool("Switching", true);
            yield return new WaitForSeconds(0.25f);
            animator.SetBool("Switching", false);
            yield return new WaitForSeconds(0.05f);
            animator.enabled = false;
        }

        public void GunReloadAnimation()
        {
            StartCoroutine(CoReloadGun());
        }

        IEnumerator CoReloadGun()
        {
            animator.enabled = false;
            yield return new WaitForSeconds(0.02f);
            activeGun.PlayReloadingSFX();
            animator.enabled = true;
            animator.SetBool("Reloading", true);
            yield return new WaitForSeconds(activeGun.reloadTime-0.25f);
            animator.SetBool("Reloading", false);
            ReloadComplete();
            yield return new WaitForSeconds(0.25f);
            animator.enabled = false;
        }
        #endregion
    }
}


