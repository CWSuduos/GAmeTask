using UnityEngine;

public class SpecialObject : MonoBehaviour
{
    public bool isSpecial = false;

    public void MakeSpecial()
    {
        isSpecial = true;
        Debug.Log($"[SpecialObject] Объект {gameObject.name} стал особенным");
    }

    public void RemoveSpecial()
    {
        isSpecial = false;
        Debug.Log($"[SpecialObject] Объект {gameObject.name} перестал быть особенным");
    }

    private void OnDestroy()
    {
        if (isSpecial)
        {
            Debug.Log($"[SpecialObject] Уничтожен особый объект: {gameObject.name}");
        }
    }
}