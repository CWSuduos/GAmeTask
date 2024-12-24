using UnityEngine;
using System.Collections.Generic;

public class PersistentReferenceManager : MonoBehaviour
{
    public static PersistentReferenceManager Instance;

    // ������� ��� �������� ���� �������� � �� �����
    private Dictionary<string, Dictionary<string, Object>> savedReferences = new Dictionary<string, Dictionary<string, Object>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            CaptureAllReferences();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// ��������� ��� ��������� ���� ���� ����������� MonoBehaviour �� �����.
    /// </summary>
    private void CaptureAllReferences()
    {
        savedReferences.Clear(); // ������� ������� ����� ����� ������������
        MonoBehaviour[] allScripts = FindObjectsOfType<MonoBehaviour>();

        foreach (MonoBehaviour script in allScripts)
        {
            if (script == null) continue;

            string objectName = script.gameObject.name;
            string scriptName = script.GetType().Name;

            string key = $"{objectName}_{scriptName}";

            if (!savedReferences.ContainsKey(key))
            {
                savedReferences[key] = new Dictionary<string, Object>();

                var fields = script.GetType().GetFields();
                foreach (var field in fields)
                {
                    if (field.FieldType.IsSubclassOf(typeof(Object)) || field.FieldType == typeof(Object))
                    {
                        Object fieldValue = (Object)field.GetValue(script);
                        if (fieldValue != null)
                        {
                            savedReferences[key][field.Name] = fieldValue;
                            Debug.Log($"��������� ������: {key} -> {field.Name}");
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// ��������������� ���������� ������ �� ��� ���������� MonoBehaviour.
    /// </summary>
    public void RestoreAllReferences()
    {
        MonoBehaviour[] allScripts = FindObjectsOfType<MonoBehaviour>();

        foreach (MonoBehaviour script in allScripts)
        {
            if (script == null) continue;

            string objectName = script.gameObject.name;
            string scriptName = script.GetType().Name;

            string key = $"{objectName}_{scriptName}";

            if (savedReferences.ContainsKey(key))
            {
                var fields = script.GetType().GetFields();
                foreach (var field in fields)
                {
                    if (savedReferences[key].ContainsKey(field.Name))
                    {
                        field.SetValue(script, savedReferences[key][field.Name]);
                        Debug.Log($"������������� ������: {key} -> {field.Name}");
                    }
                }
            }
        }
    }
}