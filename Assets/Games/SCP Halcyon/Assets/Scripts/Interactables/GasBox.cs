using UnityEngine;

namespace SCPHalcyon
{
public class GasBox : MonoBehaviour
{
 [SerializeField] Gas gasObject;
 [SerializeField] InventoryLogic inventoryLogic;

 void OnMouseDown()
 {
  if(inventoryLogic.holdingCanister && !gasObject.filling)
  {
   //inventoryLogic.RemoveObject();
   gasObject.StartFilling();
  }
 }
}
}