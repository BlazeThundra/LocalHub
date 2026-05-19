using UnityEngine;
using TMPro;

namespace SCPHalcyon
{
public class KeypadButton : MonoBehaviour
{
 [SerializeField] TextMeshPro displayText;
 [SerializeField] string number;
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
   print("worked");
   transform.localScale = new Vector3(0.5f,originalScale.y,originalScale.z); 
   if(displayText.text.Length < 4)
   {
    displayText.text += number;
   }
  }
 }

 void OnMouseUp()
 {
  transform.localScale = originalScale;
 }
}
}