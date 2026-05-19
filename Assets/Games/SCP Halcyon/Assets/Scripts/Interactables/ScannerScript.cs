using System.Collections;
using UnityEngine;

namespace SCPHalcyon
{
public class ScannerScript : MonoBehaviour
{
    [SerializeField] Transform camPos;
    [SerializeField] Transform target;
    [SerializeField] HazardLevels hazardLevels;
    [SerializeField] RoomManager roomManager;

    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor; 

    [SerializeField] Transform backCam;

    [SerializeField] float openSpeed = .75f;
    [SerializeField] int levelRequirement;
    [SerializeField] AudioClip doorOpenSound;
    [SerializeField] AudioClip doorCloseSound;
    AudioSource audioSource;
    [SerializeField] Tasks tasks;

    [SerializeField] float transitionDuration = 1f;

    bool isOpening;

    void OnMouseDown()
    {
        if(roomManager.atPanel && !isOpening)
        {
            Debug.Log("clicked");
            if (levelRequirement == 0 || FindFirstObjectByType<InventoryLogic>().equippedLevel >= levelRequirement)
            {
                Debug.Log("door open");
                audioSource = GetComponent<AudioSource>();
                StartCoroutine(OpenDoor());
            }
        }
    }

    IEnumerator OpenDoor()
    {
        if(tasks != null){StartCoroutine(tasks.FirstTaskComplete());}

        isOpening = true;
        
        Vector3 originalLeftScale = leftDoor.transform.localScale;
        Vector3 originalRightScale = rightDoor.transform.localScale;

        Vector3 currentLeftScale = leftDoor.transform.localScale;
        Vector3 currentRightScale = rightDoor.transform.localScale;
        
        audioSource.PlayOneShot(doorOpenSound, 1);
        
        while (leftDoor.transform.localScale.x > 0.05f)
        {
            leftDoor.transform.localPosition += Vector3.left * openSpeed * Time.deltaTime;
            rightDoor.transform.localPosition += Vector3.right * openSpeed * Time.deltaTime;

            currentLeftScale.x = Mathf.Max(0f, currentLeftScale.x - (openSpeed * Time.deltaTime));
            leftDoor.transform.localScale = currentLeftScale;

            currentRightScale.x = Mathf.Max(0f, currentRightScale.x - (openSpeed * Time.deltaTime));
            rightDoor.transform.localScale = currentRightScale;

            yield return null;
        }

        StartCoroutine(CameraMovement());

        audioSource.PlayOneShot(doorCloseSound, 1);
        
        while (leftDoor.transform.localScale.x < originalLeftScale.x)
        {
            leftDoor.transform.localPosition += Vector3.right * openSpeed * Time.deltaTime;
            rightDoor.transform.localPosition += Vector3.left * openSpeed * Time.deltaTime;

            currentLeftScale.x = Mathf.Min(originalLeftScale.x, currentLeftScale.x + (openSpeed * Time.deltaTime));
            leftDoor.transform.localScale = currentLeftScale;

            currentRightScale.x = Mathf.Min(originalRightScale.x, currentRightScale.x + (openSpeed * Time.deltaTime));
            rightDoor.transform.localScale = currentRightScale;

            yield return null;
        }
        
        isOpening = false;
    }

    IEnumerator CameraMovement()
    {
        Transform camTransform = roomManager.cam.transform;

        Vector3 startPos = camTransform.position;
        Quaternion startRot = camTransform.rotation;

        Vector3 endPos = camPos.position;
        Vector3 directionToTarget = target.position - endPos;
        Quaternion endRot = Quaternion.LookRotation(directionToTarget);

        roomManager.SetBackReference(backCam);
        roomManager.atPanel = false;
        roomManager.EnablePanelColliders(true);

        float elapsedTime = 0f;

        while(elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;

            float percentage = elapsedTime / transitionDuration;
            float t = Mathf.SmoothStep(0f, 1f, percentage);

            camTransform.position = Vector3.Lerp(startPos, endPos, t);
            camTransform.rotation = Quaternion.Slerp(startRot, endRot, t);
          
            yield return null;
        }

        camTransform.position = endPos;
        camTransform.rotation = endRot;
    }
}
}