using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Text number;

    public void Set(InventoryItem item)
    {
        icon.sprite = item.Data.Icon;
        number.text = item.StackSize.ToString();
    }
}
