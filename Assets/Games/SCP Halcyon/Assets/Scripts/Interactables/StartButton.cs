using UnityEngine;

namespace SCPHalcyon
{
public class StartButton : MonoBehaviour
{
 [SerializeField] HazardLevels hazardLevels;
 [SerializeField] RoomManager roomManager;
    [SerializeField] AudioClip interactSound;

    void OnMouseDown()
 {
  if(roomManager.atPanel)
  {
    GetComponent<AudioSource>().PlayOneShot(interactSound, 1);
            hazardLevels.started = true;
   transform.localScale = new Vector3(.45f,.15f,.45f);
  }
 }
}
}