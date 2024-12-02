using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySpace
{

    public class ItemController : MonoBehaviour
    {
        public ItemSO item;


        public void ClickItem()
        {
            item.PerformAction();
        }

    }

}