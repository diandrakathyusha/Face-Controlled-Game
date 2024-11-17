using System.Collections.Generic;
using UnityEngine;

public class SceneObjectManager : MonoBehaviour
{
    public static SceneObjectManager Instance { get; private set; }

    private Dictionary<string, GameObject> objectLookup = new Dictionary<string, GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            RegisterAllObjects();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void RegisterAllObjects()
    {
        ObjectID[] allObjects = FindObjectsOfType<ObjectID>(true); // Include inactive objects
        foreach (var obj in allObjects)
        {
            if (!objectLookup.ContainsKey(obj.ID))
            {
                objectLookup.Add(obj.ID, obj.gameObject);
            }
        }
    }

    public GameObject GetObjectByID(string id)
    {
        objectLookup.TryGetValue(id, out GameObject result);
        return result;
    }

    public void RefreshRegistry()
    {
        objectLookup.Clear();
        RegisterAllObjects();
    }
}
