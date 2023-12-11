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
        [Header("Gun Settings")]
        public float baseDamage = 30;
        public float fireRate = 0.1f;
        public int clipSize = 30;
        public int reservedAmmo = 270;

        bool _canShoot;
        int _currentAmmoInClip;
        int _ammoInReserve;

        //Muzzle
        public Image muzzleFlash;
        public Sprite[] muzzleSprites;

        //Aiming
        public Vector3 normalLocalPosition;
        public Vector3 aimingLocalPosition;
        public float weaponSwayIntensity = -2f;

        //Recoil
        public Vector2 randomRecoilConstraints = new Vector2(2,5);

        public GameObject bulletHole;


        public float aimSmoothing = 10f;
        #endregion

        private void Awake()
        {
            ICommon.LoadBulletHolePrefab(bulletHole);
        }

        private void Start()
        {
            _currentAmmoInClip = clipSize;
            _ammoInReserve = reservedAmmo;
            _canShoot = true;
            muzzleFlash.color = new Color(0, 0, 0, 0);
        }

        #region Gun Actions
        public void ShootGun()
        {
            if (!_canShoot || _currentAmmoInClip <= 0) { return; }
            _canShoot = false;
            _currentAmmoInClip -= 1;
            StartCoroutine(FireBullets());
        }

        public void Reload()
        {
            if (_currentAmmoInClip >= clipSize || _ammoInReserve <= 0) { return; }
            int _ammoNeeded = clipSize - _currentAmmoInClip;
            if (_ammoNeeded >= _ammoInReserve)
            {
                _currentAmmoInClip += _ammoInReserve;
                _ammoInReserve -= _ammoNeeded;
            }
            else
            {
                _currentAmmoInClip = clipSize;
                _ammoInReserve -= _ammoNeeded;
            }
        }
        #endregion



        IEnumerator FireBullets()
        {
            ShootRayCast();
            Recoil();
            StartCoroutine(MuzzleFlash());
            yield return new WaitForSeconds(fireRate);
            _canShoot = true;
        }
        IEnumerator MuzzleFlash()
        {
            muzzleFlash.sprite = muzzleSprites[Random.Range(0, muzzleSprites.Length)];
            muzzleFlash.color = Color.white;
            yield return new WaitForSeconds(0.05f);
            muzzleFlash.sprite = null;
            muzzleFlash.color = new Color(0, 0, 0, 0);
        }

        private void ShootRayCast()
        {
            RaycastHit hit;
            //Debug.DrawRay(transform.parent.position, transform.parent.forward * 100);
            if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out hit))
            {
                float dirSign = Mathf.Sign(Vector3.Dot(_cam.transform.position, hit.point));
                Debug.Log(hit.normal);
                Debug.LogWarning(hit.transform.gameObject.name);
                ICommon.CheckForHits(hit, baseDamage);

                //Instantiate(bulletHole, hit.point + new Vector3(hit.normal.x * 0.01f, hit.normal.y * 0.01f, hit.normal.z * 0.01f), Quaternion.LookRotation(-hit.normal));
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

            transform.localPosition += (Vector3)input * weaponSwayIntensity / 1000;

            //Debug.Log(input);
            //float mouseX = input.x;
            float mouseY = input.y;

            xRotation -= (mouseY * Time.deltaTime) * PlayerController.ySensitivity;
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);

            _cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            transform.parent.localRotation = _cam.transform.localRotation;
            Components.PlayerBodyAnimation.SyncCharacterHeadRotation(xRotation);

            //PlayerController.playerTransform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * PlayerController.xSensitivity);
        }

        private void Recoil()
        {
            transform.localPosition -= Vector3.forward * 0.1f; //Pure gun recoiling visual effect

            float xRecoil = Random.Range(-randomRecoilConstraints.x, randomRecoilConstraints.x);
            float yRecoil = Random.Range(-randomRecoilConstraints.y, randomRecoilConstraints.y);

            Vector2 recoil = new Vector2(xRecoil, yRecoil);
            xRotation -= Mathf.Abs(xRecoil);

            PlayerController.playerTransform.Rotate(Vector3.up * yRecoil);

        }

    }
}


