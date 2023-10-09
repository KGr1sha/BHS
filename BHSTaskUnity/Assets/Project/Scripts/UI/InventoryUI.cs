using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;

    private void OnEnable()
    {
        InventorySystem.OnInventoryChanged += OnUpdateInventory;
    }

    private void OnDisable()
    {
        InventorySystem.OnInventoryChanged -= OnUpdateInventory;
    }

    private void OnUpdateInventory()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        DrawInventory();
    }

    private void DrawInventory()
    {
        foreach(InventoryItem item in InventorySystem.Instance.InventoryItems)
        {
            AddInventorySlot(item);
        }
    }

    private void AddInventorySlot(InventoryItem item)
    {
        GameObject obj = Instantiate(slotPrefab);
        obj.transform.SetParent(transform, false);

        InventorySlot slot = obj.GetComponent<InventorySlot>();
        slot.Set(item);
    }
}
