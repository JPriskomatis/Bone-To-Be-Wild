using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace InventorySpace
{
    public class PickUpWeapon : MonoBehaviour
    {

        [Header("Weapon settings")]
        [SerializeField] private GameObject weapon;
        [SerializeField] private GameObject weaponHolder;
        private WeaponSO currentWeaponSO;
        public enum WeaponName
        {
            normalSword,
            magicSword
        }

        public WeaponName weaponName;
        private void Start()
        {
            weaponHolder = GameObject.FindGameObjectWithTag("WeapoHold");
        }

        private GameObject CheckForActiveWeapon()
        {
            foreach (Transform child in weaponHolder.transform)
            {
                if (child.gameObject.activeInHierarchy)
                {
                    return child.gameObject;
                }
            }
            return null;
        }
        public void SetItemSO(WeaponSO weaponToPlace)
        {
            currentWeaponSO = weaponToPlace;
        }
        public void WeaponPickUp()
        {
            weaponHolder = GameObject.FindGameObjectWithTag("WeaponHold");

            if (weaponHolder == null)
            {
                Debug.Log("Weapon holder null");
            }
            else
            {
                Debug.Log("Weapont holder NOT null");
            }

            GameObject activeWeapo = CheckForActiveWeapon();
            CheckWhichWeaponToEquip();
            EquipmentOfWeapon(activeWeapo);

        }

        private void EquipmentOfWeapon(GameObject activeWeapo)
        {
            EquipItemManager.Instance.EquipItem(currentWeaponSO);


            if (activeWeapo != null)
            {
                activeWeapo.SetActive(false);
                weapon.SetActive(true);
            }
            else
            {
                weapon.SetActive(true);
            }
        }

        private void CheckWhichWeaponToEquip()
        {
            if (weaponName == WeaponName.normalSword)
            {
                weapon = weaponHolder.transform.GetChild(0).gameObject;
            }
            else
            {
                weapon = weaponHolder.transform.GetChild(1).gameObject;
            }
        }
    }

}