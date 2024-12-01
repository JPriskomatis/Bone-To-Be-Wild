using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName ="New Item", menuName ="Items/Create New Item")]
    public class ItemSO : ScriptableObject
    {
        [Header("Item Values")]
        public string itemName;
        public string itemDescription;
        public Sprite icon;

        [System.Serializable]
        public enum Action { Equip, Activate };
        public Action itemAction;
    }

}