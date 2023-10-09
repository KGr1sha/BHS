using System;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;
    public static event Action OnInventoryChanged;

    private Dictionary<ItemData, InventoryItem> itemDictionary;
    public List<InventoryItem> InventoryItems { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InventoryItems = new List<InventoryItem>();
            itemDictionary = new Dictionary<ItemData, InventoryItem>();
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public InventoryItem Get(ItemData referenceData)
    {
        if (itemDictionary.TryGetValue(referenceData, out InventoryItem item)) return item;
        return null;
    }

    public void Add(ItemData referenceData, int amount)
    {
        if (itemDictionary.TryGetValue(referenceData, out InventoryItem item))
        {
            item.AddToStack(amount);
        }
        else
        {
            InventoryItem newItem = new InventoryItem(referenceData, amount);
            InventoryItems.Add(newItem);
            itemDictionary.Add(referenceData, newItem);
        }
        OnInventoryChanged?.Invoke();
    }

    public void Remove(ItemData referenceData, int amount)
    {
        if (itemDictionary.TryGetValue(referenceData, out InventoryItem item))
        {
            item.RemoveFromStack(amount);

            if (item.StackSize <= 0)
            {
                InventoryItems.Remove(item);
                itemDictionary.Remove(referenceData);
            }
        }
        OnInventoryChanged?.Invoke();
    }
}
