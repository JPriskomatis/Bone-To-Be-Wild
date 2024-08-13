using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSpace
{
    public class IceSword : Weapon_base
    {
        public IceSword()
        {
            damage = 15;
            damageType = new IceDamage();
        }
    }

}
