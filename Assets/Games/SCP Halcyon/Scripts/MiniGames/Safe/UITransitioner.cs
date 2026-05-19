using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SCPHalcyon
{
public class UITransitioner : MonoBehaviour
{
    [SerializeField] private TMP_InputField[] inputFields;

    void OnEnable()
    {
        for (int i = 0; i < inputFields.Length; i++)
        {
            int index = i; 
            inputFields[i].onValueChanged.AddListener((value) => CheckInput(value, index));
        }
    }

    void CheckInput(string input, int currentIndex)
    {
        if (input.Length >= 1)
        {
            MoveToNextField(currentIndex);
        }
    }

    void MoveToNextField(int currentIndex)
    {

        int nextIndex = currentIndex + 1;

        if (nextIndex < inputFields.Length)
        {
            inputFields[currentIndex].ActivateInputField();
            EventSystem.current.SetSelectedGameObject(inputFields[nextIndex].gameObject);
        }
    }

    void OnDisable()
    {
        foreach (var field in inputFields)
        {
            field.onValueChanged.RemoveAllListeners();
        }
    }
}
}