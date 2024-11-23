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
        public int playerLuck;

        private int extraDamageAbility;

        private void Start()
        {
            playerStrength = GetComponentInParent<AbilityScores>().mainStats.power;
            playerLuck = GetComponentInParent<AbilityScores>().mainStats.fate;
        }

        public int TryDoAttack()
        {
            int damageResult;

            if (CriticalStrike())
            {
                damageResult = (int)(damageType?.DoDamage(damage * 2 + playerStrength));
                Debug.Log(damageResult);
            }
            else
            {
                damageResult = (int)(damageType?.DoDamage(damage + playerStrength + extraDamageAbility));
                Debug.Log(damageResult);
            }

            //Reset extra damage from ability;
            extraDamageAbility = 0;

            return damageResult;
        }

        public void AbilityAttack(int extraDamage = 0)
        {
            extraDamageAbility = extraDamage;
        }

        public bool CriticalStrike()
        {
            int randomNumber = Random.Range(0, (100));
            if (randomNumber + playerLuck > 75)
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