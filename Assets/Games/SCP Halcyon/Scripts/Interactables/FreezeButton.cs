using UnityEngine;

namespace SCPHalcyon
{
public class FreezeButton : MonoBehaviour
{
 [SerializeField] HazardLevels hazardLevels;
 [SerializeField] RoomManager roomManager;
 [SerializeField] Gas gasObject;
 
 void OnMouseDown()
 {
  if(roomManager.atPanel && gasObject.fill == 1f && !gasObject.filling)
  {
   gasObject.StartEmptying();
   hazardLevels.frozen = true;
   transform.localScale = new Vector3(.3f,0.15f,.3f);
  }
 }

 public void UnFreeze()
 {
  hazardLevels.frozen = false;
  transform.localScale = new Vector3(.3f,.3f,.3f);
 }
}
}