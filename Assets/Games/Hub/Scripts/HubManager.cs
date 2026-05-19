using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hub
{
public class HubManager : MonoBehaviour
{
 public void LoadScene(string sceneName)
 {
  SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
 }
}
}