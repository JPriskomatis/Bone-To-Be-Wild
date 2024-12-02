using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilitySpace;
using PlayerSpace;

namespace InventorySpace
{
    [CreateAssetMenu(fileName = "New Potion", menuName = "Items/Create New Potion")]
    public class PotionSO : ItemSO
    {
        [Header("Potion heal amount")]
        public int healAmount;

        protected override void Activate()
        {
            Debug.Log("Healed for :" + healAmount);
            AbilityScores.Instance.IncreaseStat(AbilityScores.StatType.CurrentHP, healAmount);

            //Remove item from the Inventory;
            Inventory.Instance.Remove(this);
        }

    }

}