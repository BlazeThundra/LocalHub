using UnityEngine;
using TMPro;
using System.Collections;

namespace SCPHalcyon
{
public class Tasks : MonoBehaviour
{
 [SerializeField] TextMeshProUGUI textBox;

 bool dragDone;
 bool arrowDone;
 bool keypadDone;
 bool safeDone;
 bool buttonDone;

 [SerializeField] HazardLevels hazardLevels;

 string taskSection;

 void Start()
 {
  taskSection = "Start";
  UpdateTaskUI();
 }

 public void TaskCompleted(string task)
 {
  if(task == "Drag"){dragDone = true;}
  if(task == "Arrow"){arrowDone = true;}
  if(task == "Keypad"){keypadDone = true;}
  if(task == "Safe"){safeDone = true;}
  if(task == "Button"){buttonDone = true;}

  UpdateTaskUI();
 }

 public void UpdateTaskUI()
 {
  if(taskSection == "Start")
  {
   textBox.text = "-Find a way to open the door";
   return;
  }

  if(taskSection == "Main")
  {
   string line1 = FormatTask("Drag Minigame", dragDone);
   string line2 = FormatTask("Arrow Minigame", arrowDone);
   string line3 = FormatTask("Keypad Minigame", keypadDone);
   string line4 = FormatTask("Safe Minigame", safeDone);
   textBox.text = line1 + "\n" + line2 + "\n" + line3 + "\n" + line4;
   return;
  }

  if(taskSection == "Button")
  {
   textBox.text = "-Find and activate the control button";
   return;
  }

  if(taskSection == "Won")
  {
   textBox.text = "-Await halcyon";
   return;
  }
 }

 string FormatTask(string taskName, bool isComplete)
 {
  if(isComplete)
  {
   return "<color=#00FF00><s>-" + taskName + "</s></color>";
  }
  return "<color=#FFFFFF>-" + taskName + "</color>";
 }

 public IEnumerator FirstTaskComplete()
 {
  textBox.text = "<color=#00FF00><s>-Find a way to open the door</s></color>";
  yield return new WaitForSeconds(2f);
  taskSection = "Main";
  UpdateTaskUI();
 }

 public IEnumerator MainTasksComplete()
 {
  
  yield return new WaitForSeconds(2f);
  UpdateTaskUI();
 }

 public void ButtonTaskComplete()
 {
  taskSection = "Won";
  hazardLevels.Win();
 }
}
}