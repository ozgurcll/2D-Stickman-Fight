using UnityEngine;

public enum ItemType
{
    Equipment,
    Potion
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemIcon;

    [Range(0, 100)]
    public float dropChance;
}
