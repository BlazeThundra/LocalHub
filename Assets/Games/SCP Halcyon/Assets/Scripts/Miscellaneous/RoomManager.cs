using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace SCPHalcyon
{
 public class RoomManager : MonoBehaviour
 {
  [SerializeField] public Camera cam;
  [SerializeField] HazardLevels hazardLevels;
  private Transform activeBackTransform;
  [SerializeField] List<Collider> panelColliders = new List<Collider>();
 
  public bool atPanel;
  public bool moving;
  [SerializeField] float transitionDuration = 1f;

  public void SetBackReference(Transform newBack)
  {
   activeBackTransform = newBack;
  }
 
  void Update()
  {
   if(Input.GetMouseButtonDown(1) && !moving && atPanel && activeBackTransform != null && !hazardLevels.won)
   {
    atPanel = false;
    EnablePanelColliders(true);
    StartCoroutine(CameraMovement(activeBackTransform.position, activeBackTransform.rotation));
   }
  }

  public void EnablePanelColliders(bool state)
  {
   foreach(Collider col in panelColliders)
   {
    if(col != null) col.enabled = state;
   }
  }

  public void StartMove(Vector3 targetPos, Quaternion targetRot)
  {
   if (!moving)
   {
    StartCoroutine(CameraMovement(targetPos, targetRot));
   }
  }

  IEnumerator CameraMovement(Vector3 endPos, Quaternion endRot)
  {
   moving = true;
   Transform camTransform = cam.transform;
   Vector3 startPos = camTransform.position;
   Quaternion startRot = camTransform.rotation;
   float elapsedTime = 0f;

  while(elapsedTime < transitionDuration)
  {
    elapsedTime += Time.deltaTime;
    float t = Mathf.SmoothStep(0f, 1f, elapsedTime / transitionDuration);

    camTransform.position = Vector3.Lerp(startPos, endPos, t);
    camTransform.rotation = Quaternion.Slerp(startRot, endRot, t);
    yield return null;
   } 

   camTransform.position = endPos;
   camTransform.rotation = endRot;
   moving = false;
  }
 }
}