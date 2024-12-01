using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySpace
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

        [System.Serializable]
        public enum Category { Weapon, Potion};
        public Category itemCategory;

        public void PerformAction()
        {
            switch (itemAction)
            {
                case Action.Equip:
                    Equip();
                    break;
                case Action.Activate:
                    Activate();
                    break;
                default:
                    Debug.LogError("Unknown Action");
                    break;
            }
        }

        protected virtual void Equip()
        {

        }
        protected virtual void Activate()
        {

        }
    }

}