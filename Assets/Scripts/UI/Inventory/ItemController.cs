using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class ItemController : MonoBehaviour
    {
        public ItemSO item;

        public void ClickItem()
        {
            if(item.itemAction == ItemSO.Action.Equip)
            {
                Debug.Log("Equip!");
            }
            else
            {
                Debug.Log("Activate!");
            }
        }
    }

}