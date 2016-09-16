using UnityEngine;

public class Ball : MonoBehaviour {

    [SerializeField]
    Vector2 vel;
    Transform tf;

    [SerializeField]
    float maxVel = 0.5f;

    public Vector2 Vel {
        get { return vel; }
        set { vel = value; }
    }

    void Start() {
        vel = new Vector3();
        tf = GetComponent<Transform>();

        TriggerStart();
    }

    void Update() {
        tf.position += (Vector3)vel;
    }

    void OnCollisionEnter2D(Collision2D other) {
        string objectTag = other.gameObject.tag;

        if (objectTag == "Wall") {
            vel.y = -vel.y;
        } else if (objectTag == "Pad") {
            vel.x = -vel.x;
            vel.x *= 1.05f;

            if (vel.x > maxVel) {
                vel.x = maxVel;
            } else if (vel.x < -maxVel) {
                vel.x = -maxVel;
            }
        }
    }

    void TriggerStart() {
        vel.x = Random.Range(-0.1f, 0.1f);
        //vel.y = Random.Range(-0.2f, 0.2f);
    }
}
