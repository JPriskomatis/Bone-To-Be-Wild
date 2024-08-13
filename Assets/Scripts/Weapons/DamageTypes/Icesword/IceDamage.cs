using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSpace
{
    public class IceDamage : IDoDamage
    {
        public void DoDamage(int damage)
        {
            Debug.Log("Deal: " + damage);
            Debug.Log("Dealth ice damage");


        }

    }

}
