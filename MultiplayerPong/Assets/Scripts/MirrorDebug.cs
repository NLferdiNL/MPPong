using UnityEngine;

public class MirrorDebug : MonoBehaviour {

    Transform target;

    [SerializeField]
    Transform reflection;

    void Start() {
        target = GetComponent<Transform>();
    }

    void Update() {
        Vector3 mirrored = target.position;

        mirrored.x = -mirrored.x;
        mirrored.y = -mirrored.y;
        mirrored.z = -mirrored.z;

        reflection.position = mirrored;

    }
}
