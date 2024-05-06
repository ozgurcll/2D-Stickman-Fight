using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<InventoryItem> equipment;
    public Dictionary<ItemData_Equipment, InventoryItem> equipmentDictionary;

   

    [Header("Inventory UI")]
    [SerializeField] private Transform inventorySlotParent;
    public UI_EquipmentSlot[] itemSlots;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        equipment = new List<InventoryItem>();
        equipmentDictionary = new Dictionary<ItemData_Equipment, InventoryItem>();

        itemSlots = inventorySlotParent.GetComponentsInChildren<UI_EquipmentSlot>();
    }

    public void EquipItem(ItemData _item)
    {
        ItemData_Equipment newEquipment = _item as ItemData_Equipment;
        InventoryItem newItem = new InventoryItem(_item);

        ItemData_Equipment oldEquipment = null;

        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
        {
            if (item.Key.equipmentType == newEquipment.equipmentType)
                oldEquipment = item.Key;
        }

        if (oldEquipment != null)
        {
            UnequipItem(oldEquipment);
        }
        equipment.Add(newItem);
        equipmentDictionary.Add(newEquipment, newItem);
        newEquipment.AddModifier();

        UpdateSlotUI();
    }

    private void UnequipItem(ItemData_Equipment _itemToDelete)
    {
        if (equipmentDictionary.TryGetValue(_itemToDelete, out InventoryItem value))
        {
            equipment.Remove(value);
            equipmentDictionary.Remove(_itemToDelete);
            _itemToDelete.RemoveModifier();
        }
    }

    private void UpdateSlotUI()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
            {
                if (item.Key.equipmentType == itemSlots[i].slotType)
                    itemSlots[i].UpdateSlot(item.Value);
            }
        }
    }

    public void SwordDuration()
    {
        itemSlots[0].itemDuration.fillAmount -= 1 * 0.09f;

        if (itemSlots[0].itemDuration.fillAmount <= 0)
        {
            UnequipItem(equipment[0].data as ItemData_Equipment);
            itemSlots[0].CleanUpSlot();
        }
    }

    public void AmmoDuration()
    {
        itemSlots[0].itemDuration.fillAmount -= 1 * 0.113f;

        if (itemSlots[0].itemDuration.fillAmount <= 0)
        {
            UnequipItem(equipment[0].data as ItemData_Equipment);
            itemSlots[0].CleanUpSlot();
        }
    }
}
