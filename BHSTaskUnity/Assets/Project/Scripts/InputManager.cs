using UnityEngine;

public class InputManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            foreach (InventoryItem item in InventorySystem.Instance.InventoryItems)
            {
                Debug.Log($"{item.Data.Name}: {item.StackSize}");
            }
            
        }
    }
}
