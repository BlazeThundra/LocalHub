using UnityEngine;

namespace SCPHalcyon
{
public class ArrowRC : MonoBehaviour
{
 [SerializeField] MinigameManager minigameManager;

 void OnTriggerEnter(Collider other)
 {
  if(other.gameObject.CompareTag("Goal"))
  {
   minigameManager.MinigameCompleted();
  }

  if(other.gameObject.CompareTag("Obstacle"))
  {
   minigameManager.MinigameFailed();
  }
 }
}
}