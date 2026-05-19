using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SCPHalcyon
{
public class HazardLevels : MonoBehaviour
{
 [SerializeField] RoomManager roomManager;
 [SerializeField] public float hazardLevel = 0f;
 [SerializeField] int hazardStage = 1;
 [SerializeField] float hazardIncrease = .005f;
 [SerializeField] List<GameObject> normalLights = new List<GameObject>();
 [SerializeField] List<GameObject> emergencyLights = new List<GameObject>();
 [SerializeField] List<GameObject> eLightParents = new List<GameObject>();
 [SerializeField] GameObject slider;
 [SerializeField] GameObject LossCanvas;
 [SerializeField] GameObject WinCanvas;
 [SerializeField] Transform winPos;
 [SerializeField] Transform winTarget;
 public bool won = false;

 [Header("Audio")]
 [SerializeField] AudioSource ambiencePlayer;
 [SerializeField] AudioClip lowHazardAmbience;
 [SerializeField] AudioClip highHazardAmbience;
 [SerializeField] AudioClip medHazardAmbience;

 public bool started = false;

 public bool frozen;

 void Awake()
 {
  won = false;
 }

 void FixedUpdate()
 {
  if(!frozen && started)
  {
   hazardLevel += hazardIncrease;
   slider.GetComponent<Slider>().value = hazardLevel;
  }
  //when hazardlevel hits an interval of 10 increases stage and speed
  if(hazardLevel / 10 > hazardStage && !won)
  {
   hazardStage ++;
   hazardIncrease = hazardIncrease * 1.2f;
  }

  //Turns off regular lights and enables emergency lights
  if(hazardStage == 2)
  {
            ambiencePlayer.clip = medHazardAmbience;
            ambiencePlayer.Play();
            foreach (GameObject light in normalLights)
   {
    light.SetActive(false);
   }
   foreach(GameObject eLight in emergencyLights)
   {
    eLight.SetActive(true);
   }
  }
  if(hazardStage == 3)
  {
   foreach(GameObject eLight in eLightParents)
   {
    eLight.GetComponent<EmergencyLights>().spinSpeed = 360;
   }
   
        }
  if(hazardStage == 4)
  {
            ambiencePlayer.clip = highHazardAmbience;
            ambiencePlayer.Play();
            foreach (GameObject eLight in eLightParents)
   {
    eLight.GetComponent<EmergencyLights>().spinSpeed = 540;

            }
  }
  if(hazardStage == 5)
  {
   foreach(GameObject eLight in eLightParents)
   {
    eLight.GetComponent<EmergencyLights>().spinSpeed = 720;
   }
  }
  if(hazardStage == 6)
  {
   Lose();
  }

  if(won == true && hazardLevel <=0)
  {
   WinCanvas.SetActive(true);
  }
 }

 public void Lose()
 {
  LossCanvas.SetActive(true);
  Time.timeScale = 0;
 }

 public void Win()
 {
  roomManager.EnablePanelColliders(false);

  Vector3 direction = winTarget.position - winPos.position;
  Quaternion endRot = Quaternion.LookRotation(direction);
  roomManager.StartMove(winPos.position, endRot);
  
  frozen = false;
  hazardIncrease = hazardIncrease * (-10);
  won = true;
 }

 public void RestartButton()
 {
  int currentSceneIndex = (int)SceneManager.GetActiveScene().buildIndex;
  SceneManager.LoadScene(currentSceneIndex);
 }
}
}