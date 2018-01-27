using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem {
    public int amountToPool;
    public GameObject objectToPool;
    public bool shouldExpand;
}

public class ObjectPooler : MonoBehaviour {

    public static ObjectPooler instance = null;

    private Dictionary<string, List<GameObject>> pooledObjects;
    private const int tooManyItems = 50;
    public List<ObjectPoolItem> itemsToPool;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(instance.gameObject);
            instance = this;
        }


        pooledObjects = new Dictionary<string, List<GameObject>>();
        foreach (ObjectPoolItem item in itemsToPool) {
            pooledObjects[item.objectToPool.tag] = new List<GameObject>();
            for (int i = 0; i < item.amountToPool; i++) {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects[obj.tag].Add(obj);
            }
        }
    }


    void Update() {
        foreach (KeyValuePair<string, List<GameObject>> objects in pooledObjects) {
            if (objects.Value.Count > tooManyItems) {
                for (int i = objects.Value.Count - 1; i >= 0; i--) {
                    if (!objects.Value[i].activeSelf) {
                        GameObject temp = objects.Value[i];
                        objects.Value.RemoveAt(i);
                        Destroy(temp);
                    }
                }
            }
        }
    }

    public GameObject GetPooledObject(string tag) {
        foreach (GameObject pooledObject in pooledObjects[tag]) {
            if (!pooledObject.activeSelf) {
                return pooledObject;
            }
        }
        foreach (ObjectPoolItem item in itemsToPool) {
            if (item.objectToPool.tag == tag) {
                if (item.shouldExpand) {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects[tag].Add(obj);
                    return obj;
                }
            }
        }
        return null;
    }

    public GameObject GetPooledObjectAtPosition(string tag, Vector3 position, Quaternion rotation) {
        GameObject temp = this.GetPooledObject(tag);
        if (temp != null) {
            temp.transform.position = position;
            temp.transform.rotation = rotation;
        }
        return temp;
    }
}
