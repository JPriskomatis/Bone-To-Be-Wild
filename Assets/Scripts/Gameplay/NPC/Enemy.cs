using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCspace
{
    public abstract class Enemy : MonoBehaviour
    {
        [Header("Goblin Stats")]
        public int maxHealth;
        public int currentHealth;
    }
}

