using UnityEngine;

namespace SCPHalcyon
{
public class BlobWiggle : MonoBehaviour
{
 [SerializeField] Animator[] blobAnimator;
 [SerializeField] Transform blobParent;
 [SerializeField] HazardLevels hazardLevels;
 float blobAnimSpeed;
 float blobScale;
 
 void Update()
 {
  blobAnimSpeed = hazardLevels.hazardLevel *.1f;
  blobScale = hazardLevels.hazardLevel * .02f;

  foreach(Transform child in blobParent)
  {
   child.GetComponent<Animator>().speed = blobAnimSpeed;
   child.transform.localScale = new Vector3(blobScale,blobScale,blobScale);
  }

  if(hazardLevels.hazardLevel >= 49)
  {
 
   blobParent.transform.localScale = new Vector3(100,100,100);
   
  }
 }
}
}