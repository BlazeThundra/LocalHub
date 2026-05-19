using UnityEngine;

namespace SCPHalcyon
{
public class InvObjects : MonoBehaviour
{
    [SerializeField] AudioClip pickupSound;
    [SerializeField] Pickups itemData;
    AudioSource audiosource;

    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }   
    void OnMouseDown()
    {
        InventoryLogic inventoryLogic = FindFirstObjectByType<InventoryLogic>();
        if (inventoryLogic.isPickingUp)
        {
            return;
        }
        inventoryLogic.WaitForPickup();

        audiosource.PlayOneShot(pickupSound, 1);
        this.GetComponent<BoxCollider>().enabled = false;
        Debug.Log("pickd up something");
        inventoryLogic.AddObject(itemData.pickupName, itemData.inventoryIcon, itemData, itemData.inspectionImage);
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<InvObjects>().enabled = false;
        this.enabled = false;
    }

}
}