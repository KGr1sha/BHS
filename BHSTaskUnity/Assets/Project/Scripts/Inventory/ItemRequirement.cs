using System;

[Serializable]
public struct ItemRequirement
{
    public ItemData ItemData;
    public int Amount;

    public bool HasRequirement()
    {
        InventoryItem item = InventorySystem.Instance.Get(ItemData);
        if (item != null && item.StackSize >= Amount) { return true; }
        return false;
    }
}
