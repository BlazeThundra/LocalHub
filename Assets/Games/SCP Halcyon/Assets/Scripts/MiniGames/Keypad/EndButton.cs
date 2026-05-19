using UnityEngine;

namespace SCPHalcyon
{
public class EndButton : MonoBehaviour
{
 [SerializeField] Tasks tasks;

 void OnMouseDown()
 {
  transform.localScale = new Vector3(.3f,.05f,.3f);
  tasks.ButtonTaskComplete();
 }
}
}