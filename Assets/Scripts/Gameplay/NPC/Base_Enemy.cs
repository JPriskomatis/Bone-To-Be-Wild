using Damageables;
using System.Collections;
using UnityEngine;

namespace NPCspace
{
    public abstract class Base_Enemy : MonoBehaviour, ISpellDamageable, ISwordDamageable
    {
        public string npcName;
        public int health;
        public Animator anim;

        public float speed;

        public abstract void CloseToPlayer(GameObject player);
        public void SpellDamageable()
        {
            Debug.Log(transform.root.gameObject.name + " was hurt!");
            StartCoroutine(DelayedDeathAnima());
        }
        IEnumerator DelayedDeathAnima()
        {
            yield return new WaitForSeconds(1f);
            Death();
        }

        public void Death()
        {
            GetComponentInParent<AudioSource>().Play();
            anim.SetTrigger("death");
        }

        public void SwordDamageable()
        {
            Debug.Log("SwordDamageable");
            Death();
        }
    }

}
