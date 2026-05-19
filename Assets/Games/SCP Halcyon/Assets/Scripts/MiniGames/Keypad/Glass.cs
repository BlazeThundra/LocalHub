using UnityEngine;

namespace SCPHalcyon
{
public class Glass : MonoBehaviour
{
 [SerializeField] Transform start;
 [SerializeField] Transform end;

 public void OpenGlass()
 {
  transform.position = end.position;
  transform.rotation = end.rotation;
 }
}
}