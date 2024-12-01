using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interaction;
using UI;

namespace InventorySpace
{
    public class PickUpItem : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Inventory.Instance.Add(GetComponent<ItemController>().item);
                //Inventory.Instance.ListItems();

                //Destroy the prefab;
                Destroy(this.gameObject);
            }
        }

        public void OnInteractEnter()
        {
            TextAppear.SetText("Pick up");
        }

        public void OnInteractExit()
        {
            TextAppear.RemoveText();
        }
    }

}