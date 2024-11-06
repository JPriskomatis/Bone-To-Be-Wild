using PlayerSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace combat
{
    public class Player_Combat : MonoBehaviour, ICombat
    {
        [SerializeField] private Animator anim;

        public void TakeDamage(int damage)
        {
            anim.SetTrigger("gotHit");
            GetComponent<AbilityScores>().DecreaseStat(AbilityScores.StatType.CurrentHP, damage);
            Debug.Log("Took " + damage + " damage");
        }
    }
}