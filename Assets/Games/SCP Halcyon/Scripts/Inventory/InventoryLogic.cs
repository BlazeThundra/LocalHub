using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SCPHalcyon
{
public class InventoryLogic : MonoBehaviour
{
    public InventorySlot[] itemSlot;
    [SerializeField] GameObject inspectionImage;
    [SerializeField] GameObject inspectionText;
    Image inspectionImageComp;
    public string equippedType;
    public bool holdingCanister;
    public int equippedLevel;
    public int equippedSlotIndex;

    public bool isPickingUp = false;



    private void Start()
    {
        equippedType = "none";
        //inspectionImage = GameObject.Find("Inspection Image");
        //inspectionText = GameObject.Find("InspectionText");
        //inspectionText.GetComponent<TMP_Text>().text = " ";
        //inspectionImageComp = inspectionImage.GetComponent<Image>();
        //inspectionImageComp.enabled = false;

        // append all the inventory slots to the array wip
        for (int i = 0; i < transform.childCount; i++)
        {
            itemSlot[i].thisIndex = i;
            itemSlot[i] = transform.GetChild(i).GetComponent<InventorySlot>();
                
        }
    }

    public void AddObject(string itemName, Sprite itemSprite, Pickups itemData, Sprite inspectionSprite)
    {
        Debug.Log("pickd up something");
        for (int i = 0; i < itemSlot.Length; i++)
        {
            InventorySlot slot = transform.GetChild(i).GetComponent<InventorySlot>();
            if (!slot.isOccupied)
            {
                itemSlot[i].AddItem(itemName, itemSprite, itemData, inspectionSprite);
                return;
            }
        }
    }
    
    public void RemoveObject(string itemName)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            InventorySlot slot = transform.GetChild(i).GetComponent<InventorySlot>();
            if (slot.isOccupied && slot.itemName == itemName)
            {
                itemSlot[i].RemoveItem();
                return;
            }
        }
    }

    // to AARON - this  is what you call when you remove the equipped item, put equippedItemIndex in as parameter
    public void TargetedRemove(int itemIndex)
    {
        transform.GetChild(itemIndex).GetComponent<InventorySlot>().RemoveItem();
    }

    public void WaitForPickup()
    {
        StartCoroutine(WaitForPickupCoroutine());
    }

    IEnumerator WaitForPickupCoroutine()
    {
        isPickingUp = true;
        yield return new WaitForSeconds(0.5f);
        isPickingUp = false;
    }
}
}