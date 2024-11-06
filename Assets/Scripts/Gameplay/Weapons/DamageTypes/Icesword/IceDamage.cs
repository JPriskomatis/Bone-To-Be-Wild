using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using System.Threading.Tasks;

namespace WeaponSpace
{
    public class IceDamage : IDoDamage
    {
        public GameObject particlePrefab;
        public Transform swordTransform;
        public int DoDamage(int damage)
        {
            IceParticle();

            return damage;
        }
        public async void IceParticle()
        {
            await DelayAsync(500);
            if (particlePrefab != null)
            {
                GameObject particles = Object.Instantiate(particlePrefab, swordTransform.position, Quaternion.identity);

                Object.Destroy(particles, 1.0f); // Adjust duration as needed
            }
        }
        private async Task DelayAsync(int milliseconds)
        {
            await Task.Delay(milliseconds);
        }


    }

}
