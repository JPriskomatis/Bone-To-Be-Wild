using UnityEngine;
using UnityEngine.UI;

namespace InventorySpace
{
    public class EquipItemManager : Singleton<EquipItemManager>
    {
        [SerializeField] private Image weaponHolder;
        GameObject previousWeapon;
        public void EquipItem(ItemSO item)
        {
            if(previousWeapon != null)
            {
                previousWeapon.SetActive(true);
            }
            weaponHolder.sprite = item.icon;
            GameObject itemToDeactivate = Inventory.Instance.FindGameObjectByItemSO(item);
            itemToDeactivate.SetActive(false);
            previousWeapon = itemToDeactivate;

        }
    }

}