using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObjectGeneratorList : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public Transform[] spawnPoints;
    private List<GameObject> spawnedObjects = new List<GameObject>();

    void Start()
    {
        if (!ValidateSetup()) return;
        SpawnObjects();
    }

    private bool ValidateSetup()
    {
        if (objectsToSpawn.Length == 0) { Debug.LogError("Objects to Spawn array is empty!", this); return false; }
        if (spawnPoints.Length != 4) { Debug.LogError("Spawn Points array must have exactly 4 elements!", this); return false; }
        foreach (var point in spawnPoints)
        {
            if (point == null) { Debug.LogError("One of the Spawn Points is null!", this); return false; }
        }
        return true;
    }

    private void SpawnObjects()
    {
        if (!ValidateSetup()) return;

        ClearSpawnedObjects();

        GameObject[] selectedObjects = new GameObject[4];
        for (int i = 0; i < 4; i++)
        {
            selectedObjects[i] = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];
        }

        GameObject[] shuffledSelectedObjects = ShuffleArray(selectedObjects);

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject objectToSpawn = shuffledSelectedObjects[i];

            if (objectToSpawn == null)
            {
                Debug.LogWarning("One of the objects in the array for spawning is null.");
                continue;
            }

            Vector3 spawnPosition = spawnPoints[i].position;
            GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, spawnPoints[i].rotation);
            spawnedObjects.Add(spawnedObject);

            ObjectBehavior pairedObj = spawnedObject.GetComponent<ObjectBehavior>();
            if (pairedObj == null)
            {
                pairedObj = spawnedObject.AddComponent<ObjectBehavior>();
            }
            pairedObj.SetGenerator(this);
            spawnedObject.tag = "pairedTag";

            Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            ConfineToCameraView(spawnedObject);

            Debug.Log($"Spawned object {objectToSpawn.name} at {spawnedObject.transform.position} with rotation {spawnedObject.transform.rotation}");
        }
    }

    private GameObject[] ShuffleArray(GameObject[] array)
    {
        GameObject[] shuffledArray = (GameObject[])array.Clone();
        for (int i = shuffledArray.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            GameObject temp = shuffledArray[i];
            shuffledArray[i] = shuffledArray[randomIndex];
            shuffledArray[randomIndex] = temp;
        }
        return shuffledArray;
    }

    public void ManualRespawn()
    {
        SpawnObjects();
    }

    private void ClearSpawnedObjects()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        spawnedObjects.Clear();
    }

    public void OnPairedObjectDestroyed()
    {
        int destroyedCount = 0;
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj == null)
            {
                destroyedCount++;
            }
        }

        if (destroyedCount >= 4)
        {
            SpawnObjects();
        }
    }

    private void ConfineToCameraView(GameObject obj)
    {
        if (obj == null || Camera.main == null) return;

        Vector3 viewportPos = Camera.main.WorldToViewportPoint(obj.transform.position);
        bool isOnScreen = viewportPos.z > 0 && viewportPos.x > 0 && viewportPos.x < 1 && viewportPos.y > 0 && viewportPos.y < 1;

        if (!isOnScreen)
        {
            viewportPos.x = Mathf.Clamp01(viewportPos.x);
            viewportPos.y = Mathf.Clamp01(viewportPos.y);

            if (viewportPos.z <= 0)
            {
                viewportPos.z = 0.1f;
            }

            obj.transform.position = Camera.main.ViewportToWorldPoint(viewportPos);

            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }

    void Update()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            ConfineToCameraView(obj);
        }
    }

    public void Restart()
    {
        //Since we're not using deletedPairedTagCount anymore, we just need to respawn
        SpawnObjects();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        foreach (Transform point in spawnPoints)
        {
            if (point != null)
            {
                Gizmos.DrawSphere(point.position, 0.2f);
            }
        }

        if (Camera.main)
        {
            Gizmos.color = Color.yellow;
            Matrix4x4 temp = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(Camera.main.transform.position, Camera.main.transform.rotation, Vector3.one);
            Gizmos.DrawFrustum(Vector3.zero, Camera.main.fieldOfView, Camera.main.farClipPlane, Camera.main.nearClipPlane, Camera.main.aspect);
            Gizmos.matrix = temp;
        }
    }
}

