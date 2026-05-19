using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace SCPHalcyon
{
public class ArrowButton : MonoBehaviour
{
 [SerializeField] MinigameManager minigameManager;
 [SerializeField] Vector3 moveDir;
 [SerializeField] Rigidbody targetRb;
 public bool pressed;
 
 void OnMouseDown()
 {
  transform.localScale = new Vector3(1f, 0.02f, 1f);
  pressed = true;
 }

 void OnMouseUp()
 {
  transform.localScale = new Vector3(1f, 0.1f, 1f);
  pressed = false;
 }

 void FixedUpdate()
 {
  if(pressed && !minigameManager.arrowOnCD)
  {
   Vector3 worldDirection = transform.TransformDirection(moveDir.normalized);
   float speed = .005f;
   Vector3 movement = worldDirection * speed;
   targetRb.MovePosition(targetRb.position + movement);
  }
 }
}
}