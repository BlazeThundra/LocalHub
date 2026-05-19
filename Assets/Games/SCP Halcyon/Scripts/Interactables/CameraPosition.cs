using UnityEngine;

namespace SCPHalcyon
{
public class CameraPosition : MonoBehaviour
{
 [SerializeField] Transform camPos;
 [SerializeField] RoomManager roomManager;
 [SerializeField] Transform target;
 [SerializeField] AudioClip wooshSound;
 [SerializeField] MinigameManager minigameManager;
 [SerializeField] string minigame;
 [SerializeField] HazardLevels hazardLevels;
 [SerializeField] Transform backCam;

 void OnMouseDown()
 {
  bool isSpecial = gameObject.CompareTag("StartPanel");

  if (!roomManager.moving && !roomManager.atPanel && (hazardLevels.started || isSpecial))
  {
   roomManager.atPanel = true;
   roomManager.SetBackReference(backCam);
   
   roomManager.EnablePanelColliders(false);
   
   if (GetComponent<AudioSource>() != null)
   {
    GetComponent<AudioSource>().PlayOneShot(wooshSound, 1);
   }

   if(!string.IsNullOrEmpty(minigame))
   {
    minigameManager.currentMinigame = minigame;
   }

   Vector3 direction = target.position - camPos.position;
   Quaternion endRot = Quaternion.LookRotation(direction);
   
   roomManager.StartMove(camPos.position, endRot);
  }
 }
}
}