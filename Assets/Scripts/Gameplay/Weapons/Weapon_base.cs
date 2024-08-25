using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerSpace;

namespace WeaponSpace
{
    public class Weapon_base : MonoBehaviour
    {
        public int damage = 0;
        public IDoDamage damageType;

        public int playerStrength;

        private void Start()
        {
            playerStrength = GetComponentInParent<AbilityScores>().mainStats.strength;
        }

        public void TryDoAttack()
        {
            
            damageType?.DoDamage(damage + playerStrength);
        }

        //To use this function do the following:
        //
        public void SetDamageType(IDoDamage weaponType)
        {
            this.damageType = weaponType;
        }
    }

}