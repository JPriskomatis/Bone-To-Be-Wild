using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSpace
{
    public class NormalSword : Weapon_base {
        public NormalSword()
        {
            damage = 10;
            damageType = new NormalDamage();
        }
    }

}