using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Hub
{
public class HubManager : MonoBehaviour
{
 Image knob;
 private float holdTimer = 0f;
 [SerializeField] private float requiredHoldTime = 2f;
 
 void Awake()
 {
  knob = GetComponentInChildren<Image>();
  holdTimer = 0f;
  DontDestroyOnLoad(gameObject);
 }

 void Update()
 {
  if(Input.GetKey(KeyCode.Escape))
  {
   holdTimer += Time.deltaTime;
   knob.fillAmount = holdTimer/requiredHoldTime;

   if(holdTimer >= requiredHoldTime)
   {
    holdTimer = 0f;
    LoadScene("Hub");
   }
  }

  if(Input.GetKeyUp(KeyCode.Escape))
  {
   holdTimer = 0f;
   knob.fillAmount = holdTimer/requiredHoldTime;
  }
 }

 public void LoadScene(string sceneName)
 {
  SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
 }
}
}