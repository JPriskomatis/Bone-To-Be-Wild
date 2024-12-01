using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace InventorySpace
{
    public class PickUpWeapon : MonoBehaviour
    {

        [SerializeField] private GameObject weapon;

        [SerializeField] private GameObject weaponHolder;

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
        public void WeaponPickUp()
        {
            weaponHolder = GameObject.FindGameObjectWithTag("WeaponHold");
            if(weaponHolder == null)
            {
                Debug.Log("Weapon holder null");
            }
            else
            {
                Debug.Log("Weapont holder NOT null");
            }

            GameObject activeWeapo = CheckForActiveWeapon();
            weapon = weaponHolder.transform.GetChild(0).gameObject;
            if(activeWeapo != null)
            {
                activeWeapo.SetActive(false);
                weapon.SetActive(true);
            }
            else
            {
                weapon.SetActive(true);
            }

        }
    }

}