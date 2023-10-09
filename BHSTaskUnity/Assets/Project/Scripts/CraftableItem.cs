using System.Collections.Generic;
using UnityEngine;

public class CraftableItem : MonoBehaviour
{
    public ItemData Data;

    public List<ItemRequirement> Requirements;
    [SerializeField] public int amountToCraft;


    public bool HasRequirements()
    {
        foreach (var req in Requirements)
        {
            if (req.HasRequirement() == false)
            {
                return false;
            }
        }
        return true;
    }
}
