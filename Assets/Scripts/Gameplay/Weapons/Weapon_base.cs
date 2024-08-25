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
            if (CriticalStrike())
            {
                damageType?.DoDamage(damage*2 + playerStrength);
            }
            else
            {
                damageType?.DoDamage(damage + playerStrength);

            }
        }

        public bool CriticalStrike()
        {
            int randomNumber = Random.Range(0, 100);
            if (randomNumber > 50)
            {
                Debug.Log("Critical Strike");
                return true;
            }
            else
                return false;
        }

        //To use this function do the following:
        //
        public void SetDamageType(IDoDamage weaponType)
        {
            this.damageType = weaponType;
        }
    }

}