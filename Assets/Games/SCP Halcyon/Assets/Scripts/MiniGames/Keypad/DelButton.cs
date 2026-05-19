using UnityEngine;
using TMPro;

namespace SCPHalcyon
{
public class DelButton : MonoBehaviour
{
 [SerializeField] TextMeshPro displayText;
 [SerializeField] MinigameManager minigameManager;
 Vector3 originalScale;

 void Start()
 {
  originalScale = transform.localScale;
 }

 void OnMouseDown()
 {
  if(!minigameManager.keypadOnCD)
  {
   transform.localScale = new Vector3(0.5f,originalScale.y,originalScale.z); 
   displayText.text = displayText.text.Remove(displayText.text.Length -1);
  }
 }

 void OnMouseUp()
 {
  transform.localScale = originalScale;
 }
}
}