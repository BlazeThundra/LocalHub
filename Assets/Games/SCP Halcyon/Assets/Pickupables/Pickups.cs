using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Pickups", menuName = "Scriptable Objects/Pickups")]
public class Pickups : ScriptableObject
{
    public string pickupName;
    public string description;
    public float weight;
    public GameObject worldObject;
    public Sprite inspectionImage;
    public Sprite inventoryIcon;
    public string usageText;
    public int usageLevel;
}
