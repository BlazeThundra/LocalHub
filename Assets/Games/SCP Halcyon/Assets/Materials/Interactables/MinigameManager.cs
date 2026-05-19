using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class MinigameManager : MonoBehaviour
{
 public string currentMinigame;
 public int minigamesLeft;
 [SerializeField] HazardLevels hazardLevels;

 [Header("Drag Minigame Variables")]
 [SerializeField] GameObject handle;
 [SerializeField] public Vector3 dragStartPos;
 [SerializeField] GameObject dragPanel;
 [SerializeField] Renderer dragPanelRenderer;
 Color dragPanelOriginalColor;
 public bool dragOnCD;
 
 [Header("Arrow Minigame Variables")]
 [SerializeField] GameObject RC;
 [SerializeField] public Vector3 RCStartPos;
 [SerializeField] GameObject arrowPanel;
 [SerializeField] Renderer arrowPanelRenderer;
 [SerializeField] Renderer RCRenderer;
 [SerializeField] List<GameObject> arrowButtons = new List<GameObject>();
 Color RCOriginalColor;
 Color arrowPanelOriginalColor;
 public bool arrowOnCD;

 [Header("Keypad Variables")]
 [SerializeField] GameObject keypadText;
 [SerializeField] GameObject keypadPanel;
 [SerializeField] Renderer keypadPanelRenderer;
 [SerializeField] TextMeshPro keypadDisplayText;
 [SerializeField] GameObject glass;
 [SerializeField] Transform keypadWire;
 [SerializeField] Tasks taskBox;
 Color keypadPanelOriginalColor;
 public bool keypadOnCD;

 [Header("Safe Variables")]
 [SerializeField] GameObject safe;
 [SerializeField] GameObject safePanel;
 

 void Start()
 {
  dragPanelOriginalColor = dragPanelRenderer.material.color;
  RCOriginalColor = RCRenderer.material.color;
  arrowPanelOriginalColor = arrowPanelRenderer.material.color;
  keypadPanelOriginalColor = keypadPanelRenderer.material.color;
 }

 public void MinigameCompleted()
 {
  if(currentMinigame == "Drag")
  {
   dragPanelRenderer.material.color = Color.green;
   dragOnCD = true;
  }

  if(currentMinigame == "Arrow")
  {
   arrowPanelRenderer.material.color = Color.green;
   arrowOnCD = true;
  }

  if(currentMinigame == "Keypad")
  {
   keypadPanelRenderer.material.color = Color.green;
   keypadOnCD = true;
   glass.GetComponent<Glass>().OpenGlass();
   foreach(Transform child in keypadWire)
   {
    child.GetComponent<Renderer>().material.color = Color.green;
   }
  }

  taskBox.TaskCompleted(currentMinigame);
 }

 public void MinigameFailed()
 {
  StartCoroutine(ResetMinigame());
 }

 IEnumerator ResetMinigame()
 {
  if(currentMinigame == "Drag")
  { 
   dragOnCD = true;
   handle.transform.localPosition = dragStartPos;
   dragPanelRenderer.material.color = Color.red;
   yield return new WaitForSeconds(10f);
   dragPanelRenderer.material.color = dragPanelOriginalColor;
   dragOnCD = false;
  }

  if(currentMinigame == "Arrow")
  {
   arrowOnCD = true;
   foreach(GameObject button in arrowButtons)
   {
    button.GetComponent<ArrowButton>().pressed = false;
    button.transform.localScale = new Vector3(1f, 0.1f, 1f);
   }
   RC.transform.localPosition = RCStartPos;
   arrowPanelRenderer.material.color = Color.red;
   yield return new WaitForSeconds(10f);
   arrowPanelRenderer.material.color = arrowPanelOriginalColor;
   arrowOnCD = false;
  }

  if(currentMinigame == "Keypad")
  {
   keypadOnCD = true;
   keypadPanelRenderer.material.color = Color.red;
   keypadDisplayText.text = keypadDisplayText.text.Remove(keypadDisplayText.text.Length - keypadDisplayText.text.Length);
   yield return new WaitForSeconds(5f);
   keypadPanelRenderer.material.color = keypadPanelOriginalColor;
   keypadOnCD = false;
  }
 }
}
