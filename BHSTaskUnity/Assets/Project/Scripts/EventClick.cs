using UnityEngine;
using UnityEngine.EventSystems;

public class EventClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject craftingMenu;

    public void OnPointerClick(PointerEventData eventData)
    {
        craftingMenu.SetActive(true);
    }
}
