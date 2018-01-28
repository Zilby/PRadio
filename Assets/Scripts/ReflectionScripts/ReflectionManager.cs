using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionManager : MonoBehaviour {

    public List<GameObject> levels;

    public Transform screen;
    public GameObject levelInstance;

    [System.NonSerialized]
    public static ReflectionManager instance;

    void Awake() {
        if (instance != null) {
            Destroy(instance.gameObject);
        }
        instance = this;
    }

    public void StartReflectionLevel() {
        levelInstance = Instantiate(levels[Random.Range(0, levels.Count)], screen.transform.position, Quaternion.identity);
        levelInstance.transform.SetParent(screen.transform);
    }

    public void EndLevel() {
        Destroy(levelInstance.gameObject);
    }
}
