using System;
using System.Net;
using UnityEngine;

namespace InventorySpace
{



    [CreateAssetMenu(fileName = "New Item", menuName = "Items/Create New Weapon")]
    public class WeaponSO : ItemSO
    {
        [Header("Weapon field")]
        public GameObject weaponPrefab;


        protected override void Equip()
        {
            weaponPrefab.GetComponent<PickUpWeapon>().SetItemSO(this);
            weaponPrefab.GetComponent<PickUpWeapon>().WeaponPickUp();
            
        }



    }

}