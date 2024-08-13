using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interaction
{
    public class PickUpWeapon : MonoBehaviour
    {

        [SerializeField] private GameObject weapon;
        private void OnEnable()
        {
            Test_Object.OnWeaponPickUp += WeaponPickUp;
        }

        private void OnDisable()
        {
            Test_Object.OnWeaponPickUp += WeaponPickUp;
        }

        private void WeaponPickUp()
        {
            weapon.SetActive(true);
        }
    }

}