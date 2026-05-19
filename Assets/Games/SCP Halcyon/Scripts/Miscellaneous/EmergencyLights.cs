using UnityEngine;

namespace SCPHalcyon
{
public class EmergencyLights : MonoBehaviour
{
 public int spinSpeed = 360;

 void Update()
 {
  transform.Rotate(Vector3.up * Time.deltaTime * spinSpeed);
 }
}
}