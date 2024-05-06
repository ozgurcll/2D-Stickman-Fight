using System;

[Serializable]
public class InventoryItem
{
    public ItemData data;
    public InventoryItem(ItemData _newItemData)
    {
        data = _newItemData;
    }
}
