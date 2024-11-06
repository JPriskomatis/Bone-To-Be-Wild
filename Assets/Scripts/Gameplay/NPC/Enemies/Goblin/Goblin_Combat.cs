using Damageables;
using System;
using UnityEngine;
using UI;
using PlayerSpace;


namespace NPCspace.goblin
{
    public class Goblin_Combat : Goblin, ISwordDamageable
    {
        public static event Action OnDamaged;

        private bool isShooting;

        private void Start()
        {
            enemy_ui.SetUI(this.gameObject.name, icon, "(" + level.ToString() + ")");
            enemy = Player_Manager.instance.gameObject;
            Debug.Log(level);
            enemy_ui.SetUI(this.gameObject.name, icon, "(" + level.ToString() + ")");
        }


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

        public void SwordDamageable(int damage)
        {
            
            isShooting = false;
            Debug.Log("Hit");

            anim.SetTrigger("takeDamage");
            LookAtPlayerAgain();

            currentHealth -= 5;
            enemy_ui.UpdateSlider(currentHealth, maxHealth);
        }

        public override void PerformAction()
        {
            StartShooting();
        }

    }

}
