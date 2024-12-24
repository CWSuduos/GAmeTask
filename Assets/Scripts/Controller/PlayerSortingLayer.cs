using UnityEngine;

public class PlayerSortingLayer : MonoBehaviour
{
    public string sortingLayerName = "Foreground";
    public int orderInLayer = 10;                 

    void Start()
    {
       
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingLayerName = sortingLayerName;
            spriteRenderer.sortingOrder = orderInLayer;
        }
        
    }
}