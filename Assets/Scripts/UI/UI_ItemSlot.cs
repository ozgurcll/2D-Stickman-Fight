using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    public Image itemDuration;
    public InventoryItem item;
    public ItemData_Equipment equipmentData;

    public void UpdateSlot(InventoryItem _newItem)
    {
        item = _newItem;
        equipmentData = item.data as ItemData_Equipment;
        if (item != null)
        {
            itemImage.sprite = item.data.itemIcon;
            itemImage.color = Color.white;
            if (equipmentData.equipmentType == EquipmentType.Weapon)
            {
                itemDuration.fillAmount = 1;
            }
        }
    }

    public void CleanUpSlot()
    {
        item = null;

        itemImage.sprite = null;
        itemImage.color = Color.clear;
    }
}
