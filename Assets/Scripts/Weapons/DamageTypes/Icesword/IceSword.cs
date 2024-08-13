using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSpace
{
    public class IceSword : Weapon_base
    {
        [SerializeField] private GameObject particlePrefab;

        public IceSword()
        {
            damage = 15;
            damageType = new IceDamage();
        }

        private void Start()
        {
            if (damageType is IceDamage iceDamage)
            {
                iceDamage.particlePrefab = particlePrefab;
                iceDamage.swordTransform = transform;

            }
        }

    }

}
