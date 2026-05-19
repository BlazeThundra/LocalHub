using UnityEngine;
using UnityEngine.UI;

namespace ChessTheMaked
{
public class Flee : MonoBehaviour
{
    [Header("References")] //format INSECTOR :GASP:
    public RectTransform uielement;  
    public Canvas canvas;  

    [Header("Flee Settings")]
    public float escapeDistance = 120f;   
    public float fleeMultiplier = 1.5f;    
    public float moveSpeed = 12f;         
    public float returnSpeed = 6f;         

    [Header("Optional limits")]
    public float maxFleeDistance = 250f;  
    public Vector2 paddingFromEdges = new Vector2(10f, 10f); 

    private Vector2 originalAnchoredPos;
    private RectTransform parentRect;

    void Awake()
    {
        if (uielement == null) Debug.LogError("UIFlee: uiElement is not assigned."); //If you have no UI element you get an error
        if (canvas == null) canvas = GetComponentInParent<Canvas>(); //if nothing is in canvas it grabs a canvas
        if (canvas == null) Debug.LogError("UIFlee: Canvas not found. Assign a Canvas."); // if still no canvas then you get an error

        parentRect = uielement.parent as RectTransform;
        originalAnchoredPos = uielement.anchoredPosition; //gets position before game starts
    }

    void Update()
    {
        if (uielement == null || canvas == null) return; // if you cant find both then do nothing

        Vector2 localMousePos; // gets the mouses position in relation to the screen
        bool valid = RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentRect, 
            Input.mousePosition, 
            (canvas.renderMode == RenderMode.ScreenSpaceOverlay) ? null : canvas.worldCamera, 
            out localMousePos
        );

        if (!valid) return; // if the conversion failed it will restart

        Vector2 elementPos = uielement.anchoredPosition; //ui position
        Vector2 diff = elementPos - localMousePos; // the distance between mouse and ui
        float dist = diff.magnitude; // how far is the ui 

        Vector2 target = originalAnchoredPos; // the original position is sought to go back to

        if (dist < escapeDistance)
        {
            Vector2 dirAway = (elementPos - localMousePos).normalized; // steers UI in direction away from mouse
            if (dirAway == Vector2.zero) dirAway = Random.insideUnitCircle.normalized; // go in a random direction if its in the center

            float fleeAmount = (escapeDistance - dist) * fleeMultiplier; // how far to run away when mouse is close

            target = originalAnchoredPos + dirAway * fleeAmount; // target to run away

            Vector2 offsetFromOriginal = target - originalAnchoredPos; // how far has the ui moved from the start
            if (offsetFromOriginal.magnitude > maxFleeDistance) // if it goes to far out of its max distance then 
                target = originalAnchoredPos + offsetFromOriginal.normalized * maxFleeDistance; // run within the bounds

            target = ClampToParentBounds(target, uielement, parentRect, paddingFromEdges); // dont go out of the screen
        }

        float speed = (target == originalAnchoredPos) ? returnSpeed : moveSpeed; // caluclates whether you need return speed or run speed
        uielement.anchoredPosition = Vector2.Lerp(uielement.anchoredPosition, target, Time.deltaTime * speed); // lerps from point a to point b using target and speed
    }

    // keeps ui in screen so it doesn't run too far
    Vector2 ClampToParentBounds(Vector2 candidateAnchoredPos, RectTransform element, RectTransform parent, Vector2 pad)
    {
        Vector2 parentSize = parent.rect.size; // gets size of the background
        Vector2 elementSize = element.rect.size; // gets size of the ui

        Vector2 min = - (parentSize * 0.5f) + (elementSize * element.pivot) + pad; // gets the minimum distance before leaving the bound
        Vector2 max = (parentSize * 0.5f) - (elementSize * (Vector2.one - element.pivot)) - pad; // gets the max

        float x = Mathf.Clamp(candidateAnchoredPos.x, min.x, max.x); // makes sure it stays within max x a=and y and min x and y
        float y = Mathf.Clamp(candidateAnchoredPos.y, min.y, max.y);

        return new Vector2(x, y); // return new position
    }
}
}