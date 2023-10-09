using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RequiredItem : MonoBehaviour
{
    [SerializeField] private Image iconField;
    [SerializeField] private TextMeshProUGUI quantity;

    public void SetItem(ItemData itemType, int amount)
    {
        iconField.sprite = itemType.Icon;
        quantity.text = amount.ToString();

        //Set text color
        InventoryItem item = InventorySystem.Instance.Get(itemType);
        if (item != null && item.StackSize >= amount)
        {
            quantity.color = Color.green;
        }
        else
        {
            quantity.color = Color.red;
        }
        
    }
}
