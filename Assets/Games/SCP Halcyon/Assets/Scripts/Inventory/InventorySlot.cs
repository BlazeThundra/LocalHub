using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SCPHalcyon
{
public class InventorySlot : MonoBehaviour
{
    public string itemName;
    public Sprite itemSprite;
    public bool isOccupied;
    public Pickups thisItemData;
    public bool isInspecting = false;
    public int thisIndex;
    public Sprite inspectionSprite;
    [SerializeField] GameObject inspectionImage;
    [SerializeField] GameObject inspectionText;
    [SerializeField] GameObject mouseSprite;
    Image mouseSpriteComp;
    Image inspectionImageComp;

    InventoryLogic inventoryLogic;


    [SerializeField] private Image Image;
    [SerializeField] private TMP_Text Name;

    void Start()
    {
        inventoryLogic = FindFirstObjectByType<InventoryLogic>();
        inspectionImage = GameObject.Find("Inspection Image");
        inspectionText = GameObject.Find("InspectionText");
        inspectionText.GetComponent<TMP_Text>().text = " ";
        inspectionImageComp = inspectionImage.GetComponent<Image>();
        inspectionImageComp.enabled = false;
        isOccupied = false;
        Image.sprite = null;
        Name.text = "";
    }

    public void AddItem(string itemNamePara, Sprite itemSpritePara, Pickups itemDataPara, Sprite inspectionSpritePara)
    {
        isOccupied = true;
        Image.sprite = itemSpritePara;
        Name.text = itemNamePara;
        thisItemData = itemDataPara;
        itemName = thisItemData.pickupName;
        itemSprite = thisItemData.inventoryIcon;
        inspectionImageComp.sprite = inspectionSpritePara;
        inventoryLogic.isPickingUp = false;
    }

    public void RemoveItem()
    {
        isOccupied = false;
        Image.sprite = null;
        Name.text = "";
        thisItemData = null;
        itemName = null;
        itemSprite = null;
        inspectionImageComp.sprite = null;
    }

    //public void Inspection()
    //{
    //    if (isOccupied)
    //    {
    //        RectTransform rt = inspectionImageComp.rectTransform;
    //        Debug.Log("clicked");

    //        if (!isInspecting)
    //        {
    //            inspectionImageComp.sprite = thisItemData.inspectionImage;
    //            inspectionImageComp.enabled = true;
    //            inspectionImage.GetComponentInChildren<TMP_Text>().text = thisItemData.description;
    //            isInspecting = true;

    //        }
    //        else
    //        {
    //            inspectionImageComp.enabled = false;
    //            inspectionImage.GetComponentInChildren<TMP_Text>().text = " ";
    //            isInspecting = false;
    //        }
    //    }
    //}

    public void Equip()
    {
        if (isOccupied)
        {
            mouseSprite = GameObject.Find("MouseSprite");
            mouseSpriteComp = mouseSprite.GetComponent<Image>();
            if (thisItemData.usageText != null && inventoryLogic.equippedType == "none")
            {
                inventoryLogic.equippedSlotIndex = thisIndex;

                mouseSpriteComp.enabled = true;
                mouseSpriteComp.sprite = thisItemData.inventoryIcon;
                Debug.Log("equipped " + itemName);
                inventoryLogic.equippedType = thisItemData.usageText;
                inventoryLogic.equippedLevel = thisItemData.usageLevel;
                if(thisItemData.usageText == "canister"){inventoryLogic.holdingCanister = true;} else{inventoryLogic.holdingCanister = false;}
            }
            else
            {
                inventoryLogic.equippedSlotIndex = -1;
                mouseSpriteComp.enabled = false;
                Debug.Log("unequipped");
                inventoryLogic.equippedType = "none";
                inventoryLogic.equippedLevel = 0;
            }
        }
    }

    public void InspectIn()
    {
        if (isOccupied)
        {
            RectTransform rt = inspectionImageComp.rectTransform;
            Debug.Log("clicked");

            //if (!isInspecting)
            //{
            inspectionImageComp.sprite = thisItemData.inspectionImage;
            float width = inspectionImageComp.sprite.rect.width * .08f;
            float height = inspectionImageComp.sprite.rect.height * .08f;
            rt.sizeDelta = new Vector2(width, height);
            inspectionImageComp.enabled = true;
            inspectionImage.GetComponentInChildren<TMP_Text>().text = thisItemData.description;
            isInspecting = true;

            //}
            //else
            //{
            //    inspectionImageComp.enabled = false;
            //    inspectionImage.GetComponentInChildren<TMP_Text>().text = " ";
            //    isInspecting = false;
            //}
        }
    }

    public void InspectOut()
    {
        inspectionImageComp.enabled = false;
        inspectionImage.GetComponentInChildren<TMP_Text>().text = " ";
        isInspecting = false;
    }

}
}