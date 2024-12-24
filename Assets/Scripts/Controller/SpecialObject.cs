using UnityEngine;

public class SpecialObject : MonoBehaviour
{
    public bool isSpecial = false;

    public void MakeSpecial()
    {
        isSpecial = true;
        Debug.Log($"[SpecialObject] ������ {gameObject.name} ���� ���������");
    }

    public void RemoveSpecial()
    {
        isSpecial = false;
        Debug.Log($"[SpecialObject] ������ {gameObject.name} �������� ���� ���������");
    }

    private void OnDestroy()
    {
        if (isSpecial)
        {
            Debug.Log($"[SpecialObject] ��������� ������ ������: {gameObject.name}");
        }
    }
}