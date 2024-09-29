using Damageables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace NPCspace.goblin
{
    public class Goblin_Combat : Goblin, ISwordDamageable
    {
        private bool isShooting;

        public void StartShooting()
        {
            if (!isShooting)
            {
                isShooting = true;
                lookAtPlayer = false;
                this.GetComponent<Animator>().SetTrigger("shoot");
                StartCoroutine(RotateGoblin());
            }
        }

        public void ShootArrow()
        {
            //spawn the arrow prefab;
            GameObject arrow = Instantiate(p_arrow, new Vector3(bow.position.x, bow.position.y, bow.position.z), bow.rotation);
            arrow.GetComponent<GoblinArrow>().ShootProjectile(enemy);
        }

        public void LookAtPlayerAgain()
        {
            lookAtPlayer = true;
            isShooting = false;
        }

        public void SwordDamageable()
        {
            isShooting = false;
            Debug.Log("Hit");

            anim.SetTrigger("takeDamage");
            LookAtPlayerAgain();
            

            
        }
    }

}
