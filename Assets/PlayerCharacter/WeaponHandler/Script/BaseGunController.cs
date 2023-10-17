using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

namespace Player.WeaponHandler
{
    public class BaseGunController : MonoBehaviour
    {
        [Header("Gun Settings")]
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

        //Recoil
        public bool randomRecoil;
        public Vector2 randomRecoilConstraints;


        public float aimSmoothing = 10f;

        private void Start()
        {
            _currentAmmoInClip = clipSize;
            _ammoInReserve = reservedAmmo;
            _canShoot = true;
        }

        public void ShootGun()
        {
            if (!_canShoot || _currentAmmoInClip <= 0) { return; }
            _canShoot = false;
            _currentAmmoInClip -= 1;
            StartCoroutine(FireBullets());
        }

        IEnumerator FireBullets()
        {
            Recoil();
            StartCoroutine(MuzzleFlash());
            yield return new WaitForSeconds(fireRate);
            _canShoot = true;
        }

        public void Reload()
        {
            if (_currentAmmoInClip >= clipSize || _ammoInReserve <= 0) { return; }
            int _ammoNeeded = clipSize - _currentAmmoInClip;
            if(_ammoNeeded >= _ammoInReserve)
            {
                _currentAmmoInClip += _ammoInReserve;
                _ammoInReserve -= _ammoNeeded;
            }else
            {
                _currentAmmoInClip = clipSize;
                _ammoInReserve -= _ammoNeeded;
            }
        }

        IEnumerator MuzzleFlash()
        {
            muzzleFlash.sprite = muzzleSprites[Random.Range(0, muzzleSprites.Length)];
            muzzleFlash.color = Color.white;
            yield return new WaitForSeconds(0.05f);
            muzzleFlash.sprite = null;
            muzzleFlash.color = new Color(0, 0, 0, 0);
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

        public void HandleRotation(Vector2 input)
        {
            Debug.Log(input);
        }

        private void Recoil()
        {
            transform.localPosition -= Vector3.forward * 0.1f;
        }
    }
}


