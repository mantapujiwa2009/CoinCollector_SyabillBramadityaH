using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class ItemSlots : MonoBehaviour
{

    public String itemName;
    public int itemQuantity;
    public Sprite itemIcon;
    public bool isFull;

    [SerializeField] public Image itemImage;
    [SerializeField] public TMP_Text quantityText;

    public void AddItem(ItemDetails item)
    {
        itemName = item.ItemName;
        itemQuantity = item.Quantity;
        itemIcon = item.ItemIcon;
        isFull = true;

        quantityText.enabled = true;
        quantityText.text = itemQuantity.ToString();
        itemImage.sprite = itemIcon;
    }
}
