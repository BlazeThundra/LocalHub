using UnityEngine;

namespace SCPHalcyon
{
public class DragBox : MonoBehaviour
{
 private Vector3 offset;
 private float zCoord;
 [SerializeField] MinigameManager minigameManager;
 bool isDragging;
 int points = 0;

 void OnMouseDown()
 {
  isDragging = true;
  zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
  offset = gameObject.transform.position - GetMouseAsWorldPoint();
 }

 void OnMouseDrag()
 {
  if(!minigameManager.dragOnCD && isDragging && minigameManager.currentMinigame == "Drag")
  {
   transform.position = GetMouseAsWorldPoint() + offset;
  }
 }

 private Vector3 GetMouseAsWorldPoint()
 {
  Vector3 mousePoint = Input.mousePosition;
  mousePoint.z = zCoord;
  return Camera.main.ScreenToWorldPoint(mousePoint);
 }

 void OnTriggerEnter(Collider other)
 {
  if(other.gameObject.CompareTag("Goal"))
  {
   other.gameObject.SetActive(false);
   points ++;

   if(points == 3)
   {
    minigameManager.MinigameCompleted();
   }
  }

  if(other.gameObject.CompareTag("Obstacle"))
  {
   isDragging = false;
   minigameManager.MinigameFailed();
  }
 }
}
}