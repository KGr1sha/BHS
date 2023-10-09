using System;

[Serializable]
public class InventoryItem
{
    public ItemData Data { get; private set; }
    public int StackSize { get; private set; }

    public InventoryItem(ItemData source, int amount)
    {
        Data = source;
        AddToStack(amount);
    }

    public void AddToStack(int amount)
    {
        StackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        StackSize -= amount;
    }
}
