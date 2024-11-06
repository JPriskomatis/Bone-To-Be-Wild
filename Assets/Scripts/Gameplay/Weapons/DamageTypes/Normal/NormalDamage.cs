using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSpace
{
    public class NormalDamage : IDoDamage
    {
        public int DoDamage(int damage)
        {
            return damage;
        }
    }

}
