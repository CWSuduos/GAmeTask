using UnityEngine;
using System;

public class ObjectDestroyer : MonoBehaviour
{
    public event Action<GameObject> OnObjectDestroyed;

    private void OnDestroy()
    {
        if (OnObjectDestroyed != null)
        {
            OnObjectDestroyed(gameObject);
        }
    }
}