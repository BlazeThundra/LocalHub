using UnityEngine;

namespace SCPHalcyon
{
public class SafeScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] GameObject safeUI;
    [SerializeField] int firstNum;
    [SerializeField] int secondNum;
    [SerializeField] int thirdNum;
    [SerializeField] AudioClip safeOpenSound;
    [SerializeField] MinigameManager minigameManager;
    AudioSource audioSource;
    bool safeUIActive;


    Animator anim;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    int firstNumInput;
    int secondNumInput;
    int thirdNumInput;
    void OnMouseDown()
    {
        Debug.Log("Safe Clicked");
        if (!safeUIActive)
        {
            safeUI.SetActive(true);
            safeUIActive = true;
        }
        else
                {
            safeUIActive = false;
            safeUI.SetActive(false);
        }
    }   

    public void OpenSafe()
    {
        GetComponent<Animator>().SetTrigger("SafeOpen");
        GetComponent<BoxCollider>().enabled = false;
        audioSource.PlayOneShot(safeOpenSound);
        minigameManager.MinigameCompleted();
    }
}
}