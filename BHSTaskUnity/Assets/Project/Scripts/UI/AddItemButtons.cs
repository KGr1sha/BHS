using UnityEngine;

public class AddItemButtons : MonoBehaviour
{
    public void AddItem(ItemData item)
    {
        InventorySystem.Instance.Add(item, 10);
    }
}
