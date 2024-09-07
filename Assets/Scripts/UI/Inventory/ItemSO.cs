using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName ="New Item", menuName ="Items/Create New Item")]
    public class ItemSO : ScriptableObject
    {
        public int id;
        public string itemName;
        public string itemDescription;
        public int value;
        public Sprite icon;


    }

}