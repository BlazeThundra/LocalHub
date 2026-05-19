using UnityEngine;
using TMPro;

namespace SCPHalcyon
{
public class EnterButton : MonoBehaviour
{
 [SerializeField] TextMeshPro displayText;
 [SerializeField] MinigameManager minigameManager;
 Vector3 originalScale;
 string keyPass;

 [SerializeField] TextMeshPro char1;
 [SerializeField] TextMeshPro char2;
 [SerializeField] TextMeshPro char3;
 [SerializeField] TextMeshPro char4;

 void Start()
 {
  originalScale = transform.localScale;
  ChangePass();
 }

//DEV TOOL
/*
 void Update()
 {
  if(Input.GetKey(KeyCode.Space))
  {
   ChangePass();
  }
 }
*/
 void ChangePass()
 {
  char1.text = Random.Range(1,10).ToString();
  char2.text = Random.Range(1,10).ToString();
  char3.text = Random.Range(1,10).ToString();
  char4.text = Random.Range(1,10).ToString();
  keyPass = char1.text + char2.text + char3.text + char4.text;
 }

 void OnMouseDown()
 {
  if(!minigameManager.keypadOnCD)
  {
   transform.localScale = new Vector3(0.25f,originalScale.y,originalScale.z); 
   if(displayText.text == keyPass)
   {
    minigameManager.MinigameCompleted();
   }
   if(displayText.text != keyPass)
   {
    minigameManager.MinigameFailed();
   }
  }
 }

 void OnMouseUp()
 {
  transform.localScale = originalScale;
 }
}
}