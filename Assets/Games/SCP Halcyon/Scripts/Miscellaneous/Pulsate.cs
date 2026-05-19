using UnityEngine;

namespace SCPHalcyon
{
public class Pulsate : MonoBehaviour
{
 [SerializeField] HazardLevels hazardLevels;

 Vector3 startScale;

 void Awake()
 {
  startScale = transform.localScale;
 }

 void LateUpdate()
 {
  if(!hazardLevels.frozen)
  {
   float scaleFactor = 1 + (hazardLevels.hazardLevel * .01f);
   transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
  }
 }
}
}