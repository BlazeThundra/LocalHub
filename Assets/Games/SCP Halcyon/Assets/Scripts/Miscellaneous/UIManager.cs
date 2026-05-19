using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

namespace SCPHalcyon
{
public class UIManager : MonoBehaviour
{
 [SerializeField] private float defaultDuration = 0.3f;
 
 [Header("References")]
 [SerializeField] Button inventoryButtonComponent;
 [SerializeField] List<Button> inventorySlots = new List<Button>();
 [SerializeField] int rowsPerColumn = 3; 

 [Header("Images")]
 [SerializeField] Image inventoryImage;
 [SerializeField] Image notesImage;
 
 [Header("Highlight Settings")]
 [SerializeField] float highAlpha = 1.0f;
 [SerializeField] float lowAlpha = 0.3f;
 private Image lastSelectedImage;

 [Header("Current State")]
 public string equippedType = "none";
 public int currentEquippedLevel = 0;

 bool invEnabled = false;
 bool invActive = false;
    bool notesActive = false;
    bool notesEnabled = false;
    int[] columnPositions = new int[4];

 void Update()
 {
  if (Input.GetKeyDown(KeyCode.Tab)) inventoryButtonComponent.onClick.Invoke();

  // Logic: Cycle Columns 1-4
  if (Input.GetKeyDown(KeyCode.Alpha1)) CycleColumn(0);
  if (Input.GetKeyDown(KeyCode.Alpha2)) CycleColumn(1);
  if (Input.GetKeyDown(KeyCode.Alpha3)) CycleColumn(2);
  if (Input.GetKeyDown(KeyCode.Alpha4)) CycleColumn(3);
 }

 void CycleColumn(int column)
 {
  int slotIndex = column + (columnPositions[column] * 4);

  if (slotIndex < inventorySlots.Count && inventorySlots[slotIndex] != null)
  {
   InventorySlot slotScript = inventorySlots[slotIndex].GetComponent<InventorySlot>();

   if (slotScript != null && slotScript.isOccupied)
   {
    // Reset old BG
    if (lastSelectedImage != null) SetAlpha(lastSelectedImage, lowAlpha);

    // Trigger the actual Equip logic in the slot
    slotScript.Equip();

    // Set new BG Highlight
    lastSelectedImage = inventorySlots[slotIndex].GetComponent<Image>();
    if (lastSelectedImage != null) SetAlpha(lastSelectedImage, highAlpha);
   }
   else
   {
    Unequip();
   }
  }

  // Move to next row for next press
  columnPositions[column] = (columnPositions[column] + 1) % rowsPerColumn;
  for (int i = 0; i < columnPositions.Length; i++) if (i != column) columnPositions[i] = 0;
 }

 public void Unequip()
 {
  if (lastSelectedImage != null) SetAlpha(lastSelectedImage, lowAlpha);
  lastSelectedImage = null;

  equippedType = "none";
  currentEquippedLevel = 0;

  GameObject mouseSprite = GameObject.Find("MouseSprite");
  if (mouseSprite != null) mouseSprite.GetComponent<Image>().enabled = false;
  
  Debug.Log("Unequipped everything.");
 }

 void SetAlpha(Image img, float alpha)
 {
  if (img == null) return;
  Color c = img.color;
  c.a = alpha;
  img.color = c;
 }

 // --- Menu Sliding Logic ---
 public void InventoryButton(Image targetImage)
 {
  if (invActive) return;
  invEnabled = !invEnabled;
  TranslateElement(targetImage, invEnabled, 400f);
 }

    public void NotesButton(Image targetImage)
    {
        if (notesActive) return;
        notesEnabled = !notesEnabled;
        TranslateElement(targetImage, notesEnabled, 300f);
    }

 public void TranslateElement(Image image, bool buttonEnabled, float buttonTranslate)
 {
  Vector2 delta = new Vector2(buttonTranslate, 0f);
  RectTransform rt = image.rectTransform;
  if (rt == null) return;
  if (!buttonEnabled) delta *= -1;
    if (rt == notesImage.rectTransform)
        {
            delta *= -1;
        }
        StartCoroutine(TranslateCoroutine(rt, delta, defaultDuration));
 }

 IEnumerator TranslateCoroutine(RectTransform rt, Vector2 delta, float duration)
 {
  if (rt == inventoryImage.rectTransform)
        {
            invActive = true;
        } 
        else if (rt == notesImage.rectTransform)
        {
            notesActive = true;
        }
            Vector2 start = rt.anchoredPosition;
  Vector2 end = start + delta;
  float elapsed = 0f;

  while (elapsed < duration)
  {
   elapsed += Time.deltaTime;
   rt.anchoredPosition = Vector2.Lerp(start, end, Mathf.Clamp01(elapsed / duration));
   yield return null;
  }

  rt.anchoredPosition = end;
        if (rt == inventoryImage.rectTransform)
        {
            invActive = false;
        }
        else if (rt == notesImage.rectTransform)
        {
            notesActive = false;
        }
    }
}
}