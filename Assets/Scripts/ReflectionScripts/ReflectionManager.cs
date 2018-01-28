using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionManager : MonoBehaviour {

    public List<GameObject> levels;

    public Transform screen;
    public Vector3 offpos;
    public Vector3 onPos;
    public float speed;
    private bool started;

    private GameObject levelInstance;

    [System.NonSerialized]
    public static ReflectionManager instance;

    void Awake() {
        if (instance != null) {
            Destroy(instance.gameObject);
        }
        instance = this;
    }

    public void StartReflectionLevel() {
        if (!started) {
            levelInstance = Instantiate(levels[Random.Range(0, levels.Count)], screen.transform.position, Quaternion.identity);
            levelInstance.transform.SetParent(screen.transform);
            StartCoroutine(MoveLevelOn());
            started = true;
        }
    }

    private IEnumerator MoveLevelOn() {
        float elapsedTime = 0;
        while (elapsedTime < speed) {
            screen.transform.position = Vector3.Lerp(offpos, onPos, (elapsedTime / speed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator MoveLevelOff() {
        float elapsedTime = 0;
        while (elapsedTime < speed) {
            screen.transform.position = Vector3.Lerp(onPos, offpos, (elapsedTime / speed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        started = false;
    }

    public void EndLevel() {
        if (started) {
            StartCoroutine(MoveLevelOff());
            Destroy(levelInstance.gameObject);
        }
    }
}
