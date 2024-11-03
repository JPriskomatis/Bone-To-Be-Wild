using UnityEngine;

namespace GameToUI
{

    public class Enemy_Level : MonoBehaviour
    {
        public enum PowerLevel { Weak, Average, Strong, Legendary };
        [SerializeField] public PowerLevel level;

    }

}