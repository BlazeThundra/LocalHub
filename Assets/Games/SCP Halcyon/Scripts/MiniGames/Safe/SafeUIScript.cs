using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

namespace SCPHalcyon
{
public class SafeUIScript : MonoBehaviour
{

    [SerializeField] int thisNum;
    [SerializeField] GameObject safe;
    [SerializeField] GameObject otherInput;
    [SerializeField] GameObject otherInput2;
    [SerializeField] bool isCorrect = false;

    TMP_InputField inputField;

    int numInput;

    private void Start()
    {
        inputField = this.GetComponent<TMP_InputField>();
    }
    public void CodeInput()
    {
        numInput = int.Parse(inputField.text);
        if (numInput == thisNum)
        {
            isCorrect = true;
            if (otherInput.GetComponent<SafeUIScript>().isCorrect && otherInput2.GetComponent<SafeUIScript>().isCorrect)
            {
                this.transform.parent.gameObject.SetActive(false);
                this.transform.parent.GetChild(1).gameObject.SetActive(false);
                safe.GetComponent<SafeScript>().OpenSafe();
            }
        } else
        {
          isCorrect = false;
        }
    }
}
}