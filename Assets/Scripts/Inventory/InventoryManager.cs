
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour, GrabableItem
{

    [SerializeField] private GameObject inventoryMenu;
    InputAction inventoryAction;
    bool isOpen;

    [Tooltip("ItemSlot")]
    [SerializeField] private int slotCount;
    private ItemSlots[] itemSlots;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryAction = InputSystem.actions.FindAction("Inventory");
        inventoryMenu.SetActive(false);
        isOpen = false;

        CreateSlot();
    }

    // Update is called once per frame
    void Update()
    {
        if (inventoryAction.triggered && isOpen == false)
        {
            Debug.Log("Open inventory");
            inventoryMenu.SetActive(true);
            Time.timeScale = 0;
            isOpen = true;
        } else if (inventoryAction.triggered && isOpen == true){
            Debug.Log("Close inventory");
            inventoryMenu.SetActive(false);
            Time.timeScale = 1;
            isOpen = false; 
        }
    }

    public void AddItem(ItemDetails item)
    {
        Debug.Log("Item added to inventory: " + item.ItemName + " Quantity: " + item.Quantity); 

        if (itemSlots == null || itemSlots.Length == 0)
        {
            Debug.LogWarning("Inventory has no slots, '" + item.ItemName + "' was lost." );
            return;
        }

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (!itemSlots[i].isFull)
            {
                itemSlots[i].AddItem(item);
                return;
            }
        }       
    }

    private void CreateSlot()
    {
        ItemSlots slotPrefab = Resources.Load<ItemSlots>("ItemSlot");
        if (slotPrefab == null)
        {
            Debug.LogError("ItemSlot prefab was not found");
            itemSlots = new ItemSlots[0];
            return;
        }
        Transform parent = inventoryMenu.transform;
        GridLayoutGroup grid = inventoryMenu.GetComponentInChildren<GridLayoutGroup>(true);
        if (grid != null)
        {
            parent = grid.transform;
        }

        itemSlots = new ItemSlots[slotCount];
        for (int i = 0; 1 < slotCount; i++)
        {
            itemSlots[i] = Instantiate(slotPrefab, parent);
        }
    }
}
