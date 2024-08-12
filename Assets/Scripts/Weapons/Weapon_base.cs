using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSpace
{
    public class Weapon_base : MonoBehaviour
    {
        public int damage = 0;
        public IDoDamage damageType;


        public void TryDoAttack()
        {
            damageType?.DoDamage(damage);
        }

        //To use this function do the following:
        //
        public void SetDamageType(IDoDamage weaponType)
        {
            this.damageType = weaponType;
        }
    }

}