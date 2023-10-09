using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Craft : MonoBehaviour
{
    private CraftableItem selectedItem;

    [Header("References")]
    [SerializeField] private Button craftButton;
    [SerializeField] private TextMeshProUGUI itemNameField;
    [SerializeField] private TextMeshProUGUI descriptionField;
    [SerializeField] private Image iconField;
    [SerializeField] private Slider progressBar;

    [SerializeField] private Transform requiremetnsList;
    [SerializeField] private GameObject requiredItemPrefab;

    private bool isCrafting;

    public void TryCraft()
    {
        if (selectedItem.HasRequirements() == false || isCrafting == true) { return; }

        StartCoroutine(StartCrafting(1));
    }

    public void StopCrafting()
    {
        StopAllCoroutines();
        progressBar.value = 0;
        isCrafting = false;
    }


    public void SelectItem(CraftableItem item)
    {

        itemNameField.text = item.Data.Name;
        descriptionField.text = item.Data.Description;
        iconField.sprite = item.Data.Icon;
        selectedItem = item;

        UpdateRequirementsUI();
    }


    private IEnumerator StartCrafting(float craftingTime)
    {
        isCrafting = true;
        float progress = 0f;
        while (progress < craftingTime)
        {
            progress += 0.1f;
            progressBar.value = progress;
            yield return new WaitForSeconds(craftingTime / 10);
        }
        InventorySystem.Instance.Add(selectedItem.Data, selectedItem.amountToCraft);
        RemoveItems();

        progressBar.value = 0;
        isCrafting = false;

        UpdateRequirementsUI();
        TryCraft();
    }

    private void RemoveItems()
    {
        foreach (var req in selectedItem.Requirements)
        {
            InventorySystem.Instance.Remove(req.ItemData, req.Amount);
        }
    }

    private void UpdateRequirementsUI()
    {
        if (selectedItem == null) return;

        ClearRequirements();
        foreach (var req in selectedItem.Requirements)
        {
            AddRequirementToUI(req);
        }
    }

    private void AddRequirementToUI(ItemRequirement requirement)
    {
        GameObject newRequirement = Instantiate(requiredItemPrefab);
        newRequirement.GetComponent<RequiredItem>().SetItem(requirement.ItemData, requirement.Amount);
        newRequirement.transform.SetParent(requiremetnsList, false);
    }

    private void ClearRequirements()
    {
        foreach (Transform req in requiremetnsList.transform)
        {
            Destroy(req.gameObject);
        }
    }

    private void OnEnable()
    {
        InventorySystem.OnInventoryChanged += UpdateRequirementsUI;
    }
    private void OnDisable()
    {
        InventorySystem.OnInventoryChanged -= UpdateRequirementsUI;
    }
}
