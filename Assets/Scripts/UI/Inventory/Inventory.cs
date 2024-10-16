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

        [SerializeField] private GameObject renderTextureCam;

        private void Awake()
        {
            Instance = this;
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.I))
            {
                GameStatController.Instance.SetState(GameStatController.CurrentGameState.Paused);

                renderTextureCam.SetActive(true);
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

                //similar to above;
                var itemIcon = obj.transform.Find("ItemImage").GetComponent<Image>();

                var itemDescriptionBackground = obj.transform.Find("ItemDescriptionBackground");

                var itemName = itemDescriptionBackground.gameObject.transform.Find("ItemDescriptionTitle").GetComponent<TMP_Text>();
                var itemDescription = itemDescriptionBackground.gameObject.transform.Find("ItemDescription").GetComponent<TMP_Text>();

                //We pass the scriptable object's values to the UI;
                itemName.text = item.itemName;
                itemIcon.sprite = item.icon;
                itemDescription.text = item.itemDescription;
            }
        }

    }

}