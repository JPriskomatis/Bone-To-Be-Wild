using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Damageables;
using Audio;

public class TEST_NPC : MonoBehaviour, ISpellDamageable, ISwordDamageable
{
    [SerializeField] Animator anim;
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
