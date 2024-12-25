using UnityEngine;
using UnityEngine.UI;

public class BringUIToFront : MonoBehaviour
{
    public Canvas canvasToBringToFront; // Reference to the Canvas you want to bring to front.

    void Start()
    {
        if (canvasToBringToFront == null)
        {
            Debug.LogError("Canvas to bring to front is not assigned!");
            return;
        }
        SetCanvasToFront();

    }

    public void SetCanvasToFront()
    {

        canvasToBringToFront.overrideSorting = true;
        canvasToBringToFront.sortingOrder = 999; // Or any large number higher than other canvases.

        //Make sure the canvas is a screen space overlay, or these settings
        //won't take effect.
        if (canvasToBringToFront.renderMode != RenderMode.ScreenSpaceOverlay)
        {
            Debug.LogWarning("Canvas render mode is not ScreenSpaceOverlay. overrideSorting won't work.");
            canvasToBringToFront.renderMode = RenderMode.ScreenSpaceOverlay; //Force it to be overlay
        }
        Debug.Log(canvasToBringToFront.name + " sorting order set to: " + canvasToBringToFront.sortingOrder);
    }
    //Example of how to call this from another script or button
    public void BringSpecificCanvasToFront(Canvas canvas)
    {
        if (canvas == null)
        {
            Debug.LogError("Canvas to bring to front is null!");
            return;
        }
        canvas.overrideSorting = true;
        canvas.sortingOrder = 1000; //Even higher than the other
        if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
        {
            Debug.LogWarning("Canvas render mode is not ScreenSpaceOverlay. overrideSorting won't work.");
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }
        Debug.Log(canvas.name + " sorting order set to: " + canvas.sortingOrder);
    }
}