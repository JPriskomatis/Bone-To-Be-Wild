using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Interaction
{
    public class PickUpWeapon : MonoBehaviour, IInteractable
    {

        [SerializeField] private GameObject weapon;

        [SerializeField] private GameObject weaponHolder;

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
        private void WeaponPickUp()
        {
            GameObject activeWeapo = CheckForActiveWeapon();
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

        public void Interact()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                WeaponPickUp();
                Destroy(gameObject);
            }
        }

        public void OnInteractEnter()
        {
            TextAppear.SetText("Interact");
        }

        public void OnInteractExit()
        {
            TextAppear.SetText("End of Interaction");
        }
    }

}