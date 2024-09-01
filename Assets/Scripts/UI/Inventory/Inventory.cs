using gameStateSpace;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace UI
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory Instance;

        public GameObject inventory;

        public List<ItemSO> Items = new List<ItemSO>();

        public Transform itemContent;
        public GameObject InventoryItem;

        private void Awake()
        {
            Instance = this;
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                GameStatController.Instance.SetState(GameStatController.CurrentGameState.Paused);

               
                inventory.SetActive(true);

            }
        }

        //Adds an Item to our list of items;
        public void Add(ItemSO item)
        {
            Items.Add(item);
        }

        public void ListItems()
        {
            foreach(ItemSO item in Items)
            {
                //We create a new gameobject in the UI, we create the item prefab;
                GameObject obj = Instantiate(InventoryItem, itemContent);

                //We find the ItemName gameobject from the item prefab that we just spawned;
                var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();

                //similar to above;
                var itemIcon = obj.transform.Find("ItemImage").GetComponent<Image>();

                //We pass the scriptable object's values to the UI;
                itemName.text = item.itemName;
                itemIcon.sprite = item.icon;
            }
        }

    }

}