using UnityEngine;
using System.Collections;

namespace SCPHalcyon
{
public class Gas : MonoBehaviour
{
 public bool filling = false;
 public float fill = 0f;
 [SerializeField] Transform parent;
 [SerializeField] FreezeButton freezeButton;

 public void StartFilling()
 {
  if(!filling)
  {
   StartCoroutine(FillRoutine());
  }
 }

 public void StartEmptying()
 {
  if(!filling)
  {
   StartCoroutine(EmptyRoutine());
  }
 }

 IEnumerator FillRoutine()
 {
  filling = true;

  while(fill <= 1)
  {
   fill += .01f;
   parent.transform.localScale = new Vector3(.9f,fill,.9f);
   yield return null;
  }

  fill = 1f;
  filling = false;
 }

 IEnumerator EmptyRoutine()
 {
  filling = true;

  while(fill >= 0)
  {
   fill -= .0005f;
   parent.transform.localScale = new Vector3(.9f, fill, .9f);
   yield return null;
  }

  fill = 0f;
  filling = false;
  freezeButton.UnFreeze();
 }
}
}